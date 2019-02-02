using UnityEngine;

[CreateAssetMenu]
public sealed class InputButton : ScriptableObject
{
    public string Name
    {
        get { return _name; }
    }

    /// <summary>
    /// The description of this InputButton.
    /// </summary>
    [SerializeField]
    private string _description;

    /// <summary>
    /// The name of this InputButton as defined in the Project Settings Input
    /// panel.
    /// </summary>
    [SerializeField]
    private string _name;
}
