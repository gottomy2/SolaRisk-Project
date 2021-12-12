using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlanetSpawner : MonoBehaviour
{

    public List<GameObject> prefabs;
    GameObject parent;
    int random;
    int x;
    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("Planets");
        x = prefabs.Count-1;

        random = Random.Range(0, x);
        var planet1 = Instantiate(prefabs[random], new Vector3(-7, 0, 0), Quaternion.identity);
        prefabs.RemoveAt(random);
        x--;
        planet1.name = "Pstart";
        planet1.transform.parent = parent.transform;
        planet1.GetComponent<Rotatator>().setClickable(false);
        planet1.GetComponent<Rotatator>().setVisible(true);


        random = Random.Range(0, x);
        var planet2 = Instantiate(prefabs[random], new Vector3(7, 0, 0), Quaternion.identity);
        prefabs.RemoveAt(random);
        x--;
        planet2.name = "Pend";
        planet2.transform.parent = parent.transform;
        planet2.GetComponent<Rotatator>().setDifficulty(3);

        int[] array = { 1, 2, 3 };
        List<int> difficulties1 = array.ToList();
        List<int> difficulties2 = array.ToList();
        List<int> difficulties3 = array.ToList();
        int difficulty;

        for (int i = 0; i < 3; i++)
        {
            float x1 = Random.Range(-0.5f, 0.5f);
            float y1 = Random.Range(-0.5f, 0.5f);
            float z1 = Random.Range(-0.5f, 0.5f);

            float x2 = Random.Range(-0.5f, 0.5f);
            float y2 = Random.Range(-0.5f, 0.5f);
            float z2 = Random.Range(-0.5f, 0.5f);

            float x3 = Random.Range(-0.5f, 0.5f);
            float y3 = Random.Range(-0.5f, 0.5f);
            float z3 = Random.Range(-0.5f, 0.5f);

            
            

            //first 3
            random = Random.Range(0, x);
            var prefab1 = Instantiate(prefabs[random], new Vector3(-4 + x1, (3 - (3 * i)) + y1, 0+z1), Quaternion.identity);
            prefabs.RemoveAt(random);
            x--;
            prefab1.name = "P" + i + "R1";
            prefab1.transform.parent = parent.transform;
            prefab1.GetComponent<Rotatator>().setClickable(true);

            difficulty = Random.Range(0, difficulties1.Count);
            prefab1.GetComponent<Rotatator>().setDifficulty(difficulties1.ElementAt(difficulty));
            difficulties1.RemoveAt(difficulty);
            //second 3
            random = Random.Range(0, x);
            var prefab2 = Instantiate(prefabs[random], new Vector3(0 + x2, (3 - (3 * i)) + y2, 0+z2), Quaternion.identity);
            prefabs.RemoveAt(random);
            x--;
            prefab2.name = "P" + i + "R2";
            prefab2.transform.parent = parent.transform;

            difficulty = Random.Range(0, difficulties2.Count);
            prefab2.GetComponent<Rotatator>().setDifficulty(difficulties2.ElementAt(difficulty));
            difficulties2.RemoveAt(difficulty);
            //third 3
            random = Random.Range(0, x);
            var prefab3 = Instantiate(prefabs[random], new Vector3(4 + x3, (3 - (3 * i)) + y3, 0+z3), Quaternion.identity);
            prefabs.RemoveAt(random);
            x--;
            prefab3.name = "P" + i + "R3";
            prefab3.transform.parent = parent.transform;

            difficulty = Random.Range(0, difficulties1.Count);
            prefab3.GetComponent<Rotatator>().setDifficulty(difficulties3.ElementAt(difficulty));
            difficulties3.RemoveAt(difficulty);
        }
        var popup = FindObjectOfType<TooltipPopup>();
        popup.Deactivate();
    }
    
}
