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
        },
    };

    public List<int> difficultyChoicesList = new List<int>();
    public List<bool> visitedChoicesList = new List<bool>();

    public void SaveDifficultyChoice(int choice)
    {
        difficultyChoicesList.Add(choice);
        Debug.Log("Saved choice: " + choice);
    }

    public void SaveVisitChoice(bool isVisited)
    {
        visitedChoicesList.Add(isVisited);
        Debug.Log("Saved planet visit" + isVisited);
    }
    
    public List<IData> dataList = new List<IData>();

    public void SaveData(IData data){
        dataList.Add(data);
        Debug.Log("Data saved to GlobalVars!\n" + data.GetWholeTime() + ", " + data.GetClicks() + ", " + data.GetHasFinished());
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
        Debug.Log("Key: " + key + ", value:" + value);
        for (int i = 0; i < variables.Length; i++)
        {
            if (variables[i].key == key)
            {
                Debug.Log("Setting value to variables[i].key= " + variables[i].key);
                variables[i].value = value; ;
            }
        }
        
    }
}
