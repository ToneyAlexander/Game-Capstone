using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    [SerializeField]
    private Image imageFlipper;

    [SerializeField]
    private GameObject loadingSprites;

    private float nextImage;
    private int imageIndex;
    private int imageCount;

    void Start()
    {
        //CAN I JUST DO COROUTINE HERE
        StartCoroutine(LoadMainMenu());
        imageIndex = 0;
        imageCount = loadingSprites.transform.childCount;
        nextImage = Time.time + 2;
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
            nextImage = Time.time + 2;
            displayNextImage();
            imageIndex = (imageIndex + 1) % imageCount;
        }
    }

    IEnumerator LoadMainMenu()
    {
        //Waits for 20 seconds before executing to show the logoState off
        yield return new WaitForSeconds(20);

        //Asynchronous operation to load SampleScene
        AsyncOperation async = SceneManager.LoadSceneAsync("Menu");

        //Wait until the scene's done loading
        while (!async.isDone)
        {
            yield return null;
        }
    }
}
