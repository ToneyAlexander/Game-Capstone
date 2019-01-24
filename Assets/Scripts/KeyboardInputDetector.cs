using UnityEngine;
using States;

/// <summary>
/// Causes a GameObject to detect input from a keyboard.
/// </summary>
public class KeyboardInputDetector : MonoBehaviour
{
    [SerializeField]
    private StringToStateMap keyboardButtonMap;

    [SerializeField]
    private StateMachine gameStateMachine;

    private void Update()
    {
        foreach (string buttonName in keyboardButtonMap.Keys)
        {
            if (Input.GetButtonDown(buttonName))
            {
                gameStateMachine.SetState(keyboardButtonMap.GetState(buttonName));
            }
        }
    }
}
