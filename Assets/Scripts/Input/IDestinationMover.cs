using UnityEngine;

/// <summary>
/// Represents a Component that can be used to direct the GameObject it's 
/// attached to to move towards a given world space location.
/// </summary>
public interface IDestinationMover
{
    /// <summary>
    /// Get the current world space location of this IDestinationMover.
    /// </summary>
    /// <value>The current world space location.</value>
    Vector3 Position { get; }

    /// <summary>
    /// Cause this IDestionationMover to start to move towards the given world
    /// space location.
    /// </summary>
    /// <param name="destination">
    /// The world space location to start to move towards.
    /// </param>
    void MoveTo(Vector3 destination);
}
