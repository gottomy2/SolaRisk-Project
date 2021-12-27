using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpChest : MonoBehaviour {
    
    private GameObject[] m_ChestsActive;
    
    
    public int ChestNumberLeft() {
        m_ChestsActive = GameObject.FindGameObjectsWithTag("Chest");
        Debug.Log(m_ChestsActive.Length);
        return m_ChestsActive.Length;
    }
}