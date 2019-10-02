using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class AnalyticsEvents : MonoBehaviour
{
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
    
    /// <summary>
    /// The total time that the scene has been playing
    /// </summary>
    private float _gameSecondsElapsed = 0;
    /// <summary>
    /// The total time that the tutorial has been up for the current scene
    /// </summary>
    private float _tutorialSecondsElapsed = 0;
    /// <summary>
    /// Tracks the current state of the tutorial
    /// </summary>
    private bool _isTutorialActive = false;
    
    /// <summary>
    /// All collected data packaged to be saved
    /// </summary>
    private Dictionary<string, object> _collectedData;
    /// <summary>
    /// The file path where the analytics data is saved locally
    /// </summary>
    private string _dataPath;

    void Awake ()
    {
        _dataPath = Application.persistentDataPath + "Analytics.csv";
        _thisScene = SceneManager.GetActiveScene();
        AnalyticsEvent.LevelStart(_thisScene.name, _thisScene.buildIndex);
    }
    
    void Update(){
        _gameSecondsElapsed += Time.deltaTime;
        
        if (_isTutorialActive)
        {
            _tutorialSecondsElapsed += Time.deltaTime;
        }
    }
    
    /// <summary>
    /// Changes the current play state to the new play state
    /// </summary>
    /// <param name="newState">The new state that play state will be set to</param>
    public void SetPlayState(PlayState newState){
        _state = newState;
    }

    /// <summary>
    /// Changes the current state of tutorial active
    /// </summary>
    /// <param name="state">The new state that the tutorial active will be set to</param>
    public void SetTutorialActive(bool state)
    {
        _isTutorialActive = state;
    }

    /// <summary>
    /// Packages the collected data into the dictionary _collectedData
    /// </summary>
    void PackageData()
    {
        /*
         adds these custom params to analytics data:
         time
         scene name
         played seconds
         tutorial seconds
        */
        _collectedData = new Dictionary<string, object>
        {
            {"utc_time", System.DateTime.UtcNow},
            {"scene_name", _thisScene.name},
            {"played_seconds", _gameSecondsElapsed},
            {"tutorial_seconds", _tutorialSecondsElapsed}
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
            writer.WriteLine("UTC Time,Scene Name,Seconds Played,Tutorial Seconds");
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

    void OnDestroy()
    {
        // package data for analytics
        PackageData();
        
        // create local copy
        WriteFile();

        // different unity analytics exit events based on current game state
        switch (_state)
        {
            case PlayState.Completed:
                AnalyticsEvent.LevelComplete(_thisScene.name, _thisScene.buildIndex, _collectedData); 
                break;
            case PlayState.InProgress: 
            case PlayState.Quit:
            default:
                AnalyticsEvent.LevelQuit(_thisScene.name, _thisScene.buildIndex, _collectedData);
                break;
        }
    }
}
