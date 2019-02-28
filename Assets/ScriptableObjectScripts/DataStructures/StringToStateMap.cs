using System.Collections.Generic;
using UnityEngine;

using States;

/// <summary>
/// Represents a map/dictionary of string to State that can be edited in the
/// Unity Editor's Inspector panel.
/// </summary>
[CreateAssetMenu( menuName = "DataStructures/StringToStateMap")]
public sealed class StringToStateMap : ScriptableObject
{
    [SerializeField]
    private State defaultState;

    [SerializeField]
    private string description;

    [System.Serializable]
    sealed class StringToStateMapEntry
    {
        public string Name
        {
            get { return name; }
        }

        public State GetState()
        {
            return state;
        }

        [SerializeField]
        private string name;

        [SerializeField]
        private State state;
    }

    [SerializeField]
    private List<StringToStateMapEntry> mappings;

    public List<string> Keys
    {
        get 
        {
            UpdateKeys();
            return keys;
        }
    }

    private List<string> keys = new List<string>();

    public State GetState(string name)
    {
        State state = defaultState;

        foreach(StringToStateMapEntry entry in mappings)
        {
            if (entry.Name == name)
            {
                state = entry.GetState();
                break;
            }
        }

        return state;
    }

    private void UpdateKeys()
    {
        keys = new List<string>();
        foreach (StringToStateMapEntry entry in mappings)
        {
            keys.Add(entry.Name);
        }
    }
}
