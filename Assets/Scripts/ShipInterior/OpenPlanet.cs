using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPlanet : MonoBehaviour
{
    public GameObject text;
    public GlobalVars global;
    public float maxDistance = 4f;
    public PlanetData planetData;
    public MapData mapData;

    private SceneSwitch sceneSwitch;
    private bool inView = false;
    private GameObject player;
    private Vector3 playerPosition;
    private float distance;
    private bool condition;
    private string currentPlanetName;

    // Start is called before the first frame update
    void Start()
    {
        sceneSwitch = new SceneSwitch();
        player = GameObject.FindGameObjectWithTag("Player");
        condition = global.getVar("planetCanLand", global.hubStats);
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
                //setting the planet to the correct type:
                currentPlanetName = mapData.path[mapData.path.Count-1];
                for(int i = 0; i < mapData.planets.Count; i++)
                {
                    if(mapData.planets[i].name == currentPlanetName)
                    {
                        planetData.name = mapData.planets[i].planetName;
                        planetData.type = mapData.planets[i].type;
                        return;
                    }
                }
                sceneSwitch.SceneByPath("Assets/Scenes/PlanetaryView/SampleScene.unity");
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

    private void OnMouseEnter()
    {
        inView = true;
        text.SetActive(true);
    }
    private void OnMouseExit()
    {
        inView = false;
        text.SetActive(false);
    }
}
