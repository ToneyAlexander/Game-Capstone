using CCC.Items;
using UnityEngine;

namespace CCC.Behaviors.Props
{
    /// <summary>
    /// Represents an interactable prop that acts like a chest meaning it drops 
    /// Items.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public sealed class InteractableChest : MonoBehaviour, IInteractable
    {
        private Animator animator;

        /// <summary>
        /// The CommandProcessor that ICommands created by this 
        /// InteractableChest are sent to.
        /// </summary>
        [SerializeField]
        private CommandProcessor commandProcessor;

        /// <summary>
        /// The IItemDropper that actually drops Items for this 
        /// InteractableChest.
        /// </summary>
        [SerializeField]
        private IItemDropper itemDropper;

        /// <summary>
        /// The ItemGenerator that this InteractableChest uses to create Items 
        /// to drop.
        /// </summary>
        [SerializeField]
        private ItemGenerator itemGenerator;

        #region IInteractable
        public void RespondToInteraction()
        {
            if (itemDropper != null)
            {
                animator.SetBool("isOpen", true);
                // This InteractableChets's Animator will take care of changing 
                // the state to open and dropping the item.
            }
        }
        #endregion

        #region MonoBehaviour Messages
        private void Awake()
        {
            animator = GetComponent<Animator>();
            itemDropper = GetComponent<IItemDropper>();

            if (itemDropper == null)
            {
                Debug.LogError("[" + gameObject.name + ".InteractableChest]" +
                    gameObject.name + " does not have an IItemDropper" + 
                    " Component!");
            }
        }
        #endregion

        /// <summary>
        /// Drop an Item.
        /// </summary>
        private void DropItem()
        {
            Item item = itemGenerator.GenerateItem();
            ICommand dropItemCommand = new DropItemCommand(itemDropper, item, 
                transform.position);
            commandProcessor.ProcessCommand(dropItemCommand);
        }
    }
}
