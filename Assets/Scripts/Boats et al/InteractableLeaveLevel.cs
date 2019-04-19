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
            //  Debug.Log(Vector3.Distance(player.transform.position, transform.position));
            Debug.Log("Got here!;");
            Debug.Log("Is over UI: " + EventSystem.current.IsPointerOverGameObject());
            Debug.Log("Is far away: " + (Vector3.Distance(player.transform.position, transform.position) > 15.0f));
            Debug.Log("Fuck Unity tbh: " + clicked);
            Debug.Log("I'm going to LOSE IT: " + !(EventSystem.current.IsPointerOverGameObject() ||
                Vector3.Distance(player.transform.position, transform.position) > 15.0f));
            Debug.Log("I'm FUCKING DEAD: " + !clicked);
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAA: " + (!(EventSystem.current.IsPointerOverGameObject() ||
                Vector3.Distance(player.transform.position, transform.position) > 15.0f) && !clicked));

            if (!(EventSystem.current.IsPointerOverGameObject() ||
                Vector3.Distance(player.transform.position, transform.position) > 15.0f) && !clicked)
            {
                Debug.Log(gameObject.name + " changing state!");
                clicked = true;
                gameStateChanger.ChangeState();
            }

        }

        
        private void Awake()
        {
            if (!clicked)
            {
                
                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
                player = gameObjects[0];
                gameStateChanger = GetComponent<GameStateChanger>();
            }

        }
       
    }
}
