/// <summary>
/// Represents an ICommand that toggles the class info tab of the info menu 
/// being displayed.
/// </summary>
public sealed class ToggleClassInfoTabCommand : ICommand
{
    /// <summary>
    /// The InfoMenuScript to use.
    /// </summary>
    private readonly InfoMenuScript infoMenuScript;

    /// <summary>
    /// Initializes a new instance of the 
    /// <see cref="T:ToggleClassInfoTabCommand"/> class.
    /// </summary>
    /// <param name="infoMenuScript">Info menu script.</param>
    public ToggleClassInfoTabCommand(InfoMenuScript infoMenuScript)
    {
        this.infoMenuScript = infoMenuScript;
    }

    public void InvokeCommand()
    {
        infoMenuScript.ToggleClassInfoTab();
    }
}
