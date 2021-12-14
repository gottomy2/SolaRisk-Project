using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rigidBody;
    public ParticleSystem hitParticle;
    public AudioClip laserHit;
    AudioSource audioSource;

    float zMax;

    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        zMax = rigidBody.position.z-100;
        audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidBody.position.z<zMax)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector3 direction, float force)
    {
        rigidBody.AddForce(direction * force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Fracture asteroid = other.GetComponent<Fracture>();
            asteroid.FractureObject();

            ParticleSystem e = Instantiate(hitParticle, transform.position, transform.rotation);

            audioSource.PlayOneShot(laserHit);

            Destroy(e, 2);
            Destroy(gameObject);
        }
    }
}
