using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Represents a Component that allows Fungus enemies to move.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class SpiderMover : MonoBehaviour, IDestinationMover
{
    public Vector3 Position
    {
        get { return transform.position; }
    }

    public void MoveTo(Vector3 destination)
    {
        animator.SetBool("Run Forward", true);
        navMeshAgent.SetDestination(destination);
    }

    #region MonoBehaviour Messages
    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    #endregion

    /// <summary>
    /// The Animator that this FungusDestinationMover uses to animate.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// The NavMeshAgent that this FungusDestinationMover uses to move.
    /// </summary>
    private NavMeshAgent navMeshAgent;
}
