using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class ScienceScreenBehavior : MonoBehaviour
{
    [SerializeField] private TextAsset terrestrialFactsCSV, gasGiantFactsCSV;
    public TextAsset TerrestrialFactsCSV { get => terrestrialFactsCSV; set => terrestrialFactsCSV = value; }
    public TextAsset GasGiantFactsCSV { get => gasGiantFactsCSV; set => gasGiantFactsCSV = value; }

    private string[] terrestrialFactRows = new string[0];
    private string[] gasGiantFactRows = new string[0];
    
    private string[] factRows = new string[0];
    private List<int> rowsUsed = new List<int>();

    [SerializeField] private string[] planetBaseNames =
    {
        "YZ Ceti",
        "Gliese 876",
        "82 G. Eridani",
        "Gliese 581",
        "HR 8832",
        "61 Virginis",
        "54 Piscium",
        "Rho Coronae Borealis",
        "55 Cancri",
        "HD 217107",
        "Pi Mensae",
        "23 Librae",
        "Kepler-444 ",
        "Kepler-42",
        "HR 8799",
        "Gamma Librae",
        "HIP 14810",
        "Kepler-445",
        "Kepler-90",
        "Kepler-11",
        "Regulus",
        "WASP-47",
        "Mu Arae",
        "Upsilon Andromedae",
        "HD 40307",
        "HIP 57274",
        "HD 141399",
        "HD 10180",
        "HR 8799",
        "HD 34445",
        "Kepler-37",
        "Kepler-100",
        "Kepler-92",
        "Kepler-11",
        "Kepler-9",
        "Kepler-9",
        "HD 27894",
        "Kepler-1047",
        "HD 125612",
        "PSR B1257+12",
        "Kelper-289",
        "Kepler-1254"
    };
    string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N" };

    [Header("Text Objects")]
    [SerializeField] private TMP_Text planetNameText = null;
    [SerializeField] private FactElementBehavior[] factElements = null;

    private void OnValidate()
    {
        terrestrialFactRows = terrestrialFactsCSV.text.Split("\n"[0]); // Splits the CSV into an array of strings based on rows.
        gasGiantFactRows = gasGiantFactsCSV.text.Split("\n"[0]); // Splits the CSV into an array of strings based on rows.
    }

    private void Awake()
    {
        // selects which list of facts to use based on planet type
        factRows = FindObjectOfType<GasGiantController>() ? gasGiantFactRows : terrestrialFactRows;
    }

    private void Start()
    {
        InitalizeScienceScreen(PlanetNameGen());
    }

    /// <summary> Initalizes the values within the ScienceScreen. </summary>
    /// <param name="planetName"> Value to set planetNameText.text to. </param>
    /// <param name="fact1"> Type of fact for the first fact. </param>
    /// <param name="fact2"> Type of fact for the second fact. </param>
    /// <param name="fact3"> Type of fact for the third fact. </param>
    private void InitalizeScienceScreen(string planetName, FactTypes? fact1 = null, FactTypes? fact2 = null, FactTypes? fact3 = null)
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
    
    /// <summary>
    /// Generates a random planet name
    /// </summary>
    /// <returns>Generated planet name</returns>
    private string PlanetNameGen()
    {
        return planetBaseNames[Random.Range(0, planetBaseNames.Length)] + " - " + alphabet[Random.Range(0, alphabet.Length)];
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