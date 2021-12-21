using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCablesAndSwitches : MonoBehaviour
{
    public GameObject text;
    public GlobalVars global;
    public float maxDistance = 4f;

    private SceneSwitch sceneSwitch;
    private bool inView = false;
    private GameObject player;
    private Vector3 playerPosition;
    private float distance;
    private bool condition;

    // Start is called before the first frame update
    void Start()
    {
        sceneSwitch = new SceneSwitch();
        player = GameObject.FindGameObjectWithTag("Player");
        condition = (global.getVar("wiresBroken", global.hubStats) || global.getVar("switchesBroken", global.hubStats));
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
                Cursor.lockState = CursorLockMode.Confined;
                if(global.getVar("wiresBroken", global.hubStats)){
                    sceneSwitch.SceneByPath("Assets/Scenes/FixLightsWireSwitches/FixTheWires.unity");
                }
                else
                {
                    sceneSwitch.SceneByPath("Assets/Scenes/FixLightsWireSwitches/FixTheLightsScene.unity");
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
            if (text.activeSelf)
            {
                text.GetComponent<TextMesh>().text = "Click E\n to Repair";
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
