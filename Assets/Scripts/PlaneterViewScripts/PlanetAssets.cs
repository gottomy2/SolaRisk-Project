using UnityEditor;
using UnityEngine;

public class PlanetAssets {
    // LAVA MAP 1 
    public readonly TerrainData lavaTerrainData =
        (TerrainData) AssetDatabase.LoadAssetAtPath("Assets/Materials/Terrain/Lava/LavaTerrain.asset", typeof(TerrainData));

    public readonly NoiseData lavaNoiseData =
        (NoiseData) AssetDatabase.LoadAssetAtPath("Assets/Materials/Terrain/Lava/LavaNoise.asset", typeof(NoiseData));

    public readonly TextureData lavaTextureData =
        (TextureData) AssetDatabase.LoadAssetAtPath("Assets/Materials/Terrain/Lava/LavaPlanet.asset", typeof(TextureData));

    // DESERT MAP 2
    public readonly TerrainData desertTerrainData =
        (TerrainData) AssetDatabase.LoadAssetAtPath("Assets/Materials/Terrain/Desert/DesertTerrain.asset",
            typeof(TerrainData));

    public readonly NoiseData desertNoiseData =
        (NoiseData) AssetDatabase.LoadAssetAtPath("Assets/Materials/Terrain/Desert/DesertNoise.asset", typeof(NoiseData));

    public readonly TextureData desertTextureData =
        (TextureData) AssetDatabase.LoadAssetAtPath("Assets/Materials/Terrain/Desert/DesertPlanet.asset",
            typeof(TextureData));

    // ALIEN MAP 3 
    public readonly TerrainData alienTerrainData =
        (TerrainData) AssetDatabase.LoadAssetAtPath("Assets/Materials/Terrain/Alien/AlienTerrain.asset",
            typeof(TerrainData));

    public readonly NoiseData alienNoiseData =
        (NoiseData) AssetDatabase.LoadAssetAtPath("Assets/Materials/Terrain/Alien/AlienNoise.asset", typeof(NoiseData));

    public readonly TextureData alienTextureData =
        (TextureData) AssetDatabase.LoadAssetAtPath("Assets/Materials/Terrain/Alien/AlienPlanet.asset",
            typeof(TextureData));

    // EARTH MAP 4
    public readonly TerrainData earthTerrainData =
        (TerrainData) AssetDatabase.LoadAssetAtPath("Assets/Materials/Terrain/Earth/EarthTerrain.asset",
            typeof(TerrainData));

    public readonly NoiseData earthNoiseData =
        (NoiseData) AssetDatabase.LoadAssetAtPath("Assets/Materials/Terrain/Earth/EarthNoise.asset", typeof(NoiseData));

    public readonly TextureData earthTextureData =
        (TextureData) AssetDatabase.LoadAssetAtPath("Assets/Materials/Terrain/Earth/EarthLikePlanet.asset",
            typeof(TextureData));

    // FROZEN MAP 5
    public readonly TerrainData frozenTerrainData =
        (TerrainData) AssetDatabase.LoadAssetAtPath("Assets/Materials/Terrain/Frozen/FrozenTerrain.asset",
            typeof(TerrainData));

    public readonly NoiseData frozenNoiseData =
        (NoiseData) AssetDatabase.LoadAssetAtPath("Assets/Materials/Terrain/Frozen/FrozenNoise.asset", typeof(NoiseData));

    public readonly TextureData frozenTextureData =
        (TextureData) AssetDatabase.LoadAssetAtPath("Assets/Materials/Terrain/Frozen/FrozenPlanet.asset",
            typeof(TextureData));

    // GALAXY FIRE
    public readonly Material fireMap = (Material) AssetDatabase.LoadAssetAtPath(
        "Assets/Packages/DeepSpaceSkyboxPack/GalaxyFire/Material/GalaxyFireMaterial.mat",
        typeof(Material));

    // GALACTIC GREEN
    public readonly Material galacticGreen =
        (Material) AssetDatabase.LoadAssetAtPath(
            "Assets/Packages/DeepSpaceSkyboxPack/GalacticGreen/Material/GalacticGreenMaterial.mat",
            typeof(Material));

    // DIVERSE SPACE
    public readonly Material diverseSpace =
        (Material) AssetDatabase.LoadAssetAtPath(
            "Assets/Packages/DeepSpaceSkyboxPack/DiverseSpace/Material/DiverseSpaceMaterial.mat",
            typeof(Material));
}