using CCC.Behaviors;
using UnityEngine;

public sealed class KillablePlayer : MonoBehaviour, IKillable
{
    public void Die()
    {
        Debug.Log("Player '" + gameObject.name + "' died!");
    }
}
