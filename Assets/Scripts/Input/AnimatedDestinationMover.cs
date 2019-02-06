using UnityEngine;

/// <summary>
/// Represents a Component that causes its GameObject to move towards a given
/// world space location and animate itself as it moves.
/// </summary>
public sealed class AnimatedDestinationMover : MonoBehaviour, IDestinationMover
{
    #region IDestinationMover
    public Vector3 Position
    {
        get { return transform.position; }
    }

    public void MoveTo(Vector3 destination)
    {
        RemyController.destination = destination;
    }
    #endregion
}
