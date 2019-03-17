using UnityEngine;
using CCC.Items;

namespace CCC.Behaviors
{
    public class BossKillable : MonoBehaviour, IKillable
    {
        [SerializeField]
        private CommandProcessor commandProcessor;
        private IItemDropper itemDropper;
        [SerializeField]
        private ItemGenerator itemGenerator;
        public int maxItemsToDrop = 5;
        public float baseporb = 1f;

        void Awake()
        {
            itemDropper = GetComponent<IItemDropper>();
        }

        public void Die()
        {
            for (int i = 0; i < maxItemsToDrop; ++i)
            {
                if (Random.Range(0f, 1f) < baseporb)
                {
                    baseporb -= 0.1f;
                    Item item = itemGenerator.GenerateItem();
                    Debug.Log(gameObject.name + " died and dropped an item'" +
                        item.Name + "'!");
                    ICommand dropItemCommand = new DropItemCommand(itemDropper, item, transform.position + new Vector3(Random.Range(-0.75f, 0.75f), 0, Random.Range(-0.75f, 0.75f)));
                    commandProcessor.ProcessCommand(dropItemCommand);
                }
            }
            GetComponent<IActivatableBoss>().IsKilled();
        }
    }
}