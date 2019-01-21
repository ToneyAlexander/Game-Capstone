using UnityEngine;

/// <summary>
/// Represents data related to GameObjects that can move.
/// </summary>
[CreateAssetMenu]
public class MovementData : ScriptableObject
{
    public float Speed
    {
        get { return speed; }
    }

    [SerializeField]
    private float speed;
}
