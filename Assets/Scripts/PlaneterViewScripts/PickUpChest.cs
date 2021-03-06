using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpChest : MonoBehaviour {
    
    private GameObject[] m_ChestsActive;
    public GameObject tutorial;

    private void Start()
    {
        if (GlobalData.firstTimeOnPlanet)
        {
            GlobalData.firstTimeOnPlanet = false;
        }
        else
        {
            tutorial.SetActive(false);
        }
    }
    public int ChestNumberLeft() {
        m_ChestsActive = GameObject.FindGameObjectsWithTag("Chest");
        Debug.Log(m_ChestsActive.Length);
        return m_ChestsActive.Length;
    }
    private void OnDestroy()
    {
        //1resource<-2boxes
        if (m_ChestsActive != null)
        {
            GlobalData.resources = Mathf.Clamp(GlobalData.resources + (4 - m_ChestsActive.Length) / 2, 0, GlobalData.maxResources);
        }
        
    }
}