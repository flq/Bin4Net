using System.Collections.Generic;
using System.Collections.ObjectModel;
using Bin4Net.PublishCommands;

namespace Bin4Net
{
  internal interface IPublishCommands : IEnumerable<PublishCommand>
  {
    ReadOnlyCollection<PublishCommand> Commands { get; }
    void Prepend(PublishCommand command);
    void Append(PublishCommand command);
  }
}