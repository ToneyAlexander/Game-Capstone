using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Script for capturing screenshots.
 * Press 'P' to take a screenshot -> will save in Assets/Snapshots folder
 */
public class ImageCapture : MonoBehaviour
{
    public int resWidth = 1920; //width of screenshot
    public int resHeight = 1080; //height of sceenshot
    private bool takeScreenShot = false;
    
    void LateUpdate()
    {
        takeScreenShot |= Input.GetKeyDown(KeyCode.P);
        if(takeScreenShot){
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            GetComponent<Camera>().targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            GetComponent<Camera>().Render();
            RenderTexture.active = GetComponent<Camera>().targetTexture;
            screenShot.ReadPixels(new Rect(0,0,resWidth, resHeight), 0, 0);
			GetComponent<Camera>().targetTexture = null;
			RenderTexture.active = null; 
			Destroy(rt);
			byte[] bytes = screenShot.EncodeToPNG();
            string fileName = ScreenshotName();
            System.IO.File.WriteAllBytes(fileName, bytes);
            Debug.Log("Captured!");
            takeScreenShot = false;
        }
    }

    string ScreenshotName(){
        return string.Format("{0}/Snapshots/snap_{1}x{2}_{3}.png", 
        Application.dataPath, resWidth, resHeight, 
        System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
}
