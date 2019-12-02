using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScienceScreenBehavior))]
public class CustomInspectorScienceScreenBehavior : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ScienceScreenBehavior element = (ScienceScreenBehavior)target;
        if (element.TerrestrialFactsCSV != null)
        {
            bool isValidFileType = false;

            string path = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(element.TerrestrialFactsCSV.name)[0]);
            string[] split = path.Split("."[0]);
            foreach (string part in split)
            {
                if (part == "csv")
                    isValidFileType = true;
            }

            if (!isValidFileType)
            {
                Debug.LogWarning("You are attempting to add a non-CSV file to this field.\nYou may only add CSV files to this field.", element.TerrestrialFactsCSV);
                element.TerrestrialFactsCSV = null;
            }
        }
        
        if (element.GasGiantFactsCSV != null)
        {
            bool isValidFileType = false;

            string path = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(element.GasGiantFactsCSV.name)[0]);
            string[] split = path.Split("."[0]);
            foreach (string part in split)
            {
                if (part == "csv")
                    isValidFileType = true;
            }

            if (!isValidFileType)
            {
                Debug.LogWarning("You are attempting to add a non-CSV file to this field.\nYou may only add CSV files to this field.", element.GasGiantFactsCSV);
                element.GasGiantFactsCSV = null;
            }
        }
    }
}