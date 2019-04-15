using System.Collections.Generic;

namespace CCC.Abilities
{
    [System.Serializable]
    struct AbilityList
    {
        public static readonly AbilityList Null = 
            new AbilityList(new Ability[]{ Ability.Null });

        public static AbilityList CreateEmpty()
        {
            return FromList(new List<Ability>());
        }

        public static AbilityList FromArray(Ability[] abilities)
        {
            return new AbilityList(abilities);
        }

        public static AbilityList FromList(List<Ability> abilities)
        {
            return FromArray(abilities.ToArray());
        }

        public Ability[] Abilities;

        private AbilityList(Ability[] abilities)
        {
            Abilities = abilities;
        }
    }
}