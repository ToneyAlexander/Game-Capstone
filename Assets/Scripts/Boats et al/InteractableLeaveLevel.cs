using CCC.GameManagement.GameStates;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CCC.Behaviors.Props
{
    [RequireComponent(typeof(GameStateChanger))]
    public class InteractableLeaveLevel : MonoBehaviour, IInteractable
    {
        private GameStateChanger gameStateChanger;
        private GameObject player;
        private bool clicked = false;

        public void RespondToInteraction()
        {
            Debug.Log(Vector3.Distance(player.transform.position, transform.position));
            if (EventSystem.current.IsPointerOverGameObject() || Vector3.Distance(player.transform.position,transform.position) > 15.0f)
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
                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
                player = gameObjects[0];
                gameStateChanger = GetComponent<GameStateChanger>();
            }

        }
        #endregion
    }
}
