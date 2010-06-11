using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Bin4Net.Publish;
using Bin4Net.Publishing.PublishCommands;

namespace Bin4Net.Publishing.Inlets
{
    internal class PublishInlet : IPublisher, IPublishCommands
    {
        private readonly List<IPublishCommand> commands = new List<IPublishCommand>();

        public IPublisher ApplyAutomaton<T>() where T : IPublisherAutomaton, new()
        {
            var a = new T();
            a.Accept(this);
            return this;
        }

        public IPublisher SetupMetadata(Action<IMetadataInlet> metadata)
        {
            var i = new MetadataInlet(this);
            metadata(i);
            return this;
        }

        public IPublisher Versioning(Action<IVersioningInlet> versioning)
        {
            var i = new VersioningInlet(this);
            versioning(i);
            return this;
        }

        public IPublisher DependsOn(Action<IDependencyInlet> dependencies)
        {
            return this;
        }

        public IPublisher Download(Action<IDownloadInlet> download)
        {
            download(new DownloadInlet(this));
            return this;
        }

        public IPublisher AssociateWithTags(params string[] tags)
        {
            Add(new TagSetupCommand(tags));
            return this;
        }

        ReadOnlyCollection<IPublishCommand> IPublishCommands.Commands
        {
            get { return commands.AsReadOnly(); }
        }

        void IPublishCommands.Prepend(IPublishCommand command)
        {
            commands.Insert(0, command);
        }

        void IPublishCommands.Append(IPublishCommand command)
        {
            commands.Add(command);
        }

        internal void Add(IPublishCommand command)
        {
            commands.Add(command);
        }

        IEnumerator<IPublishCommand> IEnumerable<IPublishCommand>.GetEnumerator()
        {
            return commands.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<PublishCommand>)this).GetEnumerator();
        }
    }
}