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
    public variable[] dialoguePath;
    public variable[] hubStats;
    public Vector3 playerPosition;

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

    public List<FlappyData> flappyDataList = new List<FlappyData>();

    public void SaveFlappyData(FlappyData data){
        flappyDataList.Add(data);
        Debug.Log("FlappyData saved to GlobalVars!\n" + data.GetWholeTime() + ", " + data.GetJumps() + ", " + data.GetHasFinished());
    }

    public List<SimonData> simonDataList = new List<SimonData>();

    public void SaveSimonData(SimonData data){
        simonDataList.Add(data);
        Debug.Log("SimonData saved to GlobalVars!\n" + data.GetOverallTime() + ", " + data.GetClicks() + ", " + data.GetHasFinished());
    }
    public void setDialoguePath(string key, bool value)
    {   
        
        for(int i = 0; i < this.dialoguePath.Length; i++)
        {
            if (this.dialoguePath[i].key == key)
            {
                this.dialoguePath[i].value = value; ;
            }
        }
    }
    public bool getDialoguePath(string key) {
        bool returned = false;
        for (int i = 0; i < this.dialoguePath.Length; i++)
        {
            if (this.dialoguePath[i].key == key)
            {
                returned = this.dialoguePath[i].value;
            }
        }
        return returned;
    }

    public bool getVar(string key, variable[] variables)
    {
        bool returned = false;
        for (int i = 0; i < variables.Length; i++)
        {
            if (variables[i].key == key)
            {
                returned = variables[i].value;
            }
        }
        return returned;
    }

    public void setVar(string key, bool value, variable[] variables)
    {
        for (int i = 0; i < variables.Length; i++)
        {
            if (variables[i].key == key)
            {
                variables[i].value = value; ;
            }
        }
    }
}
