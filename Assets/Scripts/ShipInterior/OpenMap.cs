using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMap : MonoBehaviour
{
    public GameObject text;
    public float maxDistance = 4f;

    private bool inView = false;
    private GameObject player;
    private Vector3 playerPosition;
    private float distance;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        playerPosition = player.transform.position;
        distance = Mathf.Sqrt(Mathf.Pow(playerPosition.x - gameObject.transform.position.x, 2) + Mathf.Pow(playerPosition.z - gameObject.transform.position.z, 2));

        if (Input.GetKeyDown(KeyCode.E) && inView && distance <= maxDistance)
        {
            SceneManager.LoadScene("Assets/Scenes/Part2Demo/Part2Demo.unity");
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
