using UnityEngine;

public class DefaultBiome : MonoBehaviour
{
    private Material mat;

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
        gameObject.tag = biomeTags[randNum];
    }
}
