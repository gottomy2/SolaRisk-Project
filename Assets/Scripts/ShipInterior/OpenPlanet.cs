using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class OpenPlanet : MonoBehaviour
{
    public GameObject text;
    public GlobalVars global;
    public float maxDistance = 4f;
    public PlanetData planetData;
    public MapData mapData;

    private bool inView = false;
    private GameObject player;
    private Vector3 playerPosition;
    private float distance;
    private bool condition;
    private string currentPlanetName;

    // Start is called before the first frame update
    void Start()
    {
        if (!global.getVar("minigameFailed", global.hubStats))
        {
            global.setVar("planetCanLand", true, global.hubStats);
        }
        else
        {
            global.setVar("planetVisited", false, global.hubStats);
        }

        player = GameObject.FindGameObjectWithTag("Player");
        condition = global.getVar("planetCanLand", global.hubStats) && !global.getVar("planetVisited", global.hubStats) && !isEmpty(mapData.path);
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        distance = Mathf.Sqrt(Mathf.Pow(playerPosition.x - gameObject.transform.position.x, 2) + Mathf.Pow(playerPosition.z - gameObject.transform.position.z, 2));

        if (condition)
        {
            if (Input.GetKeyDown(KeyCode.E) && inView && distance <= maxDistance)
            {
                //setting the planet to the correct type
                currentPlanetName = mapData.path[mapData.path.Count - 1];
                for (int i = 0; i < mapData.planets.Count; i++)
                {
                    if (mapData.planets[i].name == currentPlanetName)
                    {
                        planetData.name = mapData.planets[i].planetName;
                        planetData.type = mapData.planets[i].type;
                        break;
                    }
                }

                SceneManager.LoadScene("Assets/Scenes/PlanetaryView/SampleScene.unity");
                global.setVar("planetVisited", true, global.hubStats);
            }
            if (distance > maxDistance && text.activeSelf == true)
            {
                text.SetActive(false);
            }
            else if (distance <= maxDistance && text.activeSelf == false && inView)
            {
                text.SetActive(true);
            }
        }
    }

    private static bool isEmpty<T>(List<T> list)
    {
        if (list == null)
        {
            return true;
        }

        return !list.Any();
    }

    private void OnMouseEnter()
    {
        if (condition)
        {
            inView = true;
            text.SetActive(true);
        }  
    }
    private void OnMouseExit()
    {
        if (!condition)
        {
            inView = false;
            text.SetActive(false);
        }
    }
}
