using UnityEditor;

public  class PlanetAssets {
    // LAVA MAP 1 
    public readonly TerrainData lavaTerrainData =
        (TerrainData) AssetDatabase.LoadAssetAtPath("Assets/TerrainAssets/Lava/LavaTerrain.asset", typeof(TerrainData));

    public readonly NoiseData lavaNoiseData =
        (NoiseData) AssetDatabase.LoadAssetAtPath("Assets/TerrainAssets/Lava/LavaNoise.asset", typeof(NoiseData));

    public readonly TextureData lavaTextureData =
        (TextureData) AssetDatabase.LoadAssetAtPath("Assets/TerrainAssets/Lava/LavaPlanet.asset", typeof(TextureData));

    // DESERT MAP 2
    public readonly TerrainData desertTerrainData =
        (TerrainData) AssetDatabase.LoadAssetAtPath("Assets/TerrainAssets/Desert/DesertTerrain.asset",
            typeof(TerrainData));

    public readonly NoiseData desertNoiseData =
        (NoiseData) AssetDatabase.LoadAssetAtPath("Assets/TerrainAssets/Desert/DesertNoise.asset", typeof(NoiseData));

    public readonly TextureData desertTextureData =
        (TextureData) AssetDatabase.LoadAssetAtPath("Assets/TerrainAssets/Desert/DesertPlanet.asset",
            typeof(TextureData));

    // ALIEN MAP 3 
    public readonly TerrainData alienTerrainData =
        (TerrainData) AssetDatabase.LoadAssetAtPath("Assets/TerrainAssets/Alien/AlienTerrain.asset",
            typeof(TerrainData));

    public readonly NoiseData alienNoiseData =
        (NoiseData) AssetDatabase.LoadAssetAtPath("Assets/TerrainAssets/Alien/AlienNoise.asset", typeof(NoiseData));

    public readonly TextureData alienTextureData =
        (TextureData) AssetDatabase.LoadAssetAtPath("Assets/TerrainAssets/Alien/AlienPlanet.asset",
            typeof(TextureData));

    // EARTH MAP 4
    public readonly TerrainData earthTerrainData =
        (TerrainData) AssetDatabase.LoadAssetAtPath("Assets/TerrainAssets/Earth/EarthTerrain.asset",
            typeof(TerrainData));

    public readonly NoiseData earthNoiseData =
        (NoiseData) AssetDatabase.LoadAssetAtPath("Assets/TerrainAssets/Earth/EarthNoise.asset", typeof(NoiseData));

    public readonly TextureData earthTextureData =
        (TextureData) AssetDatabase.LoadAssetAtPath("Assets/TerrainAssets/Earth/EarthLikePlanet.asset",
            typeof(TextureData));

    // FROZEN MAP 5
    public readonly TerrainData frozenTerrainData =
        (TerrainData) AssetDatabase.LoadAssetAtPath("Assets/TerrainAssets/Frozen/FrozenTerrain.asset",
            typeof(TerrainData));

    public readonly NoiseData frozenNoiseData =
        (NoiseData) AssetDatabase.LoadAssetAtPath("Assets/TerrainAssets/Frozen/FrozenNoise.asset", typeof(NoiseData));

    public readonly TextureData frozenTextureData =
        (TextureData) AssetDatabase.LoadAssetAtPath("Assets/TerrainAssets/Frozen/FrozenPlanet.asset",
            typeof(TextureData));
}