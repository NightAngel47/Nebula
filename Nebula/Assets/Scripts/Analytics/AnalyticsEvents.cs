﻿using System;
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

    private BiomePainter bp;
    
    /// <summary>
    /// The starting biome for the terrestrial planet
    /// </summary>
    private string _terrestrialStartBiome;
    /// <summary>
    /// List of biome names from BiomeNames
    /// </summary>
    private List<string> _terrestiralBiomeNames = new List<string>();
    /// <summary>
    /// List of counts for each biome in BiomeNames
    /// </summary>
    private int[] _terrestrialBiomeCounts;
    
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
    
    /// <summary>
    /// The atmosphere color
    /// </summary>
    private static readonly int Color = Shader.PropertyToID("_color");
    
    #endregion

    #region Gas Giant Data Vars
    
    /// <summary>
    /// Gas Giant base planet color shader property
    /// </summary>
    private static readonly int PlanetColor = Shader.PropertyToID("_Planet_Color");
    /// <summary>
    /// Gas Giant bands color shader property
    /// </summary>
    private static readonly int ColorBands = Shader.PropertyToID("_Color_Bands");
    /// <summary>
    /// Gas Giant bands size shader property
    /// </summary>
    private static readonly int Bands = Shader.PropertyToID("_Bands");

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
        // sets feature 1 active at start because that is how the system works
        _activeStates[(int) ActiveStates.Feature1] = true;

        // Sets up analytics directory
        _dataPath = Application.persistentDataPath + "//Analytics//";
        if (!Directory.Exists(_dataPath))
            Directory.CreateDirectory(_dataPath);
        
        _thisScene = SceneManager.GetActiveScene();
        // Setup analytics per scene
        switch (_thisScene.name)
        {
            case "TerrestrialCreator":
                // Set data path to terrestrial analytics file
                _dataPath += "TerrestrialAnalytics.csv";
                
                bp = FindObjectOfType<BiomePainter>();
                _terrestrialTerrain = FindObjectOfType<TerrainSelect>().terrainObjects;
                SetupBiomeTracking();

                break;
            case "GasCreator":
                // Set data path to gas gaint analytics file
                _dataPath += "GasGiantAnalytics.csv";
                
                break;
            case "Complete Screen":
                // Set data path to complate screen analytics file
                _dataPath += "CompleteScreenAnalytics.csv";
                
                break;
            default:
                // default file
                _dataPath += "Analytics.csv";
                Debug.LogError("Analytics not setup for this scene.");
                
                break;
        }
    }
    
    void Update()
    {
        TrackGameSecondsElapsed();
        TrackTutorialSecondsElapsed();
        TrackFeaturesActiveSecondsElapsed();
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

    /// <summary>
    /// Tracks the seconds elapsed for the time that each feature is active
    /// </summary>
    private void TrackFeaturesActiveSecondsElapsed()
    {
        if (_activeStates[(int) ActiveStates.Feature1])
        { 
            _secondsElapsed[(int) SecondsElapsed.Feature1] += Time.deltaTime;
        }
        else if (_activeStates[(int) ActiveStates.Feature2])
        { 
            _secondsElapsed[(int) SecondsElapsed.Feature2] += Time.deltaTime;
        }
        else if (_activeStates[(int) ActiveStates.Feature3])
        { 
            _secondsElapsed[(int) SecondsElapsed.Feature3] += Time.deltaTime;
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
    /// Changes the states of the the features
    /// </summary>
    /// <param name="featureNum">The feature number that is being set to true</param>
    public void SetFeatureActive(int featureNum)
    {
        switch (featureNum)
        {
            case 1:
                ChangeFeatureStates(true, false, false);
                break;
            case 2:
                ChangeFeatureStates(false, true, false);
                break;
            case 3:
                ChangeFeatureStates(false, false, true);
                break;
            default:
                ChangeFeatureStates(false, false, false);
                break;
        }
    }

    /// <summary>
    /// Changes the states of the all the features
    /// </summary>
    /// <param name="state1">The state of Feature 1</param>
    /// <param name="state2">The state of Feature 2</param>
    /// <param name="state3">The state of Feature 3</param>
    private void ChangeFeatureStates(bool state1, bool state2, bool state3)
    {
        _activeStates[(int) ActiveStates.Feature1] = state1;
        _activeStates[(int) ActiveStates.Feature2] = state2;
        _activeStates[(int) ActiveStates.Feature3] = state3;
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
    /// Sets up biome tracking
    /// </summary>
    private void SetupBiomeTracking()
    {
        // Get biome names
        _terrestiralBiomeNames.AddRange(Enum.GetNames(typeof(BiomePainter.BiomeNames)));
        _terrestrialBiomeCounts = new int[_terrestiralBiomeNames.Count];
    }
    
    /// <summary>
    /// Sets the starting biome of the terrestrial planet
    /// </summary>
    /// <param name="biome">The biome name</param>
    public void SetStartingBiome(string biome)
    {
        _terrestrialStartBiome = biome;
    }
    
    /// <summary>
    /// Increases the biome count for the passed in biome
    /// </summary>
    /// ,<param name="biome">The name of the placed</param>
    public void IncreaseBiomeCount(string biome)
    {
        if (_terrestiralBiomeNames.Contains(biome))
        {
            _terrestrialBiomeCounts[_terrestiralBiomeNames.IndexOf(biome)]++;
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
                    if (terrain.transform.parent.name == feature.name + "(Clone)")
                    {
                        ++featureCount;
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
    /// Adds color data RGB for selected feature into _collectedData
    /// </summary>
    /// <param name="featureName">The name of the feature that the color is for</param>
    /// <param name="color">The color that is being outputted as a string</param>
    private void AddColorData(String featureName, Color color)
    {
        _collectedData.Add(featureName + " (R)", color.r);
        _collectedData.Add(featureName + " (G)", color.g);
        _collectedData.Add(featureName + " (B)", color.b);
    }
    
    /// <summary>
    /// Packages the collected data into the dictionary _collectedData
    /// </summary>
    void PackageData()
    {
        // Basic data
        _collectedData = new Dictionary<string, object>
        {
            {"Time", System.DateTime.Now},
            {"Played Seconds", _secondsElapsed[(int) SecondsElapsed.Game]}
        };
        
        // the current planet
        GameObject planet = GameObject.FindGameObjectWithTag("Planet");
        
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
                
                // add in biome usage
                int mostUsedBiomeCount = 0;
                foreach (string biomeName in _terrestiralBiomeNames)
                {
                    // checks most used
                    if (_terrestrialBiomeCounts[_terrestiralBiomeNames.IndexOf(biomeName)] > mostUsedBiomeCount)
                    {
                        mostUsedBiomeCount = _terrestrialBiomeCounts[_terrestiralBiomeNames.IndexOf(biomeName)];
                        bp.mostUsedBiome = biomeName;
                    }
                    
                    // adds data
                    _collectedData.Add(biomeName, _terrestrialBiomeCounts[_terrestiralBiomeNames.IndexOf(biomeName)]);
                }
                
                FinalTerrainCount();
                
                // adds terrain options to _collectedData
                for (int i = 0; i < _terrestrialTerrainOptionsCount.Length; ++i)
                {
                    _collectedData.Add(_terrestrialTerrainOptionsNames[i], _terrestrialTerrainOptionsCount[i]);
                }
                
                // adds ending atmosphere color to _collectedData
                AddColorData("Atmosphere Color", planet.GetComponentInChildren<AtmosphereController>().rend.material.GetColor(Color));
            }
            else // Gas Giant additional data
            {
                // ref to Gas Giant shader
                Material planetMat = planet.GetComponent<MeshRenderer>().material;
                
                //ref to Rings
                ParticleSystem rings = planet.GetComponentInChildren<ParticleSystem>();
                
                // adds primary color data
                AddColorData("Primary Color", planetMat.GetColor(PlanetColor));
                
                // adds bands data
                AddColorData("Bands Color", planetMat.GetColor(ColorBands));
                _collectedData.Add("Bands Size", planetMat.GetFloat(Bands));
                
                // adds rings data
                AddColorData("Rings Primary Color", rings.main.startColor.colorMin);
                AddColorData("Rings Secondary Color", rings.main.startColor.colorMax);
                _collectedData.Add("Rings Size", rings.shape.scale.x);
            }
        }
        else // additional data for the complete screen
        {
            // adds planet type
            _collectedData.Add("Planet Type", planet.GetComponent<GasGiantController>() ? "Gas Giant" : "Terrestrial");
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
                writer.Write(key.Value + "\n");
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
