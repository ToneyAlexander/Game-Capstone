using UnityEngine;

/// <summary>
/// Causes a GameObject to move.
/// </summary>
public class Movable : MonoBehaviour
{
    [SerializeField]
    private MovementData movementData;

    public void Move(Vector3 velocity)
    {
        if (!GameSystem.getPaused())
        {
            transform.Translate(velocity * Time.deltaTime * movementData.Speed);
        }
        
    }

}
