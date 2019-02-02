namespace CCC.Items
{
    /// <summary>
    /// Represents something that can drop Items.
    /// </summary>
    public interface ItemDropper
    {
        /// <summary>
        /// Drop an Item.
        /// </summary>
        /// <returns>The Item.</returns>
        Item DropItem();
    }
}
