using System.Linq;
using TMPro;
using UnityEngine;

public class MainHandler : MonoBehaviour
{
    public GameObject line;
    CreateMapData createMap;
    ShowPlanets showPlanets;
    public TooltipPopup popup;
    public GameObject daysCounter;
    public GlobalVars globalVars;
    private string playerName;
    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        createMap = gameObject.GetComponent<CreateMapData>();
        showPlanets = gameObject.GetComponent<ShowPlanets>();
        popup = FindObjectOfType<TooltipPopup>();
        
        if (GlobalData.firstStart)
        {
            GlobalData.playerPosition = "Pstart";
            GlobalData.firstStart = false;
            GlobalData.days = 0;
            GlobalData.resources = GlobalData.maxResources;
            createMap.Generate();
            Debug.Log("PLANETS SIZE = " + GlobalData.planets.Count);
        }

        string resourceText = GlobalData.resources + "/" + GlobalData.maxResources;
        UIResourceBar.Instance.SetText(resourceText);
        UIResourceBar.Instance.SetValue(GlobalData.resources / (float)GlobalData.maxResources);
        daysCounter.GetComponent<TextMeshProUGUI>().text = "Days: " + GlobalData.days;

        showPlanets.Show();
        popup.Deactivate();

        // GameObject.Find(mapData.playerPosition)
    }

    void Update()
    {
      
    }
    public void ChangePlayerPosition(string name)
    {
        GlobalData.playerPosition = name;
        if (name != "Pend")
        {
            char[] x = name.ToCharArray();
            //int row = x[1] - '0';
            int col = x[3] - '0';

            for (int i = 1; i <= 3; i++)
            {
                string previous = "P" + i + "R" + col;
                GameObject planet1 = GameObject.Find(previous);
                planet1.GetComponent<Planet>().setClickable(false);
            }

            if (col == 3)
            {
                GameObject planet = GameObject.Find("Pend");
                planet.GetComponent<Planet>().setClickable(true);
            }

            else
            {
                for (int i = 1; i <= 3; i++)
                {
                    string next = "P" + i + "R" + (col + 1);
                    GameObject planet1 = GameObject.Find(next);
                    planet1.GetComponent<Planet>().setClickable(true);
                }
            }
        }

        GlobalData.path.Add(name);
        GlobalData.planetChanged = true;
        SaveDifficultyChoiceToProcess(GameObject.Find(name).GetComponent<Planet>().getDifficulty());
    }

    private void SaveDifficultyChoiceToProcess(int choice)
    {
        GlobalData.SaveDifficultyChoice(choice);
    }
    
}
