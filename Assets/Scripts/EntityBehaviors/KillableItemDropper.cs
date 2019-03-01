using CCC.Items;
using UnityEngine;

namespace CCC.Behaviors
{
    /// <summary>
    /// Represents a Killable that also drops Items.
    /// </summary>
    public sealed class KillableItemDropper : MonoBehaviour, IKillable
    {
        [SerializeField]
        private CommandProcessor commandProcessor;

        private IItemDropper itemDropper;

        [SerializeField]
        private ItemGenerator itemGenerator;

        public void Die()
        {
            Item item = itemGenerator.GenerateItem();
            Debug.Log(gameObject.name + " died and dropped an item '" +
                item.Name + "'!");
            ICommand command = new DropItemCommand(itemDropper, item, transform.position);
            commandProcessor.ProcessCommand(command);

            GetComponent<EnemyController>().Die();
        }

        #region MonoBehaviour Messages
        private void Awake()
        {
            itemDropper = GetComponent<IItemDropper>();
        }

        // TODO: Just for testing. Remove after enemies are able to be killed.
        private void Update()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                ICommand command = new DieCommand(this);
                commandProcessor.ProcessCommand(command);
            }
        }
        #endregion
    }
}
