using CCC.Behaviors;
using CCC.Items;
using UnityEngine;

/// <summary>
/// Represents an interactable prop that acts like a chest meaning it drops 
/// Items.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public sealed class InteractableChest : MonoBehaviour, IInteractable
{
    /// <summary>
    /// The Animator that handles this InteractableChest's animations.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// The AudioSource that this InteractableChest uses to play audio.
    /// </summary>
    private AudioSource audioSource;

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
            audioSource.Play();
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
        audioSource = GetComponent<AudioSource>();

        itemDropper = GetComponent<IItemDropper>();
        if (itemDropper == null)
        {
            Debug.LogError("[" + gameObject.name +
                ".InteractableChest.Awake]" + " itemDropper is null");
        }
    }
    #endregion

    /// <summary>
    /// Drop an Item. This method is called by an InteractableChets's Animator.
    /// </summary>
    private void DropItem()
    {
        Item item = itemGenerator.GenerateItem();
        ICommand dropItemCommand = new DropItemCommand(itemDropper, item, 
            transform.position);
        commandProcessor.ProcessCommand(dropItemCommand);
    }
}
