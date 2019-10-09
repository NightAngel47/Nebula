using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnalyticsEvents : MonoBehaviour
{
    #region Scene State Vars
    
    /// <summary>
    /// The active scene
    /// </summary>
    private Scene _thisScene;

    private Scene _scene;
    
    #endregion

    #region Basic Planet Data Vars
    
    /// <summary>
    /// The different tracked seconds elapsed that are held in the _secondsElapsed array
    /// </summary>
    private enum SecondsElapsed {Game, Tutorial, Feature1, Feature2, Feature3}
    /// <summary>
    /// Tracks the seconds elapsed for different aspects of the scene.
    /// Game seconds, tutorial seconds, planet feature 1, planet feature 2, planet feature 3.
    /// </summary>
    private float[] _secondsElapsed = new float[5];
    
    /// <summary>
    /// The different tracked active states for the _activeStates array
    /// </summary>
    private enum ActiveStates {Tutorial, Feature1, Feature2, Feature3}
    /// <summary>
    /// Tracks the seconds elapsed for different aspects of the scene.
    /// Tutorial state, Feature 1 state, Feature 2 state, Feature 3 state
    /// </summary>
    private bool[] _activeStates = new bool[4];
    
    /// <summary>
    /// Tracks the amount of times that the help button was tapped
    /// </summary>
    private float _tutorialHelpButtonCount = 0;
    
    #endregion

    #region Terrestrial Data Vars

    /// <summary>
    /// The starting biome for the terrestrial planet
    /// </summary>
    private string _terrestrialStartBiome;
    
    /// <summary>
    /// List of Terrestrial Biomes
    /// </summary>
    private GameObject[] _terrestrialBiomes;
    
    /// <summary>
    /// List of Terrestrial Terrain Objects
    /// </summary>
    private GameObject[] _terrestrialTerrain; 
    
    /// <summary>
    /// The names of the terrain options
    /// </summary>
    private readonly string[] _terrestrialTerrainOptionsNames = {"Terrain Up", "Terrain Tree", "Terrain Erase"};
    /// <summary>
    /// The count of usage of the terrain features. 0 terrain up, 1 terrain tree, 2 terrain erase
    /// </summary>
    private readonly int[] _terrestrialTerrainOptionsCount = new int [3];
    
    private static readonly int Color = Shader.PropertyToID("_color");
    
    #endregion

    #region Gas Giant Data Vars

    

    #endregion

    #region Data Saving Vars
    
    /// <summary>
    /// All collected data packaged to be saved
    /// </summary>
    private Dictionary<string, object> _collectedData;

    /// <summary>
    /// The file path where the analytics data is saved locally
    /// </summary>
    private string _dataPath;

    #endregion

    void Awake ()
    {
        _thisScene = SceneManager.GetActiveScene();

        // Setup analytics per scene
        switch (_thisScene.name)
        {
            case "TerrestrialCreator":
                // Set data path to terrestrial analytics file
                _dataPath = Application.persistentDataPath + "//Analytics//TerrestrialAnalytics.csv";
                
                // Get biome names
                _terrestrialBiomes = FindObjectOfType<TerrestrialPainter>().biomes;
                
                // Get terrain names
                _terrestrialTerrain = FindObjectOfType<TerrainFeatures>().terrainObjects;

                break;
            case "GasCreator":
                // Set data path to gas gaint analytics file
                _dataPath = Application.persistentDataPath + "//Analytics//GasGiantAnalytics.csv";
                
                break;
            case "Complete Screen":
                // Set data path to complate screen analytics file
                _dataPath = Application.persistentDataPath + "//Analytics//TerrestrialAnalytics.csv";
                
                break;
            default:
                // default file
                _dataPath = Application.persistentDataPath + "//Analytics//Analytics.csv";
                Debug.LogError("Analytics not setup for this scene.");
                
                break;
        }
    }
    
    void Update()
    {
        TrackGameSecondsElapsed();
        TrackTutorialSecondsElapsed();
    }

    #region Seconds Elapsed Functions
    
    /// <summary>
    /// Tracks the seconds elapsed for the time that the current planet is played
    /// </summary>
    private void TrackGameSecondsElapsed()
    {
        _secondsElapsed[(int) SecondsElapsed.Game] += Time.deltaTime;
    }
    
    /// <summary>
    /// Tracks the seconds elapsed for the time that the tutorial is up
    /// </summary>
    private void TrackTutorialSecondsElapsed()
    {
        if (_activeStates[(int) ActiveStates.Tutorial])
        { 
            _secondsElapsed[(int) SecondsElapsed.Tutorial] += Time.deltaTime;
        }
    }
    
    #endregion

    #region Active State Functions
    
    /// <summary>
    /// Changes the current state of the tutorial
    /// </summary>
    /// <param name="state">The new state that the tutorial will be set to</param>
    public void SetTutorialActive(bool state)
    {
        _activeStates[(int) ActiveStates.Tutorial] = state;
    }
    
    /// <summary>
    /// Changes the current state of feature 1
    /// </summary>
    /// <param name="state">The new state that feature 1 will be set to</param>
    public void SetFeature1Active(bool state)
    {
        _activeStates[(int) ActiveStates.Feature1] = state;
    }
    
    /// <summary>
    /// Changes the current state of feature 2
    /// </summary>
    /// <param name="state">The new state that feature 2 will be set to</param>
    public void SetFeature2Active(bool state)
    {
        _activeStates[(int) ActiveStates.Feature2] = state;
    }
    
    /// <summary>
    /// Changes the current state of feature 3
    /// </summary>
    /// <param name="state">The new state that feature 3 will be set to</param>
    public void SetFeature3Active(bool state)
    {
        _activeStates[(int) ActiveStates.Feature3] = state;
    }

    /// <summary>
    /// Sets the active states of the 3 planet features.
    /// </summary>
    /// <param name="feature1">State for Feature 1</param>
    /// <param name="feature2">State for Feature 2</param>
    /// <param name="feature3">State for Feature 3</param>
    public void SetFeatureActiveStates(bool feature1, bool feature2, bool feature3)
    {
        _activeStates[(int) ActiveStates.Feature1] = feature1;
        _activeStates[(int) ActiveStates.Feature2] = feature2;
        _activeStates[(int) ActiveStates.Feature3] = feature3;
    }
    
    #endregion

    /// <summary>
    /// Increases the tutorial help button count when the help button is tapped
    /// </summary>
    public void IncrementTutorialHelpCount()
    {
        ++_tutorialHelpButtonCount;
    }

    #region Terrestrial Tracking Functions

    /// <summary>
    /// Sets the starting biome of the terrestrial planet
    /// </summary>
    /// <param name="biome">The biome name</param>
    public void SetStartingBiome(string biome)
    {
        _terrestrialStartBiome = biome;
    }
    
    /// <summary>
    /// Collects the amount that each biome was placed
    /// </summary>
    private void FinalBiomeCount()
    {
        foreach (var biome in _terrestrialBiomes)
        {
            GameObject[] biomeType = GameObject.FindGameObjectsWithTag(biome.tag);
            if (biomeType.Length > 0)
            {
                _collectedData.Add(biome.tag, biomeType.Length);
            }
            else
            {
                _collectedData.Add(biome.tag, 0);
            }
        }
    }
    
    /// <summary>
    /// Collects the final amount of each terrain feature
    /// </summary>
    private void FinalTerrainCount()
    {
        List<GameObject> terrainFeatures = GameObject.FindGameObjectsWithTag("Terrain").ToList();
        
        foreach (var feature in _terrestrialTerrain)
        {
            int featureCount = 0;
            if (terrainFeatures.Count > 0)
            {
                foreach (var terrain in terrainFeatures)
                {
                    if (terrain.name.Equals(feature.name))
                    {
                        ++featureCount;
                        terrainFeatures.Remove(terrain);
                    }
                }
            }
            _collectedData.Add(feature.name, featureCount);
        }
    }
    
    /// <summary>
    /// Increased the usage count of terrain options
    /// </summary>
    /// <param name="terrainOptions">The terrain option selected.'</param>
    public void IncreaseTerrainOptionsUsed(int terrainOptions)
    {
        ++_terrestrialTerrainOptionsCount[terrainOptions];
    }

    #endregion

    /// <summary>
    /// Packages the collected data into the dictionary _collectedData
    /// </summary>
    void PackageData()
    {
        // Basic data (also, the only data for the complete screen)
        _collectedData = new Dictionary<string, object>
        {
            {"Time", System.DateTime.Now},
            {"Played Seconds", _secondsElapsed[(int) SecondsElapsed.Game]}
        };
            
        // package data for planets
        if (!_thisScene.name.Equals("Complete Screen"))
        {
            // General planet data
            _collectedData.Add("Tutorial Seconds", _secondsElapsed[(int) SecondsElapsed.Tutorial]);
            _collectedData.Add("Tutorial Help Button Count", _tutorialHelpButtonCount);
            _collectedData.Add("Feature 1 Seconds", _secondsElapsed[(int) SecondsElapsed.Feature1]);
            _collectedData.Add("Feature 2 Seconds", _secondsElapsed[(int) SecondsElapsed.Feature2]);
            _collectedData.Add("Feature 3 Seconds", _secondsElapsed[(int) SecondsElapsed.Feature3]);
            
            // Terrestrial planet additional data
            if (_thisScene.name.Equals("TerrestrialCreator"))
            {
                // adds starting biome
                _collectedData.Add("Starting Biome", _terrestrialStartBiome);
                
                FinalBiomeCount();
                FinalTerrainCount();
                
                // adds terrain options to _collectedData
                for (int i = 0; i < _terrestrialTerrainOptionsCount.Length; ++i)
                {
                    _collectedData.Add(_terrestrialTerrainOptionsNames[i], _terrestrialTerrainOptionsCount[i]);
                }
                
                // adds ending atmosphere color to _collectedData
                _collectedData.Add("Atmosphere Color", GameObject.FindGameObjectWithTag("Planet").GetComponentInChildren<AtmosphereController>().rend.material.GetColor(Color).ToString());
            }
            else // Gas Giant additional data
            {
                
            }
        }
    }

    /// <summary>
    /// Writes a local copy of the dictionary _collected data
    /// </summary>
    void WriteFile()
    {
        // create path and categories else append
        StreamWriter writer;
        if (!File.Exists(_dataPath))
        {
            writer = new StreamWriter(_dataPath, false);
            foreach (var key in _collectedData)
            {
                if (!key.Equals(_collectedData.Last()))
                {
                    writer.Write(key.Key + ",");
                }
                else
                {
                    writer.Write(key.Key + "\n");
                }
            }
        }
        else
        {
            writer = new StreamWriter(_dataPath, true);
        }
        
        // write data
        foreach (var key in _collectedData)
        {
            if (!key.Equals(_collectedData.Last()))
            {
                writer.Write(key.Value + ",");
            }
            else
            {
                writer.Write(key.Value);
            }
        }

        writer.Close();

        Debug.Log("<color=orange>Analytics file saved here: </color>" + _dataPath);
    }

    // Packages, writes local, then sends unity analytics event
    void OnDestroy()
    {
        // package data for analytics
        PackageData();
        
        // create local copy
        WriteFile();
    }
}
