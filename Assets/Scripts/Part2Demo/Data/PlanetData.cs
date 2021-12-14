using UnityEngine;

[CreateAssetMenu]
public class PlanetData : ScriptableObject
{
    public string planetName;
    public GameObject prefab;
    public Vector3 rotation;
    public Vector3 scale;
    public bool visited = false;
    public int difficulty = 0;
    public string type;
    public bool clickable = false;
}
