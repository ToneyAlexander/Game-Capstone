/// <summary>
/// Represents an ICommand that toggles the stats tab of the info menu being 
/// displayed.
/// </summary>
public sealed class ToggleStatsTabCommand : ICommand
{
    /// <summary>
    /// The InfoMenuScript to use.
    /// </summary>
    private readonly InfoMenuScript infoMenuScript;

    /// <summary>
    /// Initializes a new instance of the 
    /// <see cref="T:ToggleStatsTabCommand"/> class.
    /// </summary>
    /// <param name="infoMenuScript">The InfoMenuScript to use.</param>
    public ToggleStatsTabCommand(InfoMenuScript infoMenuScript)
    {
        this.infoMenuScript = infoMenuScript;
    }

    public void InvokeCommand()
    {
        infoMenuScript.ToggleStatsTab();
    }
}
