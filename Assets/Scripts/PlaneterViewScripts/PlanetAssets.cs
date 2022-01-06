using UnityEditor;
using UnityEngine;

public class PlanetAssets {
    // LAVA MAP 1 
    public readonly TerrainData lavaTerrainData =
        (TerrainData) Resources.Load("Assets/Materials/Terrain/Lava/LavaTerrain.asset", typeof(TerrainData));

    public readonly NoiseData lavaNoiseData =
        (NoiseData) Resources.Load("Assets/Materials/Terrain/Lava/LavaNoise.asset", typeof(NoiseData));

    public readonly TextureData lavaTextureData =
        (TextureData) Resources.Load("Assets/Materials/Terrain/Lava/LavaPlanet.asset", typeof(TextureData));

    // DESERT MAP 2
    public readonly TerrainData desertTerrainData =
        (TerrainData) Resources.Load("Assets/Materials/Terrain/Desert/DesertTerrain.asset",
            typeof(TerrainData));

    public readonly NoiseData desertNoiseData =
        (NoiseData) Resources.Load("Assets/Materials/Terrain/Desert/DesertNoise.asset", typeof(NoiseData));

    public readonly TextureData desertTextureData =
        (TextureData) Resources.Load("Assets/Materials/Terrain/Desert/DesertPlanet.asset",
            typeof(TextureData));

    // ALIEN MAP 3 
    public readonly TerrainData alienTerrainData =
        (TerrainData) Resources.Load("Assets/Materials/Terrain/Alien/AlienTerrain.asset",
            typeof(TerrainData));

    public readonly NoiseData alienNoiseData =
        (NoiseData) Resources.Load("Assets/Materials/Terrain/Alien/AlienNoise.asset", typeof(NoiseData));

    public readonly TextureData alienTextureData =
        (TextureData) Resources.Load("Assets/Materials/Terrain/Alien/AlienPlanet.asset",
            typeof(TextureData));

    // EARTH MAP 4
    public readonly TerrainData earthTerrainData =
        (TerrainData) Resources.Load("Assets/Materials/Terrain/Earth/EarthTerrain.asset",
            typeof(TerrainData));

    public readonly NoiseData earthNoiseData =
        (NoiseData) Resources.Load("Assets/Materials/Terrain/Earth/EarthNoise.asset", typeof(NoiseData));

    public readonly TextureData earthTextureData =
        (TextureData) Resources.Load("Assets/Materials/Terrain/Earth/EarthLikePlanet.asset",
            typeof(TextureData));

    // FROZEN MAP 5
    public readonly TerrainData frozenTerrainData =
        (TerrainData) Resources.Load("Assets/Materials/Terrain/Frozen/FrozenTerrain.asset",
            typeof(TerrainData));

    public readonly NoiseData frozenNoiseData =
        (NoiseData) Resources.Load("Assets/Materials/Terrain/Frozen/FrozenNoise.asset", typeof(NoiseData));

    public readonly TextureData frozenTextureData =
        (TextureData) Resources.Load("Assets/Materials/Terrain/Frozen/FrozenPlanet.asset",
            typeof(TextureData));

    // GALAXY FIRE
    public readonly Material fireMap = (Material) Resources.Load(
        "Assets/Packages/DeepSpaceSkyboxPack/GalaxyFire/Material/GalaxyFireMaterial.mat",
        typeof(Material));

    // GALACTIC GREEN
    public readonly Material galacticGreen =
        (Material) Resources.Load(
            "Assets/Packages/DeepSpaceSkyboxPack/GalacticGreen/Material/GalacticGreenMaterial.mat",
            typeof(Material));

    // DIVERSE SPACE
    public readonly Material diverseSpace =
        (Material) Resources.Load(
            "Assets/Packages/DeepSpaceSkyboxPack/DiverseSpace/Material/DiverseSpaceMaterial.mat",
            typeof(Material));
}
