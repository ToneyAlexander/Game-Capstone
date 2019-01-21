using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Causes a GameObject to detect input from an input source. Requires a reference to a CommandProcessor.
/// </summary>
[RequireComponent(typeof(Movable))]
public class InputDetector : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;

    private Movable movable;

    private void Awake()
    {
        Assert.IsNotNull(inputManager, name + "'s InputManager is null!");
    }

    private void Start()
    {
        movable = GetComponent<Movable>();
    }

    private void Update()
    {
        float horizontalAxisValue = Input.GetAxis("Horizontal");
        inputManager.HandleHorizontalAxisInput(horizontalAxisValue, movable);

        float verticalAxisValue = Input.GetAxis("Vertical");
        inputManager.HandleVerticalAxisInput(verticalAxisValue, movable);
    }
}
