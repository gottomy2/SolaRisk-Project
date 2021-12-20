using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpChest : MonoBehaviour {
    
    private GameObject[] m_ChestsActive;

    public void PickUp() {
        m_ChestsActive = GameObject.FindGameObjectsWithTag("Chest");
        foreach (var chest in m_ChestsActive) {
            if (chest.transform.hasChanged) {
                Debug.Log("hehe");
            }
        }
    }
}