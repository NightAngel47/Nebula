using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnalyticsEvents : MonoBehaviour
{
    #region Scene State Vars
    
    ///<summary>
    /// Possible states of the scene.
    /// </summary>
    public enum PlayState {InProgress, Completed, Quit}
    /// <summary>
    /// Tracks the current state of the scene
    /// </summary>
    private PlayState _state = PlayState.InProgress;
    /// <summary>
    /// The active scene
    /// </summary>
    private Scene _thisScene;
    
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
    /// Tracks the usage of each biome type for the terrestrial planet
    /// </summary>
    private int[] _terrestrialBiomesUsed = new int[8]; // 0 water, 1 plains, 2 sand, 3 snow, 4 forest, 5 artic, 6 badlands, 7 mountain

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
        _dataPath = Application.persistentDataPath + "Analytics.csv";
        _thisScene = SceneManager.GetActiveScene();
    }
    
    void Update()
    {
        TrackGameSecondsElapsed();
        TrackTutorialSecondsElapsed();
    }

    /// <summary>
    /// Changes the current play state to the new play state
    /// </summary>
    /// <param name="newState">The new state that play state will be set to</param>
    public void SetPlayState(PlayState newState){
        _state = newState;
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
    
    #endregion

    /// <summary>
    /// Increases the tutorial help button count when the help button is tapped
    /// </summary>
    public void IncrementTutorialHelpCount()
    {
        ++_tutorialHelpButtonCount;
    }

    /// <summary>
    /// Increased the placement count of the biome selected
    /// </summary>
    /// <param name="biomeSelected">The biome placed that's count will be increased'</param>
    public void IncreaseBiomeUsed(int biomeSelected)
    {
        ++_terrestrialBiomesUsed[biomeSelected];
    }

    /// <summary>
    /// Packages the collected data into the dictionary _collectedData
    /// </summary>
    void PackageData()
    {
        /*
         adds these custom params to analytics data:
         time
         platform
         scene name
         played seconds
         tutorial seconds
         tutorial help button count
         feature 1 seconds
         feature 2 seconds
         feature 3 seconds
        */
        _collectedData = new Dictionary<string, object>
        {
            {"UTC Time", System.DateTime.UtcNow},
            {"Platform", Application.platform},
            {"Scene Name", _thisScene.name},
            {"Played Seconds", _secondsElapsed[(int) SecondsElapsed.Game]},
            {"Tutorial Seconds", _secondsElapsed[(int) SecondsElapsed.Tutorial]},
            {"Tutorial Help Button Count", _tutorialHelpButtonCount},
            {"Feature 1 Seconds", _secondsElapsed[(int) SecondsElapsed.Feature1]},
            {"Feature 2 Seconds", _secondsElapsed[(int) SecondsElapsed.Feature2]},
            {"Feature 3 Seconds", _secondsElapsed[(int) SecondsElapsed.Feature3]}
        };
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

        Debug.Log("Analytics file saved here: " + _dataPath);
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
