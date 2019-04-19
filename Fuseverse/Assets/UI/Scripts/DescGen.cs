using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DescGen : MonoBehaviour
{

    public Text text;
    private string RName;
    int sizeOfList;

    void Start()
    {
        List<string> namegen = new List<string>();

        namegen.Add("Sirius");
        namegen.Add("Canopus");
        namegen.Add("Alpha Centauri");
        namegen.Add("Vega");
        namegen.Add("Arcturus");
        namegen.Add("Capella");
        namegen.Add("Rigel");
        namegen.Add("Procyon");
        namegen.Add("Archernar");
        namegen.Add("Beta Centauri");
        namegen.Add("Altair");
        namegen.Add("Betelgeuse");
        namegen.Add("Alderbaran");
        namegen.Add("Alpha Crucis");
        namegen.Add("Spica");
        namegen.Add("Pollux");
        namegen.Add("Antares");
        namegen.Add("Fomalhaut");
        namegen.Add("Deneb");
        namegen.Add("Beta Crucis");
        namegen.Add("Regulus");
        namegen.Add("Mira");
        namegen.Add("Alpha Herculis");
        namegen.Add("Algol");
        namegen.Add("Gamma Cephi");
        namegen.Add("Alpha Ursa Majoris");

        string randomListString = namegen[Random.Range(0, namegen.Count)];
        string randomNumber = (Random.Range(0, 10)).ToString();
        string RandomLetter = namegen[Random.Range(0, namegen.Count)];

        text.text = randomListString + " - " + randomNumber;
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
