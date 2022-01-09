using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class CreateMapData : MonoBehaviour
{
    public List<GameObject> prefabs;
    public void Generate()
    {
        int x = prefabs.Count - 1;
        int random;
        float rotationOffset = 20f;
        Vector3 randomRotation;

        int[] array = { 1, 2, 3 };
        List<int> difficulties1 = array.ToList();
        List<int> difficulties2 = array.ToList();
        List<int> difficulties3 = array.ToList();
        int difficulty;

        GlobalData.path = new List<string>();
        for (int i = 0; i <= 10; i++)
        {
            random = Random.Range(0, x);

            var scale = Random.Range(0.1f, 0.4f);
            Vector3 scaleChange = new Vector3(scale, scale, scale);

            randomRotation.x = Random.Range(-rotationOffset, rotationOffset);
            randomRotation.y = Random.Range(-rotationOffset, rotationOffset);
            randomRotation.z = Random.Range(-rotationOffset, rotationOffset);

            string[] input = prefabs[random].GetComponent<MeshFilter>().sharedMesh.name.Split(' ');
            string type = Regex.Replace(input[0], @"[\d-]", string.Empty);


            GlobalData.planets[i].planetName = GenerateName(Random.Range(4, 8));
            GlobalData.planets[i].prefab = prefabs[random];
            GlobalData.planets[i].rotation = randomRotation;
            GlobalData.planets[i].scale = scaleChange;
            GlobalData.planets[i].visited = false;
            GlobalData.planets[i].clickable = false;
            if (i > 0 && i <= 3)
            {
                difficulty = Random.Range(0, difficulties1.Count);
                GlobalData.planets[i].difficulty = difficulties1.ElementAt(difficulty);
                difficulties1.RemoveAt(difficulty);
                GlobalData.planets[i].clickable = true;
            }
            else if (i > 3 && i <= 6)
            {
                difficulty = Random.Range(0, difficulties2.Count);
                GlobalData.planets[i].difficulty = difficulties2.ElementAt(difficulty);
                difficulties2.RemoveAt(difficulty);
            }
            else if (i > 6 && i <= 9)
            {
                difficulty = Random.Range(0, difficulties3.Count);
                GlobalData.planets[i].difficulty = difficulties3.ElementAt(difficulty);
                difficulties3.RemoveAt(difficulty);
            }
            else if (i > 9) GlobalData.planets[i].difficulty = 3;
            if (i == 0)
            {
                GlobalData.planets[i].visited = true;
            }


            GlobalData.planets[i].type = type;

            prefabs.RemoveAt(random);
            x--;
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
}
