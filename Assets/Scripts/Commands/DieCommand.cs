using CCC.Behaviors;

public sealed class DieCommand : ICommand
{
    private IKillable killable;

    public DieCommand(IKillable killable)
    {
        this.killable = killable;
    }

    public void InvokeCommand()
    {
        killable.Die();
    }
}
