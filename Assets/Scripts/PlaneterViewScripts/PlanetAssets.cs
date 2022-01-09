using UnityEditor;
using UnityEngine;

public class PlanetAssets {
    // LAVA MAP 1 
    public readonly TerrainData lavaTerrainData =
        (TerrainData) Resources.Load("Terrain/Lava/LavaTerrain", typeof(TerrainData));

    public readonly NoiseData lavaNoiseData =
        (NoiseData) Resources.Load("Terrain/Lava/LavaNoise", typeof(NoiseData));

    public readonly TextureData lavaTextureData =
        (TextureData) Resources.Load("Terrain/Lava/LavaPlanet", typeof(TextureData));

    // DESERT MAP 2
    public readonly TerrainData desertTerrainData =
        (TerrainData) Resources.Load("Terrain/Desert/DesertTerrain",
            typeof(TerrainData));

    public readonly NoiseData desertNoiseData =
        (NoiseData) Resources.Load("Terrain/Desert/DesertNoise", typeof(NoiseData));

    public readonly TextureData desertTextureData =
        (TextureData) Resources.Load("Terrain/Desert/DesertPlanet",
            typeof(TextureData));

    // ALIEN MAP 3 
    public readonly TerrainData alienTerrainData =
        (TerrainData) Resources.Load("Terrain/Alien/AlienTerrain",
            typeof(TerrainData));

    public readonly NoiseData alienNoiseData =
        (NoiseData) Resources.Load("Terrain/Alien/AlienNoise", typeof(NoiseData));

    public readonly TextureData alienTextureData =
        (TextureData) Resources.Load("Terrain/Alien/AlienPlanet",
            typeof(TextureData));

    // EARTH MAP 4
    public readonly TerrainData earthTerrainData =
        (TerrainData) Resources.Load("Terrain/Earth/EarthTerrain",
            typeof(TerrainData));

    public readonly NoiseData earthNoiseData =
        (NoiseData) Resources.Load("Terrain/Earth/EarthNoise", typeof(NoiseData));

    public readonly TextureData earthTextureData =
        (TextureData) Resources.Load("Terrain/Earth/EarthLikePlanet",
            typeof(TextureData));

    // FROZEN MAP 5
    public readonly TerrainData frozenTerrainData =
        (TerrainData) Resources.Load("Terrain/Frozen/FrozenTerrain",
            typeof(TerrainData));

    public readonly NoiseData frozenNoiseData =
        (NoiseData) Resources.Load("Terrain/Frozen/FrozenNoise", typeof(NoiseData));

    public readonly TextureData frozenTextureData =
        (TextureData) Resources.Load("Terrain/Frozen/FrozenPlanet",
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
