using UnityEngine;

/// <summary>
/// Detects input.
/// </summary>
public class InputDetector : MonoBehaviour
{
    [SerializeField]
    private CommandProcessor commandProcessor;

    [SerializeField]
    private Movable movable;

    private void Update()
    {
        float z = Input.GetAxis("Vertical");
        Vector3 velocity = new Vector3(0.0f, 0.0f, z);
        commandProcessor.ProcessCommand(new MoveCommand(movable, velocity));
    }
}
