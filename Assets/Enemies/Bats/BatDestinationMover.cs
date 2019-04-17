using CCC.Movement;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Represents a Component that allows bat enemies to move.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public sealed class BatDestinationMover : MonoBehaviour, IDestinationMover
{
    public Vector3 Position
    {
        get { return transform.position; }
    }

    public void MoveTo(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }

    #region MonoBehaviour Messages
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    #endregion

    /// <summary>
    /// The NavMeshAgent that this BatDestinationMover uses to move.
    /// </summary>
    private NavMeshAgent navMeshAgent;
}
