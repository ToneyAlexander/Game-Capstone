using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.GameManagement.GameStates;
using CCC.GameManagement;

public class HubGameState : IGameState
{
    private readonly SceneChanger sceneChanger;
    private readonly SceneReference playIslandSceneReference;

    public HubGameState(SceneChanger sceneChanger,
            SceneReference playIslandSceneReference)
    {
        this.sceneChanger = sceneChanger;
        this.playIslandSceneReference = playIslandSceneReference;
    }
    public void Enter(Game game)
    {
        sceneChanger.ChangeToScene(playIslandSceneReference);
    }

    public void Exit(Game game)
    {
        // No-op
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
