using UnityEngine;

/// <summary>
/// Causes a GameObject to move.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Movable : MonoBehaviour
{
    [SerializeField]
    private MovementData movementData;

    private Rigidbody rb;

    private Vector3 velocity = Vector3.zero;

    public void Move(Vector3 newVelocity)
    {
        if (!GameSystem.getPaused())
        {
            velocity = newVelocity * movementData.Speed;
        }

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = velocity;
    }

}
