using CCC.GameManagement.GameStates;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

[RequireComponent(typeof(GameStateChanger))]
public class ProcedeButton : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private GameObject BlackBox;

    [SerializeField]
    private GameObject remy;

    [SerializeField]
    private GameObject display;

    [SerializeField]
    private CommandProcessor processor;

    private GameObject camera;
    private Bloom bloom;

    private bool isChangingState = false;

    /// <summary>
    /// The GameStateChanger that will be used to change the game state.
    /// </summary>
    private GameStateChanger gameStateChanger;

    private bool clicked = false;

    private bool ended = false;

    private float alpha = 1f;

    private float exposure = 2.21f;

    private void Awake()
    {
        for (int i = 0; i < remy.transform.childCount; i++)
        {
            if (remy.transform.GetChild(i).name.Equals("Main Camera"))
            {
                camera = remy.transform.GetChild(i).gameObject;
            }
        }

        bloom = camera.GetComponent<PostProcessVolume>().profile.GetSetting<Bloom>();
        gameStateChanger = GetComponent<GameStateChanger>();
    }

    void Start()
    {
        button.onClick.AddListener(ProcedeClicked);

        RenderSettings.skybox.SetFloat("_Exposure", exposure);

        //How to slow remy down at start?
        //How to make remy running to somewhere
    }

    void ProcedeClicked()
    {
        clicked = true;
    }

    private void Update()
    {
        if (!ended)
        {
            remy.GetComponent<Animator>().SetBool("isRunning", true);
        }

        if (clicked)
        {
            if (display.GetComponent<CanvasGroup>().alpha > .001)
            {
                display.GetComponent<CanvasGroup>().alpha -= .01f;
            }
            else if (alpha > .001)
            {
                processor.ProcessCommand(new MoveToCommand(remy.GetComponent<IDestinationMover>(), remy.transform.position, new Vector3(136f, 4f, 83f)));
                for (int i = 0; i < BlackBox.transform.childCount; i++)
                {
                    Color c = BlackBox.transform.GetChild(i).GetComponent<Renderer>().material.color;
                    c.a -= 0.01f;
                    BlackBox.transform.GetChild(i).GetComponent<Renderer>().material.color = c;
                }
                alpha -= .01f;
            }
            else if(!ended)
            {
                ended = true;
                Destroy(BlackBox);
                
                StartCoroutine(RunToDistance());
            }
            else if(bloom.intensity.value < 75)
            {
                bloom.intensity.value += .1f;
            }
            else
            {
                //TODO: GO TO MAIN MENU/CLASS SELECT?
                if (!isChangingState)
                {
                    isChangingState = true;
                    gameStateChanger.ChangeState();
                }
            }
        }
    }

    IEnumerator RunToDistance()
    {
        //Wait for a time
        yield return new WaitForSeconds(4);

        camera.transform.parent = null;
        GameObject.Find("Wind Sounder").GetComponent<AudioFadeOut>().start = true;
        //Make remy run into distance
        processor.ProcessCommand(new MoveToCommand(remy.GetComponent<IDestinationMover>(), remy.transform.position, new Vector3(136f, 2.5f, 700f)));

        //Fade to white
        Instantiate(Resources.Load<GameObject>("White Screen"));
    }
}