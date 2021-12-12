using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHandler : MonoBehaviour
{
    private string playerPosition = "Pstart";
    public GameObject line;
    private SceneSwitch sceneSwitch = new SceneSwitch();

    // Update is called once per frame
    void Update()
    {
        if (playerPosition == "Pend")
        {
            sceneSwitch.NextScene();
        }
        else
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
    }
    public void ChangePlayerPosition(string name)
    {
        playerPosition = name;
        if (name != "Pend")
        {
            char[] x = name.ToCharArray();
            //int row = x[1] - '0';
            int col = x[3] - '0';

            for (int i = 0; i < 3; i++)
            {
                string previous = "P" + i + "R" + col;
                GameObject planet1 = GameObject.Find(previous);
                planet1.GetComponent<Rotatator>().setClickable(false);
            }

            if (col == 3)
            {
                GameObject planet = GameObject.Find("Pend");
                planet.GetComponent<Rotatator>().setClickable(true);
            }

            else
            {
                for (int i = 0; i < 3; i++)
                {
                    string next = "P" + i + "R" + (col + 1);
                    GameObject planet1 = GameObject.Find(next);
                    planet1.GetComponent<Rotatator>().setClickable(true);
                }
            }
        }
    }
    public string GetPlayerPosition()
    {
        return playerPosition;
    }
}
