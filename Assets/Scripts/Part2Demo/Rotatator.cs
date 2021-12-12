using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatator : MonoBehaviour {

    Vector3 scaleChange;
    Vector3 randomRotation;

    public float rotationOffset = 20f;
    private int difficulty;
    private string planetName;
    bool visible = false;
    bool clickable = false;

    Material[] materials;
    Color[] materialsCopy;

    GameObject linePrefab;
    GameObject line;
    MainHandler mainHandler;

    TooltipPopup popup;
    // Start is called before the first frame update
    void Awake()
    {
        popup = FindObjectOfType<TooltipPopup>();
        mainHandler = FindObjectOfType<MainHandler>();
        materials = GetComponent<Renderer>().materials;
        materialsCopy = new Color[materials.Length];
        linePrefab = mainHandler.line;
        planetName = GenerateName(Random.Range(4, 8));

        for(int i = 0; i < materials.Length; i++)
        {
            materialsCopy[i] = materials[i].color;
            materials[i].color = Color.gray;
        }

        var scale = Random.Range(0.1f, 0.4f);
       
        scaleChange = new Vector3(scale,scale,scale);
        gameObject.transform.localScale += scaleChange;

        randomRotation.x = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.y = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.z = Random.Range(-rotationOffset, rotationOffset);
    }

    // Update is called once per frame
  
    void FixedUpdate()
    {
        gameObject.transform.Rotate(randomRotation * Time.deltaTime);
        if (visible)
        {
            for (int i = 0; i < materialsCopy.Length; i++)
            {
                materials[i].color = materialsCopy[i];
            }
        }
    }
    private void OnMouseEnter()
    {
        if (!visible && clickable && line==null)
        {
            line = Instantiate(linePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            line.name = "line";
            var playerPosition = GameObject.Find(mainHandler.GetPlayerPosition());
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
   public static string GenerateName(int len)
   {
       string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
       string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
       string Name = "";
       Name += consonants[Random.Range(0, consonants.Length)].ToCharArray()[0].ToString().ToUpper();
       Name += vowels[Random.Range(0, vowels.Length)];
       int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
       while (b < len)
       {
            string nextCons = consonants[Random.Range(0, consonants.Length)];
            string nextVow = vowels[Random.Range(0, vowels.Length)];
            Name += nextCons;
            b += nextCons.Length;
            Name += nextVow;
            b += nextVow.Length;
       }
       return Name;
   }
    public void setClickable(bool x)
    {
        clickable = x;
    }
    public void setVisible(bool x)
    {
        visible = x;
    }
    public void setDifficulty(int x)
    {
        difficulty = x;
    }
    public int getDifficulty()
    {
        return difficulty;
    }
    public bool getClickable()
    {
        return clickable;
    }
    public bool getVisible()
    {
        return visible;
    }
    public string getName()
    {
        return planetName;
    }
    public GameObject getLine()
    {
        return line;
    }
}
