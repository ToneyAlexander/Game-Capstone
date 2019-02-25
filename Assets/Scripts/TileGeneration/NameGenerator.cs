using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator
{
    private string[] infixes;
    private string[] suffixes;
    public NameGenerator()
    {
        TextAsset infixFile = Resources.Load<TextAsset>("infixes");
        TextAsset suffixFile = Resources.Load<TextAsset>("suffixes");

        infixes = infixFile.text.Split('\n');
        suffixes = suffixFile.text.Split('\n');
    }

    public string generateName()
    {
        string mid = "";
        for(int i = 0; i < Random.Range(1, 3); i++)
        {
            mid += infixes[Random.Range(0, infixes.Length)];
        }
        string end = suffixes[Random.Range(0, suffixes.Length)];

        string prefinal = mid + end;
        prefinal = prefinal.Substring(0,1).ToUpper() + prefinal.Substring(1);

        char tempC = '0';
        int count = 0;
        string final = "";
        foreach (char c in prefinal)
        {
            if (c == tempC)
                count++;
            else {
                tempC = c;
                count = 1;
            }

            if (count < 3) {
                final += c;
            }
        }
        if (final.Equals("Remy"))
        {
            Debug.Log("CONGRATS");
        }
        return final;
    }

}
