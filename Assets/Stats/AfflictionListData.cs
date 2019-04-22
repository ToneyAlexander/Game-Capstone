using System.Collections.Generic;

namespace CCC.Stats
{
    /// <summary>
    /// Data for an AfflictionList. Required for JSON serialization.
    /// </summary>
    [System.Serializable]
    public struct AfflictionListData
    {
        public static readonly AfflictionListData Null = new AfflictionListData
        {
            Afflictions = { }
        };

        public static AfflictionListData FromArray(Affliction[] afflictions)
        {
            return new AfflictionListData(afflictions);
        }

        public static AfflictionListData FromList(List<Affliction> afflictions)
        {
            var afflictionList = Null;

            if (afflictions.Count > 0)
            {
                afflictionList = FromArray(afflictions.ToArray());
            }

            return afflictionList;
        }

        public Affliction[] Afflictions;

        private AfflictionListData(Affliction[] afflictions)
        {
            Afflictions = afflictions;
        }
    }
}