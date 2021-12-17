using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMap : MonoBehaviour
{
    public GameObject text;
    public GlobalVars global;
    public float maxDistance = 4f;

    private SceneSwitch sceneSwitch;
    private bool inView = false;
    private GameObject player;
    private Vector3 playerPosition;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        sceneSwitch = new SceneSwitch();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        distance = Mathf.Sqrt(Mathf.Pow(playerPosition.x - gameObject.transform.position.x, 2) + Mathf.Pow(playerPosition.z - gameObject.transform.position.z, 2));

        if (Input.GetKeyDown(KeyCode.E) && inView && distance <= maxDistance)
        {
            Cursor.lockState = CursorLockMode.Confined;
            sceneSwitch.SceneByPath("Assets/Scenes/Part2Demo/Part2Demo.unity");
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
            text.GetComponent<TextMesh>().text = "Click E\n to open Map";
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
