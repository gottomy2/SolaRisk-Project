using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct variable
{
    [SerializeField]
    public string key;
    [SerializeField]
    public bool value;

    public variable(string key, bool value)
    {
        this.key = key;
        this.value = value;
    }
}

[CreateAssetMenu()]
public class GlobalVars : ScriptableObject
{
    public float MainMenuSliderValue;
    public string PlayerName = "";
    [SerializeField]
    public variable[] testDialoguePath;

    
    public Dictionary<string, bool> dialoguePath = new Dictionary<string, bool>()
    {
        {"intro1",false},
        {"intro2",false},
        {"hubTutorial1",false},
        {"mapTutorial1",false},
        {"mapTutorial2",false},
        {"mapTutorialFinished",false},
        {"mapAssistantActive",true},
        {"hubTutorial2",false}
    };

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
        {2, new string[]{
                "Witaj na statku kapitanie!",
                "Zanim zajmiemy przejdziemy do podr�y pozw�l �e Ci� oprowadz�!",
                "Zacznijmy od g��wnego panelu!, to jest od mapy!"
            } 
        },
    };

    public bool inMap = false;

    public List<IData> dataList = new List<IData>();

    public void SaveData(IData data){
        dataList.Add(data);
        Debug.Log("Data saved to GlobalVars!\n" + data.GetWholeTime() + ", " + data.GetClicks() + ", " + data.GetHasFinished());
    }
    public void setDialoguePath(string key, bool value)
    {   
        
        for(int i = 0; i < this.testDialoguePath.Length; i++)
        {
            if (this.testDialoguePath[i].key == key)
            {
                this.testDialoguePath[i].value = value; ;
            }
        }
    }
    public bool getDialoguePath(string key) {
        bool returned = false;
        for (int i = 0; i < this.testDialoguePath.Length; i++)
        {
            if (this.testDialoguePath[i].key == key)
            {
                returned = this.testDialoguePath[i].value;
            }
        }
        return returned;
    }
}
