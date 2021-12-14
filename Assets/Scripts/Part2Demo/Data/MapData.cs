using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MapData : ScriptableObject
{
    public bool firstStart = true;
    public string playerPosition = "Pstart";
    public List<PlanetData> planets;
    public List<string> path;
}

