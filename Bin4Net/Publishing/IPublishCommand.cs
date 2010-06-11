namespace Bin4Net.Publishing
{
    /// <summary>
    /// Interface of commands
    /// </summary>
    public interface IPublishCommand
    {
        void Execute(ExecutionContext context);
    }
}