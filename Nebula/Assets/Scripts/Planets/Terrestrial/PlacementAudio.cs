using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementAudio : MonoBehaviour
{
    /// <summary>
    /// Audio source for placement sounds
    /// </summary>
    private AudioSource audioSource;
    public AudioClip[] placeAudioClips = new AudioClip[10];  
    // 0 drop-tree, 1 cactus-plant, 2 island-place, 3 snow-good,
    // 4 grass-good, 5 rocks-falling, 6 basic-thud, 7 sand-disperse
    // 8 digital-water, 9 pouring-sand

    private TerrainSelect ts;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ts = FindObjectOfType<TerrainSelect>();
    }
    
    /// <summary>
    /// Plays selected audio
    /// </summary>
    public void PlayPlacementAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    /// <summary>
    /// Stops playing audio
    /// </summary>
    public void StopPlacementAudio()
    {
        audioSource.Stop();
    }
    
    /// <summary>
    /// Chooses what biome audio clip to play
    /// </summary>
    /// <param name="selectedBiome">Selected biome</param>
    public void ChooseBiomeAudio(BiomePainter.BiomeNames selectedBiome)
    {
        print(selectedBiome);
        switch (selectedBiome)
        {
            case BiomePainter.BiomeNames.Plains:
            case BiomePainter.BiomeNames.Coniferous:
                audioSource.clip = placeAudioClips[4];
                break;
            case BiomePainter.BiomeNames.Savana:
            case BiomePainter.BiomeNames.Tropical:
                audioSource.clip = placeAudioClips[7];
                break;
            case BiomePainter.BiomeNames.Taiga:
            case BiomePainter.BiomeNames.Ice:
                audioSource.clip = placeAudioClips[3];
                break;
            case BiomePainter.BiomeNames.Ocean:
                audioSource.clip = placeAudioClips[8];
                break;
            case BiomePainter.BiomeNames.Temperate:
                audioSource.clip = placeAudioClips[5];
                break;
            default:
                audioSource.clip = placeAudioClips[6]; // default                
                break;
        }
    }

    /// <summary>
    /// Chooses what terrain audio clip to play
    /// </summary>
    /// <param name="selectedTerrain">Selected terrain</param>
    /*public void ChooseTerrainAudio(GameObject selectedTerrain)
    {
        if (selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Pine] ||
            selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.PineSnowy])
            audioSource.clip = placeAudioClips[0];
        else if (selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Cacti])
            audioSource.clip = placeAudioClips[1];
        else if (selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Island])
            audioSource.clip = placeAudioClips[2];
        else if (selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Rock] ||
                 selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.RockDesert])
            audioSource.clip = placeAudioClips[5];
        else if (selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.HillSnowy])
            audioSource.clip = placeAudioClips[3];
        else if (selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Bush])
            audioSource.clip = placeAudioClips[4];
        else if (selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Mountain] ||
                 selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Plateau] ||
                 selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Volcano] ||
                 selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.IceChunk])
            audioSource.clip = placeAudioClips[6];
        else
            audioSource.clip = placeAudioClips[9];
    }*/
}
