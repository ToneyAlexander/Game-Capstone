using CCC.GameManagement.GameStates;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CCC.Behaviors.Props
{
    [RequireComponent(typeof(GameStateChanger))]
    public class InteractableIslandTransfer : MonoBehaviour, IInteractable
    {
        private GameStateChanger gameStateChanger;

        private bool clicked = false;

        public void RespondToInteraction()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                clicked = false;
            }
            else
            {
                Debug.Log(gameObject.name + " changing state!");
                gameStateChanger.ChangeState();
            }

        }

        #region MonoBehaviour Messages
        private void Awake()
        {
            if (!clicked)
            {
                clicked = true;
                gameStateChanger = GetComponent<GameStateChanger>();
            }
        }
        #endregion
    }
}
