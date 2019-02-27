/// <summary>
/// Represents an ICommand that causes the inventory tab of the info menu to 
/// be displayed.
/// </summary>
public sealed class ToggleInventoryTabCommand : ICommand
{
    /// <summary>
    /// The InfoMenuScript instance to use.
    /// </summary>
    private InfoMenuScript infoMenuScript;

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
