using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject asteroid;
    public float respawnTime = 2.0f;
    public int waveNumber = 10;
    Transform shipTransform;
    GameObject ship;

    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(AsteroidWave());
    }
    private void SpawnAsteroid()
    {
        shipTransform = ship.transform;
        for (int i = 0; i < waveNumber; i++)
        {
            float x = ship.transform.position.x + Random.Range(-150.0f, 150.0f);
            float y = ship.transform.position.y + Random.Range(-150.0f, 150.0f);
            float z = ship.transform.position.z + Random.Range(-300.0f, -250.0f);
            GameObject a = Instantiate(asteroid) as GameObject;
            a.transform.position = new Vector3(x, y, z);
            Rigidbody rigidBody = a.AddComponent<Rigidbody>();
            rigidBody.isKinematic = false;
            Vector3 direction = new Vector3(Random.Range(-0.1f,0.1f), Random.Range(-0.1f, 0.1f), 1);
            rigidBody.AddForce(direction * Random.Range(1000,2000));
        }
    }
    IEnumerator AsteroidWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnAsteroid();
        }
    }
}
