
using UnityEngine;

public class ChangePlanet : MonoBehaviour {

    public PlanetData pData;
    public GameObject alien;
    public GameObject earthLike;
    public GameObject desert;
    public GameObject frozen;
    public GameObject tundra;
    public GameObject lava;
    public GameObject temperate;
    private PlanetAssets m_PlanetAssets;

    public ChangePlanet(PlanetAssets planetAssets) {
        this.m_PlanetAssets = planetAssets;
    }

    void Start() {

        m_PlanetAssets = new PlanetAssets();    
        MapGenerator mapGenerator = GameObject.Find("MapGenerator").GetComponent<MapGenerator>();


        switch (pData.type) {
            case "Lava":
                mapGenerator.terrainData = m_PlanetAssets.lavaTerrainData ;
                mapGenerator.noiseData = m_PlanetAssets.lavaNoiseData;
                mapGenerator.textureData = m_PlanetAssets.lavaTextureData;
                lava.SetActive(true);
                mapGenerator.DrawMapInEditor();
                break;
            case "Desert":
                mapGenerator.terrainData = m_PlanetAssets.desertTerrainData;
                mapGenerator.noiseData = m_PlanetAssets.desertNoiseData;
                mapGenerator.textureData = m_PlanetAssets.desertTextureData;
                desert.SetActive(true);
                mapGenerator.DrawMapInEditor();
                break;
            case "Alien":
                mapGenerator.terrainData = m_PlanetAssets.alienTerrainData;
                mapGenerator.noiseData = m_PlanetAssets.alienNoiseData;
                mapGenerator.textureData = m_PlanetAssets.alienTextureData;
                    
                alien.SetActive(true);
                mapGenerator.DrawMapInEditor();
                
                break;
            case "EarthLike":
                mapGenerator.terrainData = m_PlanetAssets.earthTerrainData;
                mapGenerator.noiseData = m_PlanetAssets.earthNoiseData;
                mapGenerator.textureData = m_PlanetAssets.earthTextureData;
                earthLike.SetActive(true);
                mapGenerator.DrawMapInEditor();
                break;
            case "Frozen":
                mapGenerator.terrainData = m_PlanetAssets.frozenTerrainData;
                mapGenerator.noiseData = m_PlanetAssets.frozenNoiseData;
                mapGenerator.textureData = m_PlanetAssets.frozenTextureData;
                frozen.SetActive(true);
                mapGenerator.DrawMapInEditor();
                break;
            // TODO do tundry dodać drzewka ma być frozen a do temperate to jak earth like więcej wody chuj wie mokra ma być 
            case "Tundra":
                mapGenerator.terrainData = m_PlanetAssets.frozenTerrainData;
                mapGenerator.noiseData = m_PlanetAssets.frozenNoiseData;
                mapGenerator.textureData = m_PlanetAssets.frozenTextureData;
                tundra.SetActive(true);
                mapGenerator.DrawMapInEditor();
                break;
            case "Temperate":
                mapGenerator.terrainData = m_PlanetAssets.earthTerrainData;
                mapGenerator.noiseData = m_PlanetAssets.earthNoiseData;
                mapGenerator.textureData = m_PlanetAssets.earthTextureData;
                temperate.SetActive(true);
                mapGenerator.DrawMapInEditor();
                break;
            // default: Destroy(this);
                // break;
        }
        GameObject.Find("Mesh").AddComponent<MeshCollider>();


        
    }
}