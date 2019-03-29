namespace CCC.GameManagement
{
    /// <summary>
    /// Represents something that is savable to a JSON file.
    /// </summary>
    public interface IJsonSavable
    {
        /// <summary>
        /// Save this IJsonSavable to a JSON file.
        /// </summary>
        void Save();
    }
}
