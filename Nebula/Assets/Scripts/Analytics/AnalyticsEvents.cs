using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class AnalyticsEvents : MonoBehaviour
{
    public enum PlayState {InProgress, Completed, Quit} // play state

    private PlayState _state = PlayState.InProgress; // tracks play state
    private Scene _thisScene; // active scene
    private float _secondsElapsed = 0; // time played
    
    // file saving data
    private string _dataPath;

    void Awake ()
    {
        _dataPath = Application.persistentDataPath + "Analytics.txt";
        _thisScene = SceneManager.GetActiveScene();
        AnalyticsEvent.LevelStart(_thisScene.name, _thisScene.buildIndex);
        WriteFile();
    }
    
    void Update(){
        _secondsElapsed += Time.deltaTime;
    }
    
    public void SetPlayState(PlayState newState){
        _state = newState;
    }

    void WriteFile()
    {
        StreamWriter writer = new StreamWriter(_dataPath, true);

        string analyticsData = "Scene name: " + _thisScene.name;
        
        writer.WriteLine(analyticsData);
        Debug.Log("Analytics file saved here: " + _dataPath);
        writer.Close();
    }

    void OnDestroy()
    {
        // adds these custom params to analytics data:
        // seconds played
        Dictionary<string, object> customParams = new Dictionary<string, object> {{"seconds_played", _secondsElapsed}};

        switch (_state)
        {
            case PlayState.Completed:
                AnalyticsEvent.LevelComplete(_thisScene.name, _thisScene.buildIndex, customParams); 
                break;
            case PlayState.InProgress: 
            case PlayState.Quit:
            default:
                AnalyticsEvent.LevelQuit(_thisScene.name, _thisScene.buildIndex, customParams);
                break;
        }
    }
}
