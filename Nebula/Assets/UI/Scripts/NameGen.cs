using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NameGen : MonoBehaviour
{

    public Text text;
    private string RName;
    int sizeOfList;

    void Start()
    {
        List<string> namegen = new List<string>();

        namegen.Add("YZ Ceti");
        namegen.Add("Gliese 876");
        namegen.Add("82 G. Eridani");
        namegen.Add("Gliese 581");
        namegen.Add("HR 8832");
        namegen.Add("61 Virginis");
        namegen.Add("54 Piscium");
        namegen.Add("Rho Coronae Borealis");
        namegen.Add("55 Cancri");
        namegen.Add("HD 217107");
        namegen.Add("Pi Mensae  ");
        namegen.Add("23 Librae");
        namegen.Add("Kepler-444 ");
        namegen.Add("Kepler-42");
        namegen.Add("HR 8799");
        namegen.Add("Gamma Librae");
        namegen.Add("HIP 14810");
        namegen.Add("Kepler-445");
        namegen.Add("Kepler-90");
        namegen.Add("Kepler-11");
        namegen.Add("Regulus");
        namegen.Add("WASP-47");
        namegen.Add("Mu Arae");
        namegen.Add("Upsilon Andromedae");
        namegen.Add("HD 40307");
        namegen.Add("HIP 57274");
        namegen.Add("HD 141399");
        namegen.Add("HD 10180");
        namegen.Add("HR 8799");
        namegen.Add("HD 34445");
        namegen.Add("Kepler-37");
        namegen.Add("Kepler-100");
        namegen.Add("Kepler-92");
        namegen.Add("Kepler-11");
        namegen.Add("Kepler-9");
        namegen.Add("Kepler-9");
        namegen.Add("HD 27894");
        namegen.Add("Kepler-1047");
        namegen.Add("HD 125612");
        namegen.Add("PSR B1257+12");
        namegen.Add("Kelper-289");
        namegen.Add("Kepler-1254");

        string[] Alphabet = new string[14] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N" };

        string randomListString = namegen[Random.Range(0, namegen.Count)];
        string randomNumber = (Random.Range(0, 99)).ToString();
        string RandomLetter = Alphabet[Random.Range(0, Alphabet.Length)];

        text.text = randomListString + " - " + RandomLetter;
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
