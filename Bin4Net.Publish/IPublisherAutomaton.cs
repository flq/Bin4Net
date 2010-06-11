namespace Bin4Net.Publish
{
  /// <summary>
  /// Implement this interface to reuse interaction with the <see cref="IPublisher"/> interface.
  /// </summary>
  public interface IPublisherAutomaton
  {
    void Accept(IPublisher publisher);
  }
}