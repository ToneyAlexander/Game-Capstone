using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCC.Stats
{
    [System.Serializable]
    public class PerkStatEntry
    {
        [SerializeField]
        private StatIdentifier statName;
        public Stat StatInst { get { return new Stat(statName.InternalStatName, statValue); } }

        [SerializeField]
        private float statValue;
    }
}
