using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipPopup : MonoBehaviour, IPointerExitHandler
{
    TextMeshProUGUI text;
    Camera mainCamera;
    TooltipButton button;
    Image riskIcon;
    GameObject line;
    GameObject planet;

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
    }
    public void Activate(GameObject planet2, GameObject line2)
    {
        planet = planet2;
        gameObject.SetActive(true);
        line = line2;
        Vector3 newPos = planet.transform.position;
        gameObject.transform.position = mainCamera.WorldToScreenPoint(newPos);
        int difficulty = planet.GetComponent<Planet>().getDifficulty();
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

        text.SetText("Nazwa: " + planet.GetComponent<Planet>().getName() + "\n" + "Planeta: " + planet.GetComponent<Planet>().getTranslatedType() + "\n" + "Ryzyko: " + difficulty);
        button.SetPlanet(planet.name);
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (line != null) Destroy(line);
        gameObject.SetActive(false);
    }
}
