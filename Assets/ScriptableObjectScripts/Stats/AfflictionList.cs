using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/AfflictionList")]
public class AfflictionList : ScriptableObject
{

    public string listName;
    public string listDescription;
    public List<Affliction> afflictions;
}
