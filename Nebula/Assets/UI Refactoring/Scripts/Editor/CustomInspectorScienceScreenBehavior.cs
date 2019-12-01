using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScienceScreenBehavior))]
public class CustomInspectorScienceScreenBehavior : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ScienceScreenBehavior element = (ScienceScreenBehavior)target;
        if (element.FactCSV != null)
        {
            bool isValidFileType = false;

            string path = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(element.FactCSV.name)[0]);
            string[] split = path.Split("."[0]);
            foreach (string part in split)
            {
                if (part == "csv")
                    isValidFileType = true;
            }

            if (!isValidFileType)
            {
                Debug.LogWarning("You are attempting to add a non-CSV file to this field.\nYou may only add CSV files to this field.", element.FactCSV);
                element.FactCSV = null;
            }
        }
    }
}