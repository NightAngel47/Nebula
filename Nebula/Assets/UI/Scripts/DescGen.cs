using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DescGen : MonoBehaviour
{

    public Text text;
    private string RName;
    int sizeOfList;

    // description parts
    public string desLine1 = "Your planet is in a solar system populated with ";
    public string desLine2 = " other planets. The planet is exactly ";
    public string desLine3 = " times the size of earth. The average surface temperature of your planet is ";
    public string desLine4 = " degrees Fahrenheit. ";
    // line about moons goes here
    public string desLine5 = ". A single day takes ";
    public string desLine6 = " earth days on your planet and it takes ";
    public string desLine7 = " earth days to perform one revolution around it’s star.";

    // random numbers limits
    public int randNumPlanetsMin = 1;
    public int randNumPlanetsMax = 20;
    public int randEarthSizeMin = 1;
    public int randEarthSizeMax = 10;
    public int randTempMin = 32;
    public int randTempMax = 300;
    public float randDayMin = .25f;
    public float randDayMax = 30.0f;
    public float randRevMin = 30.0f;
    public float randRevMax = 999.9f;

    void Start()
    {
        List<string> Desc = new List<string>();

        //We need to rename all of these, there is no specific designation
        Desc.Add("Phobos");
        Desc.Add("Deimos");
        Desc.Add("Io");
        Desc.Add("Europa");
        Desc.Add("Callisto");
        Desc.Add("Ganymede");
        Desc.Add("Atlas");
        Desc.Add("Phoebe");
        Desc.Add("Janus");
        Desc.Add("Titania");
        Desc.Add("Oberon");
        Desc.Add("Triton");
        Desc.Add("Styx");
        Desc.Add("Hydra");
        Desc.Add("Nix");
        Desc.Add("Kerberos");
        Desc.Add("Charon");
        Desc.Add("Dysnomia");
        Desc.Add("Remus");
        Desc.Add("Linus");
        Desc.Add("Zoe");
        Desc.Add("Romulus");
        Desc.Add("Sawiskera");
        Desc.Add("Petit Prince");
        Desc.Add("Dactyl");
        Desc.Add("Hyperion");

        string randomListString = Desc[Random.Range(0, Desc.Count)];
        string randomListString2 = Desc[Random.Range(0, Desc.Count)];
        string randomListString3 = Desc[Random.Range(0, Desc.Count)];

        string randomNumberPlanets = Random.Range(randNumPlanetsMin, randNumPlanetsMax).ToString();
        string randomEarthSize = Random.Range(randEarthSizeMin, randEarthSizeMax).ToString();
        string randomTemp = Random.Range(randTempMin, randTempMax).ToString();
        string randomDay = Random.Range(randDayMin, randDayMax).ToString("F1");
        string randomRev = Random.Range(randRevMin, randRevMax).ToString("F1");
        int moonCount = Random.Range(1, 3);

        if (moonCount == 1)
        {
            text.text = desLine1 + randomNumberPlanets + 
                        desLine2 + randomEarthSize + 
                        desLine3 + randomTemp + 
                        desLine4 + " There is one moon that orbits your planet named " + randomListString + 
                        desLine5 + randomDay + 
                        desLine6 + randomRev +
                        desLine7;
        }

        if (moonCount == 2)
        {
            text.text = desLine1 + randomNumberPlanets +
                        desLine2 + randomEarthSize +
                        desLine3 + randomTemp +
                        desLine4 + "There are two moons that orbit your planet named " + randomListString + " and " + randomListString2 +
                        desLine5 + randomDay +
                        desLine6 + randomRev +
                        desLine7;

        }
        if (moonCount == 3)
        {
            text.text = desLine1 + randomNumberPlanets +
                        desLine2 + randomEarthSize +
                        desLine3 + randomTemp +
                        desLine4 + "There are three moons that orbit your planet named " + randomListString + ", " + randomListString2 + ", and " + randomListString3 + 
                        desLine5 + randomDay +
                        desLine6 + randomRev +
                        desLine7;
        }
    }

    /* Name List
 * Sirius
 * Canopus
 * Alpha Centauri
 * Arcturus
 * Vega
 * Capella
 * Regel
 * Procyon
 * Archernar
 * Beta Centauri
 * Altair
 * Betelgeuse
 * Aldebaran
 * Alpha Crucis
 * Spica
 * Antares
 * Pollux
 * Fomalhaut
 * Deneb
 * Beta Crucis
 * Regulus
 */
}
