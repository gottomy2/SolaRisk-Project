using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomIn : MonoBehaviour
{
    GameObject current;
    MainHandler mainHandler;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().enabled = false;
        mainHandler = FindObjectOfType<MainHandler>();
        current = GameObject.Find(mainHandler.GetPlayerPosition());
    }

    // Update is called once per frame
    void Update()
    {   
        
    }
    public void EnableZoom()
    {
        current = GameObject.Find(mainHandler.GetPlayerPosition());
        gameObject.transform.position = new Vector3(current.transform.position.x, current.transform.position.y, -5);
        GetComponent<Camera>().enabled = true;
    }
    public void DisableZoom()
    {
        GetComponent<Camera>().enabled = false;
    }
}
