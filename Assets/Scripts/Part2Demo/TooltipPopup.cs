using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipPopup : MonoBehaviour, IPointerExitHandler
{
    TextMeshProUGUI text;
    Camera mainCamera;
    TooltipButton button;
    Image riskIcon;
    GameObject line;
    GameObject planet;
    ZoomIn zoom;

    public Sprite green;
    public Sprite yellow;
    public Sprite red;
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        button = FindObjectOfType<TooltipButton>();
        riskIcon = GameObject.Find("RiskIcon").GetComponent<Image>();
        riskIcon.sprite = green;
        zoom = FindObjectOfType<ZoomIn>();
    }
    public void Activate(GameObject planet2, GameObject line2)
    {
        planet = planet2;
        gameObject.SetActive(true);
        line = line2;
        Vector3 newPos = planet.transform.position;
        gameObject.transform.position = mainCamera.WorldToScreenPoint(newPos);
        int difficulty = planet.GetComponent<Rotatator>().getDifficulty();
        switch (difficulty)
        {
            case 1:
                {
                    riskIcon.sprite = green;
                    break;
                }
            case 2:
                {
                    riskIcon.sprite = yellow;
                    break;
                }
            case 3:
                {
                    riskIcon.sprite = red;
                    break;
                }
        }

        string[] input = planet.GetComponent<MeshFilter>().sharedMesh.name.Split(' ');
        string name = Regex.Replace(input[0], @"[\d-]", string.Empty);
        text.SetText("Name: "+ planet.GetComponent<Rotatator>().getName()+"\n"+"Type: "+name+"\n"+"Risk: "+difficulty);
        button.SetPlanet(planet.name);
        //zoom.EnableZoom();
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    { 
        if (line != null) Destroy(line);
        //zoom.DisableZoom();
        gameObject.SetActive(false);
    }
}
