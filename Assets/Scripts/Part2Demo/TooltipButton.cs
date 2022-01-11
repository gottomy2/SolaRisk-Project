using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipButton : MonoBehaviour
{
    private string planetName;
    MainHandler mainHandler;
    Button button;
    TooltipPopup popup;
    TextMeshProUGUI text;
    EventManager eventManager;
    private void Start()
    {
        mainHandler = FindObjectOfType<MainHandler>();
        popup = FindObjectOfType<TooltipPopup>();
        planetName = GlobalData.playerPosition;
        text = popup.GetComponentInChildren<TextMeshProUGUI>();
        eventManager = FindObjectOfType<EventManager>();

        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Fly);

    }
    private void Fly()
    {
        GameObject planet = GameObject.Find(planetName);
        Planet planet1 = planet.GetComponent<Planet>();

        int days = 0;
        int resources = 0;
        switch (planet1.getDifficulty())
        {
            case 1:
                {
                    days = 3;
                    resources = 3;
                    break;
                }
            case 2:
                {
                    days = 2;
                    resources = 2;
                    break;
                }
            case 3:
                {
                    days = 1;
                    resources = 1;
                    break;
                }
        }

        if (planet1.getClickable())
        {
            if (GlobalData.resources >= resources)
            {
                planet1.setVisited(true);
                mainHandler.ChangePlayerPosition(planetName);
                popup.Deactivate();
                GlobalData.days += days;
                GlobalData.resources -= resources;
                eventManager.onButtonClick();
            }
            else
            {
                text.SetText("Not enough resources");

            }
            Destroy(GameObject.Find("line"));
        }
    }

    public void SetPlanet(string name)
    {
        planetName = name;
    }
}