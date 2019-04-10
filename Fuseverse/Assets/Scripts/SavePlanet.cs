using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlanet : MonoBehaviour
{
    public Transform rings;
    public RenderTexture canvasTexture;
    public Material baseMaterial;

    private void Start()
    {
        UpdatePlanets();
    }

    public void UpdatePlanets()
    {
        if ((SceneManager.GetActiveScene().name == "TerrestrialCreator" || 
            SceneManager.GetActiveScene().name == "GasCreator") &&
            SceneManager.GetActiveScene().name != "Complete Screen")
        {
            DontDestroyOnLoad(gameObject);
        }
        else if(SceneManager.GetActiveScene().name == "Main Menu")
        {
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name == "Complete Screen")
        {
            // move planet
            transform.position = GameObject.FindGameObjectWithTag("FinishPos").transform.position;
            transform.localScale = GameObject.FindGameObjectWithTag("FinishPos").transform.localScale;

            // make planet spin
            gameObject.AddComponent<planetSpin>();

            // destroy unnessessary componets
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<CameraRotation>());
            Destroy(gameObject.GetComponent<MeshCollider>());
            Destroy(gameObject.GetComponent<GasGiantController>());
            Destroy(gameObject.GetComponent<HideRings>());
            Destroy(gameObject.GetComponent<GasRotation>());

            if (rings != null)
            {
                rings.localScale = GameObject.FindGameObjectWithTag("FinishPos").transform.localScale;
            }
        }
    }

    //https://codeartist.mx/dynamic-texture-painting/

    public void SaveTexture()
    {
        System.DateTime date = System.DateTime.Now;
        RenderTexture.active = canvasTexture;
        Texture2D tex = new Texture2D(canvasTexture.width, canvasTexture.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, canvasTexture.width, canvasTexture.height), 0, 0);
        tex.Apply();
        RenderTexture.active = null;
        baseMaterial.mainTexture = tex; //Put the painted texture as the base
        //foreach (Transform child in brushContainer.transform)
        //{//Clear brushes
        //    Destroy(child.gameObject);
        //}
        StartCoroutine(SaveTextureToFile(tex)); // save the texture
    }

    IEnumerator SaveTextureToFile(Texture2D savedTexture)
    {
        string fullPath = System.IO.Directory.GetCurrentDirectory() + "\\UserCanvas\\";
        System.DateTime date = System.DateTime.Now;
        string fileName = "CanvasTexture.png";
        if (!System.IO.Directory.Exists(fullPath))
            System.IO.Directory.CreateDirectory(fullPath);
        var bytes = savedTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(fullPath + fileName, bytes);
        Debug.Log("<color=orange>Saved Successfully!</color>" + fullPath + fileName);
        yield return null;
    }
}
