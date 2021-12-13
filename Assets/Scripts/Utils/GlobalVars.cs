using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GlobalVars : ScriptableObject
{
    public float MainMenuSliderValue;
    public string PlayerName = "";

    public Dictionary<int, string[]> dictionary = new Dictionary<int, string[]>()
    {
        {0, new string[] {
                "Oho, a kogo to przywia�o?",
                "Nie wydaje mi si� �eby�my widzieli si� kiedykolwiek wcze�niej...",
                "Pewnie jeste� tym rekrutem o kt�rym tyle s�ysza�em!",
                "Pozw�l no �e Ci si� przedstawi�, nazywam si� Mao",
                "A jak Ciebie zw�?"
            }
        },
    };
}
