/// <summary>
/// Represents an ICommand that toggles the inventory tab of the info menu 
/// being displayed.
/// </summary>
public sealed class ToggleInventoryTabCommand : ICommand
{
    /// <summary>
    /// The InfoMenuScript instance to use.
    /// </summary>
    private readonly InfoMenuScript infoMenuScript;

    /// <summary>
    /// Initializes a new instance of the 
    /// <see cref="T:ToggleInventoryTabCommand"/> class.
    /// </summary>
    /// <param name="infoMenuScript">The InfoMenuScript instance to use.</param>
    public ToggleInventoryTabCommand(InfoMenuScript infoMenuScript)
    {
        this.infoMenuScript = infoMenuScript;
    }

    public void InvokeCommand()
    {
        infoMenuScript.ToggleInventoryTab();
    }
}
