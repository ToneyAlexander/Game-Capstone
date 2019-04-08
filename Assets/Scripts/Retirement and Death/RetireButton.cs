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

    private float timing;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(RetireClicked);
        timing = 5f;
    }

    void RetireClicked()
    {
        GameObject screen = Resources.Load<GameObject>("Black Screen");
        screen.GetComponentInChildren<FadeIn>().time = timing;
        Instantiate(screen);
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(timing + .1f);
        manager.TransitionTo(retire);
    }
}