using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PrintArma : MonoBehaviour
{
    public Camera renderCamera; // arraste a câmera aqui no inspetor
    public int width = 1024;
    public int height = 1024;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // pressione "P" para capturar
        {
            StartCoroutine(CaptureScreenshot());
        }
    }

    IEnumerator CaptureScreenshot()
    {
        yield return new WaitForEndOfFrame();

        RenderTexture rt = new RenderTexture(width, height, 24);
        renderCamera.targetTexture = rt;

        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGBA32, false);
        renderCamera.Render();

        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenshot.Apply();

        renderCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        string path = Application.dataPath + "/Prints/print_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        Directory.CreateDirectory(Application.dataPath + "/Prints");
        File.WriteAllBytes(path, screenshot.EncodeToPNG());

        Debug.Log("Imagem salva em: " + path);
    }
}
