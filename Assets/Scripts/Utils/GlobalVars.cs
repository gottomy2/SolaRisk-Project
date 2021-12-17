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
        }
    };

    public bool inMap = false;

    public FlappyData flappyData;

    public void SaveFlappyData(FlappyData data){
        this.flappyData = data;
        Debug.Log("FlappyData saved to GlobalVars!\n" + data.GetWholeTime() + ", " + data.GetJumps() + ", " + data.GetHasFinished());
    }

    public SimonData simonData;

    public void SaveSimonData(SimonData data){
        this.simonData = data;
        Debug.Log("SimonData saved to GlobalVars!\n" + data.GetOverallTime() + ", " + data.GetClicks() + ", " + data.GetHasFinished());
    }

}
