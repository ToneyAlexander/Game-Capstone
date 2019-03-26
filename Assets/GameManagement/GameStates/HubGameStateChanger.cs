using UnityEngine;
using CCC.GameManagement.GameStates;
using CCC.GameManagement;

public class HubGameStateChanger : MonoBehaviour, IGameStateChanger
{
    [SerializeField]
    private Game game;

    [SerializeField]
    private SceneReference sceneReference;


    public void ChangeGameState()
    {
        game.TransitionTo(new PlayIslandGameState(sceneReference));
    }
}
