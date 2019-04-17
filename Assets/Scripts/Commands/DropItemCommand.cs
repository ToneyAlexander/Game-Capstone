using CCC.Behaviors;
using UnityEngine;

namespace CCC.Items
{
    public class DropItemCommand : ICommand
    {
        private readonly IItemDropper itemDropper;
        private readonly Item item;
        private Vector3 position;

        public DropItemCommand(IItemDropper itemDropper, Item item, Vector3 position)
        {
            this.itemDropper = itemDropper;
            this.item = item;
            this.position = position;
        }

        public void InvokeCommand()
        {
            itemDropper.DropItem(item, position);
        }
    }
}
