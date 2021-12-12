using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GlobalVars : ScriptableObject
{
    public float MainMenuSliderValue;
    public string PlayerName;

    //Introduction Text used in Assistant scene
    public string[] Introduction = new string[]
            {
                "Oho, a kogo to przywia³o?",
                "Nie wydaje mi siê ¿ebyœmy widzieli siê kiedykolwiek wczeœniej...",
                "Pozwól no ¿e Ci siê przedstawiê, nazywam siê Mao",
                "A jak Ciebie zw¹?"
            };
}
