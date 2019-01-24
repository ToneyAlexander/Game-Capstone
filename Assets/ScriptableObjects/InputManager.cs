using UnityEngine;
using States;

[CreateAssetMenu]
public sealed class InputManager : ScriptableObject
{
    [SerializeField]
    private CommandProcessor commandProcessor;

    public void HandleHorizontalAxisInput(float axisValue, Movable movable)
    {
        Vector3 velocity = Vector3.right * axisValue;
        SendMoveCommand(velocity, movable);
    }

    public void HandleVerticalAxisInput(float axisValue, Movable movable)
    {
        Vector3 velocity = Vector3.forward * axisValue;
        SendMoveCommand(velocity, movable);
    }

    private void SendMoveCommand(Vector3 velocity, Movable movable)
    {
        ICommand command = new MoveCommand(movable, velocity);
        commandProcessor.ProcessCommand(command);
    }
    public void SendPauseCommand()
    {
        ICommand command = new PauseCommand(GameSystem.getMenu("Pause"));
        commandProcessor.ProcessCommand(command);
    }
}
