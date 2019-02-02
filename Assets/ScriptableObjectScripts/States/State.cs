using UnityEngine;

namespace States
{
    public abstract class State : ScriptableObject
    {
        public abstract void Handle();
    }
}
