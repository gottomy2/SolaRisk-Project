using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OxygenHandler : MonoBehaviour {
    private Text m_OxygenLevel;
    private const float m_maxTime = 60f;
    private float m_OxygenTimer;
    public GlobalVars global;

    void Start() {
        m_OxygenLevel = GameObject.Find("OxygenMeter").GetComponent<Text>();
        m_OxygenTimer = 0f;
        SceneShader.GetInstance().SetIsLighting(true);
    }

    private void Update() {
        m_OxygenTimer += Time.deltaTime;
        double oxygenPercentage = Math.Round(((m_maxTime - m_OxygenTimer) * 100) / m_maxTime);
        if (oxygenPercentage >= 0) {
            m_OxygenLevel.text = oxygenPercentage + "% Tlenu";
        }
        if (m_maxTime - m_OxygenTimer <= 0) {
            StartCoroutine(StartSceneChangeRoutine());
        }
    }

    private IEnumerator StartSceneChangeRoutine() {
        yield return new WaitForSeconds(2f);
        SceneShader.GetInstance().SetIsShading(true);
        yield return new WaitForSeconds(1f);

        GlobalData.SetVar("planetCanLand", false, GlobalData.hubStats);
        SceneManager.LoadScene("Assets/Scenes/ShipInterior/InteriorScene.unity");
    }
}