using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickable : MonoBehaviour {
    private BoxCollider m_BoxCollider;
    private Vector3 m_PlayerPosition;
    private float m_Distance;
    private GameObject m_Player;
    private float maxDistance = 5f;
    private bool m_InView;
    private PickUpChest m_PickUpChest;
    private int m_Chests = 4;
    private Text m_ChestCount;

    void Start() {
        m_BoxCollider = gameObject.GetComponent<BoxCollider>();
        m_Player = GameObject.FindWithTag("Player");
        m_PickUpChest = GameObject.Find("ChestCount").GetComponent<PickUpChest>();
        m_ChestCount = GameObject.Find("ChestPicked").GetComponent<Text>();
    }

    void Update() {
        m_PlayerPosition = m_Player.transform.position;
        m_Distance = Mathf.Sqrt(Mathf.Pow(m_PlayerPosition.x - gameObject.transform.position.x, 2) +
                                Mathf.Pow(m_PlayerPosition.z - gameObject.transform.position.z, 2));

        if (Input.GetKeyDown(KeyCode.E) && m_InView && m_Distance <= maxDistance) {
            gameObject.SetActive(false);
            m_ChestCount.text = (m_Chests - m_PickUpChest.ChestNumberLeft()).ToString();
        }
    }

    private void OnMouseEnter() {
        m_InView = true;
    }

    private void OnMouseExit() {
        m_InView = false;
    }
}