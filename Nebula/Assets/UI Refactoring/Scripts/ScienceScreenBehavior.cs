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

    private bool isGasGiant;

    private GameObject planet;
    private GasGiantController gasGiantController;
    /// <summary>
    /// Gas Giant base planet color shader property
    /// </summary>
    private static readonly int PlanetColor = Shader.PropertyToID("_Planet_Color");
    /// <summary>
    /// Gas Giant bands color shader property
    /// </summary>
    private static readonly int ColorBands = Shader.PropertyToID("_Color_Bands");

    private FactTypes? fact1 = null;
    private FactTypes? fact2 = null;
    private FactTypes? fact3 = null;

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
    
    [Header("Fact Header text for Terrestial and Gas Giant")]
    [SerializeField] private string[] terrestrialHaders = new string[3];
    [SerializeField] private string[] gasGiantHeaders = new string[3];

    private void OnValidate()
    {
        terrestrialFactsCSV = Resources.Load<TextAsset>("CSVs/TerrestrialFactsFile");
        gasGiantFactsCSV = Resources.Load<TextAsset>("CSVs/GasGiantFactsFile");

        terrestrialFactRows = terrestrialFactsCSV.text.Split("\n"[0]); // Splits the CSV into an array of strings based on rows.
        gasGiantFactRows = gasGiantFactsCSV.text.Split("\n"[0]); // Splits the CSV into an array of strings based on rows.
    }

    private void Awake()
    {
        terrestrialFactsCSV = Resources.Load<TextAsset>("CSVs/TerrestrialFactsFile");
        gasGiantFactsCSV = Resources.Load<TextAsset>("CSVs/GasGiantFactsFile");

        terrestrialFactRows = terrestrialFactsCSV.text.Split("\n"[0]); // Splits the CSV into an array of strings based on rows.
        gasGiantFactRows = gasGiantFactsCSV.text.Split("\n"[0]); // Splits the CSV into an array of strings based on rows.
    }

    private void Start()
    {
        planet = GameObject.FindGameObjectWithTag("Planet");
        
        // selects which list of facts to use based on planet type
        if (planet.TryGetComponent<GasGiantController>(out gasGiantController))
        {
            isGasGiant = true;
            factRows = gasGiantFactRows;
        }
        else
        {
            isGasGiant = false;
            factRows = terrestrialFactRows;
        }
        
        DetermineFacts();
        InitalizeScienceScreen(PlanetNameGen(), fact1, fact2, fact3);
    }

    private void DetermineFacts()
    {
        if (isGasGiant)
        {
            // ref to Gas Giant shader
            Material planetMat = planet.GetComponent<MeshRenderer>().material;
            var baseColor = planetMat.GetColor(PlanetColor);
            var bandsColor = planetMat.GetColor(ColorBands);

            #region determine base color fact

            if (baseColor == gasGiantController.baseColors[0])
            {
                fact1 = FactTypes.Chlorine;
            }
            else if (baseColor == gasGiantController.baseColors[1])
            {
                fact1 = FactTypes.Oxygen;
            }
            else if (baseColor == gasGiantController.baseColors[2])
            {
                fact1 = FactTypes.Fluorine;
            }
            else if (baseColor == gasGiantController.baseColors[3])
            {
                fact1 = FactTypes.Potassium;
            }
            else if (baseColor == gasGiantController.baseColors[4])
            {
                fact1 = FactTypes.Strontium;
            }
            else if (baseColor == gasGiantController.baseColors[5])
            {
                fact1 = FactTypes.Rubidium;
            }
            else if (baseColor == gasGiantController.baseColors[6])
            {
                fact1 = FactTypes.Hydrogen;
            }
            else if (baseColor == gasGiantController.baseColors[7])
            {
                fact1 = FactTypes.Bromine;
            }

            #endregion
            
            #region determine bands color fact

            if (bandsColor == gasGiantController.bandColors[0])
            {
                fact2 = FactTypes.Barium;
            }
            else if (bandsColor == gasGiantController.bandColors[1])
            {
                fact2 = FactTypes.Oxygen;
            }
            else if (bandsColor == gasGiantController.bandColors[2])
            {
                fact2 = FactTypes.Sodium;
            }
            else if (bandsColor == gasGiantController.bandColors[3])
            {
                fact2 = FactTypes.Iodine;
            }
            else if (bandsColor == gasGiantController.bandColors[4])
            {
                fact2 = FactTypes.Lithium;
            }
            else if (bandsColor == gasGiantController.bandColors[5])
            {
                fact2 = FactTypes.Calcium;
            }
            else if (bandsColor == gasGiantController.bandColors[6])
            {
                fact2 = FactTypes.Magnesium;
            }
            else if (bandsColor == gasGiantController.bandColors[7])
            {
                fact2 = FactTypes.Nitrogen;
            }

            #endregion
            
            fact3 = FactTypes.Rings;
        }
        else
        {
            BiomePainter bp = FindObjectOfType<BiomePainter>();

            #region determine default biome

            if (bp.defaultPlanetBiome == BiomePainter.BiomeNames.Ocean.ToString())
            {
                fact1 = FactTypes.Oceania;
            }
            else if (bp.defaultPlanetBiome == BiomePainter.BiomeNames.Savana.ToString())
            {
                fact1 = FactTypes.Savanna;
            }
            else if (bp.defaultPlanetBiome == BiomePainter.BiomeNames.Ice.ToString())
            {
                fact1 = FactTypes.ArcticTundra;
            }
            else if (bp.defaultPlanetBiome == BiomePainter.BiomeNames.Plains.ToString())
            {
                fact1 = FactTypes.TemperateGrasslands;
            }
            else if (bp.defaultPlanetBiome == BiomePainter.BiomeNames.Tropical.ToString())
            {
                fact1 = FactTypes.TropicalDesert;
            }
            else if (bp.defaultPlanetBiome == BiomePainter.BiomeNames.Temperate.ToString())
            {
                fact1 = FactTypes.TemperateDesert;
            }
            else if (bp.defaultPlanetBiome == BiomePainter.BiomeNames.Coniferous.ToString())
            {
                fact1 = FactTypes.TaigaForest;
            }
            else if (bp.defaultPlanetBiome == BiomePainter.BiomeNames.Taiga.ToString()) // taiga is mislabeled, represents deciduous
            {
                fact1 = FactTypes.TemperateForest;
            }

            #endregion
            
            #region determine most used biome

            if (bp.mostUsedBiome == bp.defaultPlanetBiome)
            {
                fact2 = FactTypes.SingleBiome;
            }
            else if (bp.mostUsedBiome == BiomePainter.BiomeNames.Ocean.ToString())
            {
                fact2 = FactTypes.Oceania;
            }
            else if (bp.mostUsedBiome == BiomePainter.BiomeNames.Savana.ToString())
            {
                fact2 = FactTypes.Savanna;
            }
            else if (bp.mostUsedBiome == BiomePainter.BiomeNames.Ice.ToString())
            {
                fact2 = FactTypes.ArcticTundra;
            }
            else if (bp.mostUsedBiome == BiomePainter.BiomeNames.Plains.ToString())
            {
                fact2 = FactTypes.TemperateGrasslands;
            }
            else if (bp.mostUsedBiome == BiomePainter.BiomeNames.Tropical.ToString())
            {
                fact2 = FactTypes.TropicalDesert;
            }
            else if (bp.mostUsedBiome == BiomePainter.BiomeNames.Temperate.ToString())
            {
                fact2 = FactTypes.TemperateDesert;
            }
            else if (bp.mostUsedBiome == BiomePainter.BiomeNames.Coniferous.ToString())
            {
                fact2 = FactTypes.TemperateForest;
            }
            else if (bp.mostUsedBiome == BiomePainter.BiomeNames.Taiga.ToString())
            {
                fact2 = FactTypes.TaigaForest;
            }

            #endregion
            
            fact3 = FactTypes.Atmosphere;
        }
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
        string[] factRow2 = GetNewFactRow(fact2);
        string[] factRow3 = GetNewFactRow(fact3);
        
        if (isGasGiant)
        {
            factElements[0].UpdateHeaderText(gasGiantHeaders[0]);
            factElements[0].UpdateInfoText(factRow1);

            factElements[1].UpdateHeaderText(gasGiantHeaders[1]);
            factElements[1].UpdateInfoText(factRow2);

            factElements[2].UpdateHeaderText(gasGiantHeaders[2]);
            factElements[2].UpdateInfoText(factRow3);
        }
        else
        {
            factElements[0].UpdateHeaderText(terrestrialHaders[0]);
            factElements[0].UpdateInfoText(factRow1);

            factElements[1].UpdateHeaderText(terrestrialHaders[1]);
            factElements[1].UpdateInfoText(factRow2);

            factElements[2].UpdateHeaderText(terrestrialHaders[2]);
            factElements[2].UpdateInfoText(factRow3);
        }
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

        List<string[]> factTypeRows = new List<string[]>();
        List<int> factTypeRowIndexes = new List<int>();
        
        for (int rowIndex = 0; rowIndex < factRows.Length; rowIndex++)
        {
            string[] row = factRows[rowIndex].Replace("\"", string.Empty).Split(","[0]);    // Removes quotation marks and spilts the row based on the comma.

            if (factType.ToString() == row[0] && !rowsUsed.Contains(rowIndex))
            {
                factTypeRows.Add(row);
                factTypeRowIndexes.Add(rowIndex);
            }
        }

        if (factTypeRows.Count > 0)
        {
            // selects random fact from possible factType facts
            int randFact = Random.Range(0, factTypeRows.Count);
            rowsUsed.Add(factTypeRowIndexes[randFact]);
            return factTypeRows[randFact];
        }

        return new string[] { "FactType: " + factType , "Fact Not Found" };
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
        // terrestrial
        ArcticTundra,
        Oceania,
        Savanna,
        TaigaForest,
        TemperateDesert,
        TemperateForest,
        TemperateGrasslands,
        TropicalDesert,
        Atmosphere,
        SingleBiome,
        
        // gas giant
        Barium,
        Bromine,
        Calcium,
        Chlorine,
        Fluorine,
        Hydrogen,
        Iodine,
        Lithium,
        Magnesium,
        Nitrogen,
        Potassium,
        Rubidium,
        Sodium,
        Strontium,
        Oxygen,
        Rings
    }
}