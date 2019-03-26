using CCC.GameManagement.GameStates;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

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

    private MainMenuGameStateChanger mainMenuGameStateChanger;

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
                camera.transform.parent = null;
            }
        }

        bloom = camera.GetComponent<PostProcessVolume>().profile.GetSetting<Bloom>();
        mainMenuGameStateChanger = GetComponent<MainMenuGameStateChanger>();
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
            else if(bloom.intensity.value < 100)
            {
                bloom.intensity.value += .1f;
            }
            else
            {
                //TODO: GO TO MAIN MENU/CLASS SELECT?
                //remy.GetComponent<MainMenuGameStateChanger>().ChangeGameState();
                if (!isChangingState)
                {
                    isChangingState = true;
                    mainMenuGameStateChanger.ChangeGameState();
                    Debug.Log("Changed game state to MainMenuGameState");
                }
            }
        }
    }

    IEnumerator RunToDistance()
    {
        //Wait for a time
        yield return new WaitForSeconds(4);

        //Make remy run into distance
        processor.ProcessCommand(new MoveToCommand(remy.GetComponent<IDestinationMover>(), remy.transform.position, new Vector3(136f, 2.5f, 700f)));

        //Fade to white
        Instantiate(Resources.Load<GameObject>("White Screen"));
    }
}