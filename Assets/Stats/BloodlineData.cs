namespace CCC.Stats
{
    [System.Serializable]
    struct BloodlineData
    {
        public static BloodlineData ForAge(int age)
        {
            return new BloodlineData(age);
        }

        public int Age;

        private BloodlineData(int age)
        {
            Age = age;
        }
    }
}