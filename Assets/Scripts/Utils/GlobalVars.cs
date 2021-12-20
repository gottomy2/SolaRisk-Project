using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GlobalVars : ScriptableObject
{
    public float MainMenuSliderValue;
    public string PlayerName = "";
    public Dictionary<string, bool> dialoguePath = new Dictionary<string, bool>()
    {
        {"intro1",false},
        {"intro2",false},
        {"hubTutorial1",true},
        {"hubTutorial2",false}
    };

    public Dictionary<int, string[]> dictionary = new Dictionary<int, string[]>()
    {
        {0, new string[] {
                "Oho, a kogo to przywia³o?",
                "Nie wydaje mi siê ¿ebyœmy widzieli siê kiedykolwiek wczeœniej...",
                "Pewnie jesteœ tym rekrutem o którym tyle s³ysza³em!",
                "Pozwól no ¿e Ci siê przedstawiê, nazywam siê Mao",
                "A jak Ciebie zw¹?"
            }
        },
        {2, new string[]{
                "Witaj na statku kapitanie!",
                "Zanim zajmiemy przejdziemy do podró¿y pozwól ¿e Ciê oprowadzê!",
                "Zacznijmy od g³ównego panelu!, to jest od mapy!"
            } 
        }
    };

    public bool inMap = false;

    public List<IData> dataList = new List<IData>();

    public void SaveData(IData data){
        dataList.Add(data);
        Debug.Log("Data saved to GlobalVars!\n" + data.GetWholeTime() + ", " + data.GetClicks() + ", " + data.GetHasFinished());
    }

}
