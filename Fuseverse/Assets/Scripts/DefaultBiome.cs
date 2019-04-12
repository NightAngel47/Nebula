using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBiome : MonoBehaviour
{
    public Material mat;
    public string defaultBiome;
    public Texture[] biomeTexs;
    public string[] biomeTags;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        SetDefaultBiome();
    }

    void SetDefaultBiome()
    {
        int randNum = Random.Range(0, biomeTags.Length);

        mat.SetTexture("_texture", biomeTexs[randNum]);
        defaultBiome = biomeTags[randNum];
    }
}
