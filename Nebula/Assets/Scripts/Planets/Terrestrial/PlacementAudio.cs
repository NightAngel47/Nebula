using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementAudio : MonoBehaviour
{
    /// <summary>
    /// Audio source for placement sounds
    /// </summary>
    [SerializeField]
    private AudioSource audioSource;
    public AudioClip[] placeAudioClips = new AudioClip[10];
    private enum PlacementSounds
    {
        DropTree,
        CactusPlant,
        IslandPlace,
        GrassGood,
        RocksFalling,
        BasicThud,
        SandDisperse,
        DigitalWater,
        PouringSand
    }

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
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
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
            case BiomePainter.BiomeNames.Savana:
            case BiomePainter.BiomeNames.Coniferous:
            case BiomePainter.BiomeNames.Taiga:
                audioSource.clip = placeAudioClips[(int) PlacementSounds.GrassGood];
                break;
            case BiomePainter.BiomeNames.Tropical:
            case BiomePainter.BiomeNames.Temperate:
                audioSource.clip = placeAudioClips[(int) PlacementSounds.SandDisperse];
                break;
            case BiomePainter.BiomeNames.Ocean:
            case BiomePainter.BiomeNames.Ice:
                audioSource.clip = placeAudioClips[(int) PlacementSounds.DigitalWater];
                break;
            default:
                audioSource.clip = placeAudioClips[(int) PlacementSounds.BasicThud]; // default                
                break;
        }
    }

    /// <summary>
    /// Chooses what terrain audio clip to play
    /// </summary>
    /// <param name="selectedTerrain">Selected terrain</param>
    public void ChooseTerrainAudio(GameObject selectedTerrain)
    {
        if (selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.ConiferousTree] ||
            selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.BirchTree] ||
            selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.JoshuaTree] ||
            selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.AcaciaTree] ||
            selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Palm] ||
            selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.OakTree])
        {
            audioSource.clip = placeAudioClips[(int) PlacementSounds.DropTree];
        }
        else if (selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Cactus] || 
                 selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.TemperateShrub] ||
                 selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.ElephantGrass] ||
                 selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.TropicalShrub] ||
                 selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.AbalShrub])
        {
            audioSource.clip = placeAudioClips[(int) PlacementSounds.CactusPlant];
        }
        else if (selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Island])
        {
            audioSource.clip = placeAudioClips[(int) PlacementSounds.IslandPlace];
        }
        else if (selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Algae])
        {
            audioSource.clip = placeAudioClips[(int) PlacementSounds.DigitalWater];
        }
        else if (selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.ConiferousBoulder] ||
                 selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.TopicalRock] ||
                 selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.DeciduousRock])
        {
            audioSource.clip = placeAudioClips[(int) PlacementSounds.RocksFalling];
        }
        else if (selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Flower1] || 
                 selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Flower2] ||
                 selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Grass] ||
                 selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Flower3])
        {
            audioSource.clip = placeAudioClips[(int) PlacementSounds.GrassGood];
        }
        else if (selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.ConiferousMountain] ||
                 selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.IceSheet] ||
                 selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Volcano] ||
                 selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.TundraIsland] ||
                 selectedTerrain == ts.terrainObjects[(int) TerrainSelect.TerrainNames.Iceberg])
        {
            audioSource.clip = placeAudioClips[(int) PlacementSounds.BasicThud];
        }
        else
        {
            audioSource.clip = placeAudioClips[(int) PlacementSounds.PouringSand];
        }
    }
}
