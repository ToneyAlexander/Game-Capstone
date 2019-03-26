using CCC.GameManagement.GameStates;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MainMenuGameStateChanger))]
public class LoadingScript : MonoBehaviour
{
    private MainMenuGameStateChanger mainMenuGameStateChanger;

    [SerializeField]
    private Image imageFlipper;

    [SerializeField]
    private GameObject loadingSprites;

    [SerializeField]
    private Slider loadingSlider;

    private float nextImage;
    private float nextLoad;
    private int imageIndex;
    private int imageCount;

    private void Awake()
    {
        mainMenuGameStateChanger = GetComponent<MainMenuGameStateChanger>();
    }

    void Start()
    {
        //CAN I JUST DO COROUTINE HERE
        StartCoroutine(LoadMainMenu());
        imageIndex = 0;
        imageCount = loadingSprites.transform.childCount;
        nextImage = Time.time + .9f;
        nextLoad = Time.time + .033f;
        displayNextImage();
    }

    void displayNextImage()
    {
        RectTransform imageFlipperRT = (RectTransform)imageFlipper.transform;
        Transform nextSpriteObject = loadingSprites.transform.GetChild(imageIndex);
        Sprite nextSprite = nextSpriteObject.GetComponent<SpriteRenderer>().sprite;
        imageFlipperRT.sizeDelta = new Vector2(nextSprite.rect.width * nextSpriteObject.localScale.x, nextSprite.rect.height * nextSpriteObject.localScale.y);
        imageFlipper.sprite = nextSprite;
        imageFlipper.color = nextSpriteObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextImage)
        {
            nextImage = Time.time + .9f;
            displayNextImage();
            imageIndex = (imageIndex + 1) % imageCount;
        }
        if(Time.time > nextLoad)
        {
            loadingSlider.value += .005f;
            nextLoad = Time.time + .033f;
        }
    }

    IEnumerator LoadMainMenu()
    {
        //Waits for 8 seconds before executing to show the logoState off
        yield return new WaitForSeconds(8);

        //Asynchronous operation to load SampleScene
        //AsyncOperation async = SceneManager.LoadSceneAsync("ProcGen");

        //Wait until the scene's done loading
        mainMenuGameStateChanger.ChangeGameState();
    }
}
