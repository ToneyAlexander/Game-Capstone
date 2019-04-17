/// <summary>
/// An ICommand that does nothing.
/// </summary>
public sealed class NullCommand : ICommand
{
    public static NullCommand Instance
    {
        get { return instance; }
    }

    private static readonly NullCommand instance = new NullCommand();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:NullCommand"/> class. 
    /// Private so no other instances can be created.
    /// </summary>
    private NullCommand()
    {
    }

    public void InvokeCommand()
    {
        // No-op
    }
}
