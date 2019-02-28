using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a list of InputButtons that can be added to in the Unity 
/// Inspector.
/// </summary>
[CreateAssetMenu(menuName = "DataStructures/InputButtonList")]
public sealed class InputButtonList : ScriptableObject
{
    /// <summary>
    /// Get all the InputButtons that this InputButtonList contains.
    /// </summary>
    /// <value>The InputButtons that this InputButotnList contains.</value>
    public List<InputButton> Buttons
    {
        get { return buttons; }
    }

    /// <summary>
    /// The list of InputButtons that this InputButtonList contains.
    /// </summary>
    [SerializeField]
    private List<InputButton> buttons = new List<InputButton>();
}
