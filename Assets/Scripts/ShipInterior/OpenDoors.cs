using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    public GameObject doors;
    public GameObject text;
    public float maxDistance = 4f;
    private Animator animator;
    private bool inView = false;
    private string state = "Otworzyæ";
    private GameObject player;
    private Vector3 playerPosition;
    private float distance;
    void Start()
    {
        animator = doors.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
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
                    state = "Zamkn¹æ";
                }
                else
                {
                    animator.SetBool("DoorUp", true);
                    state = "Otworzyæ";
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
                    state = "Otworzyæ";
                }
                else
                {
                    state = "Zamkn¹æ";
                }
                text.GetComponent<TextMesh>().text = "Naciœnij E\nAby " + state + " Drzwi";
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
