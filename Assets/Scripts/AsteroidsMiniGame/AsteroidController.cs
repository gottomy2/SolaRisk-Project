using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject asteroid;
    public float respawnTime = 0.3f;
    public int waveNumber;
    Transform shipTransform;
    GameObject ship;
    
    void Start()
    {
        ship = GameObject.FindGameObjectWithTag("Player");
        respawnTime = GetRespawnTimeFromDifficulty();
        StartCoroutine(AsteroidWave());
    }
    
    private void SpawnAsteroid()
    {
        shipTransform = ship.transform;
        for (int i = 0; i < GetWaveNumberFromDifficulty(); i++)
        {
            Debug.Log("Get num from dif: " + GetWaveNumberFromDifficulty() +", Diff: " + ShipController.GetDifficulty());
            float x = ship.transform.position.x + Random.Range(-150.0f, 150.0f);
            float y = ship.transform.position.y + Random.Range(-150.0f, 150.0f);
            float z = ship.transform.position.z + Random.Range(-300.0f, -250.0f);
            GameObject a = Instantiate(asteroid) as GameObject;
            a.transform.position = new Vector3(x, y, z);
            Rigidbody rigidBody = a.AddComponent<Rigidbody>();
            rigidBody.isKinematic = false;
            rigidBody.useGravity = false;
            Vector3 direction = new Vector3(Random.Range(-0.1f,0.1f), Random.Range(-0.1f, 0.1f), 1);
            rigidBody.AddForce(direction * Random.Range(1000 * GetNumberFromDifficulty(),2000 * GetNumberFromDifficulty()));
        }
    }
    IEnumerator AsteroidWave()
    {
        while (!ship.GetComponent<ShipController>().CanSeeEnd())
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnAsteroid();
        }
    }

    private float GetNumberFromDifficulty()
    {
        switch (ShipController.GetDifficulty())
        {
            case Difficulty.Easy:
            default: return 1;
            case Difficulty.Medium: return 1.5f;
            case Difficulty.Hard: return 2f;
            
        }
    }

    private int GetWaveNumberFromDifficulty()
    {
        switch (ShipController.GetDifficulty())
        {
            case Difficulty.Easy:
            default: return 10;
            case Difficulty.Medium: return 15;
            case Difficulty.Hard: return 20;
        }
    }

    private float GetRespawnTimeFromDifficulty()
    {
        switch (ShipController.GetDifficulty())
        {
            case Difficulty.Easy:
            default: return 0.3f;
            case Difficulty.Medium: return 0.2f;
            case Difficulty.Hard: return 0.1f;
        }
    }

}
