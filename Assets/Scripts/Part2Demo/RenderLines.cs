using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLines : MonoBehaviour
{
    public GameObject prefab;
    GameObject parent;
    GameObject planet;

    void Start()
    {
        parent = GameObject.Find("Lines");
        StartCoroutine(LateStart(0.5f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        var start = GameObject.Find("Pstart");
        var end = GameObject.Find("Pend");

        //   / P
        // P - P
        //   \ P
        for (int i = 0; i < 3; i++)
        {
            var planet = GameObject.Find("P" + i + "R1");

            var line = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            line.name = "Pstart - " + "P" + i + "R1";
            line.transform.parent = parent.transform;

            LineController lineController = line.GetComponent<LineController>();
            Transform[] points = new Transform[2];
            points[0] = start.transform;
            points[1] = planet.transform;
            Color color = Color.white;
            switch (planet.GetComponent<Rotatator>().getDifficulty())
            {
                case 0:
                    {
                        color = Color.green;
                        break;
                    }
                case 1:
                    {
                        color = Color.yellow;
                        break;
                    }
                case 2:
                    {
                        color = Color.red;
                        break;
                    }
            }
            lineController.SetUpLine(points,color);

        }
        //  P - P
        //  P - P
        //  P - P

        //p1->p2
        for (int i = 0; i < 3; i++)
        {
            var planet1 = GameObject.Find("P" + i + "R1");
            var planet2 = GameObject.Find("P" + i + "R2");

            var line1 = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            line1.name = "P" + i + "R1 - "+ "P" + i + "R2";
            line1.transform.parent = parent.transform;

            LineController lineController1 = line1.GetComponent<LineController>();
            Transform[] points = new Transform[2];
            points[0] = planet1.transform;
            points[1] = planet2.transform;
            Color color = Color.white;
            switch (planet.GetComponent<Rotatator>().getDifficulty())
            {
                case 0:
                    {
                        color = Color.green;
                        break;
                    }
                case 1:
                    {
                        color = Color.yellow;
                        break;
                    }
                case 2:
                    {
                        color = Color.red;
                        break;
                    }
            }
            lineController1.SetUpLine(points, color);
        }
        //p2->p3
        for (int i = 0; i < 3; i++)
        {
            var planet1 = GameObject.Find("P" + i + "R2");
            var planet2 = GameObject.Find("P" + i + "R3");

            var line1 = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            line1.name = "P" + i + "R2 - " + "P" + i + "R3";
            line1.transform.parent = parent.transform;

            LineController lineController1 = line1.GetComponent<LineController>();
            Transform[] points = new Transform[2];
            points[0] = planet1.transform;
            points[1] = planet2.transform;
            Color color = Color.white;
            switch (planet.GetComponent<Rotatator>().getDifficulty())
            {
                case 0:
                    {
                        color = Color.green;
                        break;
                    }
                case 1:
                    {
                        color = Color.yellow;
                        break;
                    }
                case 2:
                    {
                        color = Color.red;
                        break;
                    }
            }
            lineController1.SetUpLine(points, color);
        }

        // P \
        // P - P
        // P /
        for (int i = 0; i < 3; i++)
        {
            var planet = GameObject.Find("P" + i + "R3");

            var line = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            line.name = "P" + i + "R3 - "+ "Pend";
            line.transform.parent = parent.transform;

            LineController lineController = line.GetComponent<LineController>();
            Transform[] points = new Transform[2];
            points[0] = planet.transform;
            points[1] = end.transform;
            Color color = Color.white;
            switch (planet.GetComponent<Rotatator>().getDifficulty())
            {
                case 0:
                    {
                        color = Color.green;
                        break;
                    }
                case 1:
                    {
                        color = Color.yellow;
                        break;
                    }
                case 2:
                    {
                        color = Color.red;
                        break;
                    }
            }
            lineController.SetUpLine(points, color);
        }


    }

}
