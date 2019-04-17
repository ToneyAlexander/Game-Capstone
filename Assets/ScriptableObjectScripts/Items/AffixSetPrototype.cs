using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Items;
[CreateAssetMenu(menuName = "Items/AffixSetPrototype")]
public class AffixSetPrototype : ScriptableObject
{
    [SerializeField]
    private string setName;
    [SerializeField]
    private string setNameShort;
    [SerializeField]
    List<Affix> tiers;

    public string SetName { get { return setName; } }
    public string SetNameShort { get { return setNameShort; } }
    public List<Affix> Tiers { get { return tiers; } }
}
