using UnityEngine;
using TMPro;

public class FactElementBehavior : MonoBehaviour
{
    private TMP_Text headerText = null;
    private TMP_Text infoText = null;

    private void Awake()
    {
        headerText = transform.GetChild(0).GetComponent<TMP_Text>();
        infoText = transform.GetChild(1).GetComponent<TMP_Text>();
    }

    /// <summary> Updates the value of headerText.text to the value of the given string. </summary>
    /// <param name="text"> The value to set headerText.text to. </param>
    public void UpdateHeaderText(string text)
    {
        headerText.text = text;
    }

    /// <summary> Updates the value of infoText.text to the value of the given string. </summary>
    /// <param name="text"> The value to set infoText.text to. </param>
    public void UpdateInfoText(string text)
    {
        infoText.text = text;
    }
}