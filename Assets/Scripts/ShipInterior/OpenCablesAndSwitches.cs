using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenCablesAndSwitches : MonoBehaviour
{
    public GameObject text;
    public float maxDistance = 4f;

    private bool inView = false;
    private GameObject player;
    private Vector3 playerPosition;
    private float distance;
    private bool condition;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        condition = (GlobalData.GetVar("wiresBroken", GlobalData.hubStats) || GlobalData.GetVar("switchesBroken", GlobalData.hubStats));
    }
    void Update()
    { 
        playerPosition = player.transform.position;
        distance = Mathf.Sqrt(Mathf.Pow(playerPosition.x - gameObject.transform.position.x, 2) + Mathf.Pow(playerPosition.z - gameObject.transform.position.z, 2));
        if (condition)
        {
            if (Input.GetKeyDown(KeyCode.E) && inView && distance <= maxDistance)
            {
                if(GlobalData.GetVar("wiresBroken", GlobalData.hubStats)){
                    SceneManager.LoadScene("Assets/Scenes/FixLightsWireSwitches/FixTheWires.unity");
                }
                else
                {
                    SceneManager.LoadScene("Assets/Scenes/FixLightsWireSwitches/FixTheLightsScene.unity");
                }
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
