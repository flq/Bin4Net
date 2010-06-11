using System.Collections.Generic;
using System.Collections.ObjectModel;
using Bin4Net.Publishing.PublishCommands;

namespace Bin4Net.Publishing
{
  internal interface IPublishCommands : IEnumerable<IPublishCommand>
  {
    ReadOnlyCollection<IPublishCommand> Commands { get; }
    void Prepend(IPublishCommand command);
    void Append(IPublishCommand command);
  }
}