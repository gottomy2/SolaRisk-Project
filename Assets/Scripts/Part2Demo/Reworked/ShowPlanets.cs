using UnityEngine;

public class ShowPlanets : MonoBehaviour
{
    public void Show(MapData mapData)
    {
        GameObject parent = GameObject.Find("Planets");

        var planet1 = Instantiate(mapData.planets[0].prefab, new Vector3(-7, 0, 0), Quaternion.identity);
        planet1.name = "Pstart";
        planet1.transform.parent = parent.transform;
        planet1.transform.localScale += mapData.planets[0].scale;
        planet1.GetComponent<Planet>().SetPlanetData(mapData.planets[0]);

        var planet2 = Instantiate(mapData.planets[10].prefab, new Vector3(7, 0, 0), Quaternion.identity);
        planet2.name = "Pend";
        planet2.transform.parent = parent.transform;
        planet2.transform.localScale += mapData.planets[10].scale;
        planet2.GetComponent<Planet>().SetPlanetData(mapData.planets[10]);

        for (int i = 1; i <= 3; i++)
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
            var prefab1 = Instantiate(mapData.planets[i].prefab, new Vector3(-4 + x1, (3 - (3 * (i - 1))) + y1, 0 + z1), Quaternion.identity);

            prefab1.name = "P" + i + "R1";
            prefab1.transform.parent = parent.transform;
            prefab1.transform.localScale += mapData.planets[i].scale;
            prefab1.GetComponent<Planet>().SetPlanetData(mapData.planets[i]);

            //second 3
            var prefab2 = Instantiate(mapData.planets[i + 3].prefab, new Vector3(0 + x2, (3 - (3 * (i - 1))) + y2, 0 + z2), Quaternion.identity);

            prefab2.name = "P" + i + "R2";
            prefab2.transform.parent = parent.transform;
            prefab2.transform.localScale += mapData.planets[i+3].scale;
            prefab2.GetComponent<Planet>().SetPlanetData(mapData.planets[i + 3]);

            //third 3
            var prefab3 = Instantiate(mapData.planets[i + 6].prefab, new Vector3(4 + x3, (3 - (3 * (i - 1))) + y3, 0 + z3), Quaternion.identity);

            prefab3.name = "P" + i + "R3";
            prefab3.transform.parent = parent.transform;
            prefab3.transform.localScale += mapData.planets[i+6].scale;
            prefab3.GetComponent<Planet>().SetPlanetData(mapData.planets[i + 6]);
        }
    }
}
