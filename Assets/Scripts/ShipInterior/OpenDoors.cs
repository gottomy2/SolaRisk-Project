using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    public GameObject doors;
    public GameObject text;
    public GlobalVars global;
    public float maxDistance = 4f;
    private Animator animator;
    private bool inView = false;
    private string state = "Open";
    private GameObject player;
    private Vector3 playerPosition;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        animator = doors.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        distance = Mathf.Sqrt(Mathf.Pow(playerPosition.x - gameObject.transform.position.x, 2) + Mathf.Pow(playerPosition.z - gameObject.transform.position.z, 2));

        if (GlobalData.GetVar("hubTutorial1", GlobalData.dialoguePath) && GlobalData.GetVar("hubTutorial2", GlobalData.dialoguePath))
        {   
            if (Input.GetKeyDown(KeyCode.E) && inView && distance <= maxDistance)
            {
                if (animator.GetBool("DoorUp"))
                {
                    animator.SetBool("DoorUp", false);
                    state = "Close";
                }
                else
                {
                    animator.SetBool("DoorUp", true);
                    state = "Open";
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
                if (animator.GetBool("DoorUp"))
                {
                    state = "Open";
                }
                else
                {
                    state = "Close";
                }
                text.GetComponent<TextMesh>().text = "Click E\nTo " + state + " Doors";
            }
        }
    }
    private void OnMouseEnter()
    {
        if(GlobalData.GetVar("hubTutorial1", GlobalData.dialoguePath) && GlobalData.GetVar("hubTutorial2", GlobalData.dialoguePath))
        {
            inView = true;
            text.SetActive(true);
        }
    }
    private void OnMouseExit()
    {
        if(GlobalData.GetVar("hubTutorial1", GlobalData.dialoguePath) && GlobalData.GetVar("hubTutorial2", GlobalData.dialoguePath))
        {
            inView = false;
            text.SetActive(false);
        }       
    }
}
