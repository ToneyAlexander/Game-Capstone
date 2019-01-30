/// <summary>
/// An ICommand that does nothing.
/// </summary>
public sealed class NullCommand : ICommand
{
    public void InvokeCommand()
    {
        // No-op
    }
}
