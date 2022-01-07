using UnityEngine;

public class Planet : MonoBehaviour
{
    private string planetName;
    private Vector3 randomRotation;
    private Vector3 scaleChange;
    private bool visited;
    private int difficulty;
    private string type;
    private bool clickable;

    private MapData mapData;
    public float rotationOffset = 20f;
    public GlobalVars global;

    Material[] materials;
    Color[] materialsCopy;

    PlanetData planetData;
    GameObject linePrefab;
    GameObject line;
    MainHandler mainHandler;

    TooltipPopup popup;
    // Start is called before the first frame update
    void Awake()
    {
        mainHandler = FindObjectOfType<MainHandler>();
        mapData = mainHandler.mapData;
        materials = GetComponent<Renderer>().materials;
        materialsCopy = new Color[materials.Length];
        linePrefab = mainHandler.line;
        popup = mainHandler.popup;

        if (!visited)
        {
            for (int i = 0; i < materials.Length; i++)
            {
                materialsCopy[i] = materials[i].color;
                materials[i].color = Color.gray;
            }
        }
      
    }
    private void OnDestroy()
    {
        planetData.planetName = planetName;
        planetData.rotation = randomRotation;
        planetData.scale = scaleChange;
        planetData.visited = visited;
        planetData.difficulty = difficulty;
        planetData.type = type;
        planetData.clickable = clickable;
    }
    // Update is called once per frame

    void FixedUpdate()
    {  
        if (visited && GlobalDataHandler.GetPref(GlobalDataHandler.MAP_ACTIVE))
        {
            for (int i = 0; i < materialsCopy.Length; i++)
            {
                materials[i].color = materialsCopy[i];
            }
        }
        gameObject.transform.Rotate(randomRotation * Time.deltaTime);
    }
    private void OnMouseEnter()
    {
        if (!visited && clickable && line == null && GlobalDataHandler.GetPref(GlobalDataHandler.MAP_ACTIVE))
        {
            line = Instantiate(linePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            line.name = "line";
            var playerPosition = GameObject.Find(mapData.playerPosition);
            LineController lineController = line.GetComponent<LineController>();
            Transform[] points = new Transform[2];
            points[0] = playerPosition.transform;
            points[1] = gameObject.transform;
            Color color = Color.white;
            switch (difficulty)
            {
                case 1:
                    {
                        color = Color.green;
                        break;
                    }
                case 2:
                    {
                        color = Color.yellow;
                        break;
                    }
                case 3:
                    {
                        color = Color.red;
                        break;
                    }
            }
            lineController.SetUpLine(points, color);
            popup.Activate(gameObject,line);
        }
    }
    public void setClickable(bool x)
    {
        clickable = x;
    }
    public void setDifficulty(int x)
    {
        difficulty = x;
    }
    public void setVisited(bool x)
    {
        visited = x;
    }

    public bool isVisited()
    {
        return visited;
    }
    
    public int getDifficulty()
    {
        return difficulty;
    }
    public bool getClickable()
    {
        return clickable;
    }
    public string getName()
    {
        return planetName;
    }
    public GameObject getLine()
    {
        return line;
    }
    public Vector3 getScale()
    {
        return scaleChange;
    }
    public Vector3 getRotation()
    {
        return randomRotation;
    }
    public string getType()
    {
        return type;
    }
    public void SetPlanetData(PlanetData planetData)
    {
        planetName = planetData.planetName;
        randomRotation = planetData.rotation;
        scaleChange = planetData.scale;
        visited = planetData.visited;
        difficulty = planetData.difficulty;
        type = planetData.type;
        clickable = planetData.clickable;
        this.planetData = planetData;
    }
}
