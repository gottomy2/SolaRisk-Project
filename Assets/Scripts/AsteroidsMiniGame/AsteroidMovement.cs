using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    Vector3 scaleChange;
    Vector3 randomRotation;

    GameObject ship;

    public float rotationOffset = 100f;

    // Start is called before the first frame update
    void Awake()
    {
        ship = GameObject.FindGameObjectWithTag("Player");

        scaleChange = new Vector3(Random.Range(2f, 4f), Random.Range(2f, 4f), Random.Range(2f, 4f));
        gameObject.transform.localScale += scaleChange;

        randomRotation.x = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.y = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.z = Random.Range(-rotationOffset, rotationOffset);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(randomRotation * Time.deltaTime);

        if (gameObject.transform.position.z > ship.transform.position.z + 10 || !GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }
}