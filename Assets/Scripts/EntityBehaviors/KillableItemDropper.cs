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
        [SerializeField]
        private bool dontDropItem;
        private IItemDropper itemDropper;
        private int levelToAdd;

        [SerializeField]
        private ItemGenerator itemGenerator;

        public void Die()
        {
            if(!dontDropItem)
            {
                Item item = itemGenerator.GenerateItem(levelToAdd);
                Debug.Log(gameObject.name + " died and dropped an item '" +
                    item.Name + "'! Tier Increased by: " + levelToAdd);
                ICommand dropItemCommand = new DropItemCommand(itemDropper, item, transform.position);
                commandProcessor.ProcessCommand(dropItemCommand);
            }
            GetComponent<EnemyController>().isKilled();
        }

        #region MonoBehaviour Messages
        private void Awake()
        {
            itemDropper = GetComponent<IItemDropper>();
            levelToAdd = GameObject.Find("Generator").GetComponent<GenerateIsland>().islandStorage.level / 4;
        }

        // TODO: Just for testing. Remove after enemies are able to be killed.
        //private void Update()
        //{
        //    if (Input.GetButtonDown("Fire2"))
        //    {
        //        ICommand command = new DieCommand(this);
        //        commandProcessor.ProcessCommand(command);
        //    }
        //}
        #endregion
    }
}
