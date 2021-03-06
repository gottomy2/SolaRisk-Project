using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class OpenPlanet : MonoBehaviour
{
    public GameObject text;
    public float maxDistance = 4f;
    public PlanetData planetData;

    private bool inView = false;
    private GameObject player;
    private Vector3 playerPosition;
    private float distance;
    private bool condition;
    private string currentPlanetName;

    void Start()
    {
        if (!GlobalData.GetVar("minigameFailed", GlobalData.hubStats))
        {
            GlobalData.SetVar("planetCanLand", true, GlobalData.hubStats);
        }
        else
        {
            GlobalData.SetVar("planetCanLand", false, GlobalData.hubStats);
        }

        player = GameObject.FindGameObjectWithTag("Player");
        condition = GlobalData.GetVar("planetCanLand", GlobalData.hubStats) && !GlobalData.GetVar("planetVisited", GlobalData.hubStats) && !isEmpty(GlobalData.path);
    }

    void Update()
    {
        playerPosition = player.transform.position;
        distance = Mathf.Sqrt(Mathf.Pow(playerPosition.x - gameObject.transform.position.x, 2) + Mathf.Pow(playerPosition.z - gameObject.transform.position.z, 2));

        if (condition)
        {
            if (Input.GetKeyDown(KeyCode.E) && inView && distance <= maxDistance)
            {
                SceneManager.LoadScene("Assets/Scenes/PlanetaryView/SampleScene.unity");
                GlobalData.SaveVisitChoice(true);
                GlobalData.resources++;
                GlobalData.days++;
                GlobalData.SetVar("planetVisited", true, GlobalData.hubStats);
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
