using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipButton : MonoBehaviour
{
    private string planetName;
    MainHandler mainHandler;
    Button button;
    TooltipPopup popup;
    private void Start()
    {
        mainHandler = FindObjectOfType<MainHandler>();
        popup = FindObjectOfType<TooltipPopup>();
        planetName = mainHandler.GetPlayerPosition();

        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Fly);
        
    }
    private void Fly()
    {
        GameObject planet = GameObject.Find(planetName);
        Rotatator rotatator = planet.GetComponent<Rotatator>();
        
        if (rotatator.getClickable())
        {
            rotatator.setVisible(true);
            mainHandler.ChangePlayerPosition(planetName);
            popup.Deactivate();
            Destroy(GameObject.Find("line"));
        }
    }
    public void SetPlanet(string name)
    {
        planetName = name;
    }
}