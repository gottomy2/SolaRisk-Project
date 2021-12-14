using UnityEngine;

public class MainHandler : MonoBehaviour
{
    public MapData mapData;
    public GameObject line;
    CreateMapData createMap;
    ShowPlanets showPlanets;
    public TooltipPopup popup;

    private void Start()
    {
        createMap = gameObject.GetComponent<CreateMapData>();
        showPlanets = gameObject.GetComponent<ShowPlanets>();
        popup = FindObjectOfType<TooltipPopup>();

        if (mapData.firstStart)
        {
            mapData.playerPosition = "Pstart";
            mapData.firstStart = false;
            createMap.Generate(mapData);
        }
        showPlanets.Show(mapData);
        popup.Deactivate();

        // GameObject.Find(mapData.playerPosition)
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
    public void ChangePlayerPosition(string name)
    {
        mapData.playerPosition = name;
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
        mapData.path.Add(name);
    }
}
