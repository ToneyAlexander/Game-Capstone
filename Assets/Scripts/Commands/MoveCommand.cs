using UnityEngine;

/// <summary>
/// An ICommand to move a Movable.
/// </summary>
public class MoveCommand : ICommand
{
    private Movable movable;
    private Vector3 velocity;

    public MoveCommand(Movable movable, Vector3 velocity)
    {
        this.movable = movable;
        this.velocity = velocity;
    }

    public void InvokeCommand()
    {
        movable.Move(velocity);
    }
}
