using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.GameManagement.GameStates;
using CCC.GameManagement;

public class HubGameStateChanger : MonoBehaviour, IGameStateChanger
{
    // Start is called before the first frame update
    [SerializeField]
    private SceneChanger sceneChanger;

    [SerializeField]
    private Game game;
    [SerializeField]
    private SceneReference sceneReference;


    public void ChangeGameState()
    {
        game.CurrentState = new PlayIslandGameState(sceneChanger,
            sceneReference);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        sceneChanger = GetComponent<SceneChanger>();
    }
}
