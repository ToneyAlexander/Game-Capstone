using UnityEngine;

namespace CCC.Stats
{
    /// <summary>
    /// The data for a player's current level and experience.
    /// </summary>
    [System.Serializable]
    public sealed class LevelExpData
    {
        public float Exp
        {
            get { return exp; }
            set { exp = value; }
        }

        public float ExpToLevel
        {
            get { return expToLevel; }
            set { expToLevel = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public int PerkPoints
        {
            get { return perkPoints; }
            set { perkPoints = value; }
        }

        /// <summary>
        /// The player's current experience.
        /// </summary>
        [SerializeField]
        private float exp;

        /// <summary>
        /// The total experience the player needs to reach the next level.
        /// </summary>
        [SerializeField]
        private float expToLevel;

        /// <summary>
        /// The player's current level.
        /// </summary>
        [SerializeField]
        private int level;

        /// <summary>
        /// The player's current number of perk points.
        /// </summary>
        [SerializeField]
        private int perkPoints;

        public static LevelExpData CreateLevelOne()
        {
            return new LevelExpData(0.0f, 100.0f, 1, 1);
        }

        public static LevelExpData CreateEmpty()
        {
            return new LevelExpData(0.0f, 0.0f, 0, 0);
        }

        public LevelExpData(float exp, float expToLevel, int level, 
            int perkPoints)
        {
            this.exp = exp;
            this.expToLevel = expToLevel;
            this.level = level;
            this.perkPoints = perkPoints;
        }
    }
}
