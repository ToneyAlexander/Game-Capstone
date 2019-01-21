using UnityEngine;

[CreateAssetMenu]
public sealed class InputManager : ScriptableObject
{
    [SerializeField]
    private CommandProcessor commandProcessor;

    public void HandleHorizontalAxisInput(float axisValue, Movable movable)
    {
        Vector3 velocity = new Vector3(axisValue, 0.0f, 0.0f);
        SendMoveCommand(velocity, movable);
    }

    public void HandleVerticalAxisInput(float axisValue, Movable movable)
    {
        Vector3 velocity = new Vector3(0.0f, 0.0f, axisValue);
        SendMoveCommand(velocity, movable);
    }

    private void SendMoveCommand(Vector3 velocity, Movable movable)
    {
        ICommand command = new MoveCommand(movable, velocity);
        commandProcessor.ProcessCommand(command);
    }
}
