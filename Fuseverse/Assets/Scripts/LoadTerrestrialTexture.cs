using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadTerrestrialTexture : MonoBehaviour
{
    public void LoadTexture()
    {
        GetComponent<Renderer>().material.SetTexture("_Texture", LoadPNG(Application.persistentDataPath + "//UserCanvas//CanvasTexture.png"));
    }

    //https://answers.unity.com/questions/432655/loading-texture-file-from-pngjpg-file-on-disk.html

    private Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
}
