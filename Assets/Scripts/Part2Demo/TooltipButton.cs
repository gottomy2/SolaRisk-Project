using UnityEngine;
using UnityEngine.UI;

public class TooltipButton : MonoBehaviour
{
    private string planetName;
    private MapData mapData;
    MainHandler mainHandler;
    Button button;
    TooltipPopup popup;
    private void Start()
    {
        mainHandler = FindObjectOfType<MainHandler>();
        popup = FindObjectOfType<TooltipPopup>();
        mapData = mainHandler.mapData;
        planetName = mainHandler.mapData.playerPosition;

        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Fly);

    }
    private void Fly()
    {
        GameObject planet = GameObject.Find(planetName);
        Planet planet1 = planet.GetComponent<Planet>();

        if (planet1.getClickable())
        {
            planet1.setVisited(true);
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