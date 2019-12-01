using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScienceScreenBehavior : MonoBehaviour
{
    [SerializeField] private TextAsset factCSV = null;
    public TextAsset FactCSV { get => factCSV; set => factCSV = value; }

    private string[] factRows = new string[0];
    private List<int> rowsUsed = new List<int>();

    [Header("Text Objects")]
    [SerializeField] private TMP_Text planetNameText = null;
    [SerializeField] private FactElementBehavior[] factElements = null;

    private void OnValidate()
    {
        factRows = factCSV.text.Split("\n"[0]); // Splits the CSV into an array of strings based on rows.
    }

    private void Start()
    {
        InitalizeScienceScreen("Test Planet 1 2 3");
    }

    /// <summary> Initalizes the values within the ScienceScreen. </summary>
    /// <param name="planetName"> Value to set planetNameText.text to. </param>
    /// <param name="fact1"> Type of fact for the first fact. </param>
    /// <param name="fact2"> Type of fact for the second fact. </param>
    /// <param name="fact3"> Type of fact for the third fact. </param>
    public void InitalizeScienceScreen(string planetName, FactTypes? fact1 = null, FactTypes? fact2 = null, FactTypes? fact3 = null)
    {
        rowsUsed = new List<int>();

        UpdatePlanetNameText(planetName);

        string[] factRow1 = GetNewFactRow(fact1);
        factElements[0].UpdateHeaderText(factRow1[0]);  // Currently this just sets the header to the FactType.
        factElements[0].UpdateInfoText(factRow1[1]);

        string[] factRow2 = GetNewFactRow(fact2);
        factElements[1].UpdateHeaderText(factRow2[0]);  // Currently this just sets the header to the FactType.
        factElements[1].UpdateInfoText(factRow2[1]);

        string[] factRow3 = GetNewFactRow(fact3);
        factElements[2].UpdateHeaderText(factRow3[0]);  // Currently this just sets the header to the FactType.
        factElements[2].UpdateInfoText(factRow3[1]);
    }

    /// <summary> Updates the value of planetNameText.text to the value of the given string. </summary>
    /// <param name="text"> The value to set planetNameText.text to. </param>
    private void UpdatePlanetNameText(string text)
    {
        planetNameText.text = text;
    }

    /// <summary> Gets row as a string[] based on a given FactType. </summary>
    /// <param name="factType"> The type of fact to be searched for. </param>
    /// <returns> Returns a string[] with the FactType at index 0 and the fact at index 1. </returns>
    private string[] GetNewFactRow(FactTypes? factType = null)
    {
        if (factType == null)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, factRows.Length);
            } while (rowsUsed.Contains(randomIndex));
            rowsUsed.Add(randomIndex);

            return factRows[randomIndex].Replace("\"", string.Empty).Split(","[0]);
        }

        for (int rowIndex = 0; rowIndex < factRows.Length; rowIndex++)
        {
            string[] row = factRows[rowIndex].Replace("\"", string.Empty).Split(","[0]);    // Removes quotation marks and spilts the row based on the comma.

            if (factType.ToString() == row[0] && !rowsUsed.Contains(rowIndex))
            {
                rowsUsed.Add(rowIndex);
                return row;
            }
        }

        return new string[] { "FactType Not Found" , "Fact Not Found" };
    }

    public enum FactTypes
    {
        ArcticTundra,
        Oceania,
        Savanna,
        TaigaForest,
        TemperateDesert,
        TemperateForest,
        TemperateGrasslands,
        TropicalDesert,
        Terrestrial_Atmospheric,
        Terrestrial_Other,
        Barium,
        Bromine,
        Calcium,
        Chlorine,
        Fluorine,
        Hydrogen,
        Iodine,
        Lithium,
        Magnesium,
        NitrogenDioxide,
        Potassium,
        Rubidium,
        Sodium,
        Strontium,
        Gas_Color,
        Gas_Rings,
        Gas_Other,
    }
}