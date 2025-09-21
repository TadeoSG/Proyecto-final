using UnityEngine;

public class ScreenshotTool : MonoBehaviour
{
    public Camera cam;
    public string saveName = "Level1Preview.png";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Optional: set camera to RenderTexture
            RenderTexture rt = new RenderTexture(512, 512, 24);
            cam.targetTexture = rt;

            RenderTexture.active = rt;
            cam.Render();

            Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect(0,0, rt.width, rt.height), 0, 0);
            tex.Apply();
            RenderTexture.active = null;

            byte[] bytes = tex.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/Textures/" + saveName, bytes);

            Debug.Log("Saved screenshot: " + saveName);

            cam.targetTexture = null;
        }
    }
}
