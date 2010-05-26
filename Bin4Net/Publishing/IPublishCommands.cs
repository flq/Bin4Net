using System.Collections.Generic;
using System.Collections.ObjectModel;
using Bin4Net.Publishing.PublishCommands;

namespace Bin4Net.Publishing
{
  internal interface IPublishCommands : IEnumerable<PublishCommand>
  {
    ReadOnlyCollection<PublishCommand> Commands { get; }
    void Prepend(PublishCommand command);
    void Append(PublishCommand command);
  }
}