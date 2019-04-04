using CCC.GameManagement;
using CCC.GameManagement.GameStates;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RetireButton : MonoBehaviour
{
    [SerializeField]
    private GameManager manager;

    [SerializeField]
    private GameState retire;

    [SerializeField]
    private SceneChanger changer;

    [SerializeField]
    private SceneReference retireScene;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(RetireClicked);
    }

    void RetireClicked()
    {
        manager.TransitionTo(retire);
    }
}