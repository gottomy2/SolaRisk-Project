using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public GameObject explosion;
    public GameObject projectilePrefab;
    public AudioClip explosionSound;
    public AudioClip laserShoot;
    AudioSource audioSource;

    public float speed = 3.0f;
    public int maxHealth = 5;
    public float bulletSpeed = 1500;
    public int maxAmmo = 5;
    public int ammoReload = 2;
    
    float horizontal;
    float vertical;
    

    Rigidbody rigidBody;
    Animator animator;

    int health;
    float ammo;
    float ammoTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        health = maxHealth;
        ammo = maxAmmo;
        Debug.Log("Health: " + health + "/" + maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal")*-1;
        vertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        if (ammo < maxAmmo)
        {
            ammoTimer += Time.deltaTime;
            ammo += Time.deltaTime;
            UIAmmoBar.Instance.SetValue(ammo / (float)maxAmmo);
            if (ammoTimer >= ammoReload)
            {
                ammoTimer = 0;
            }
        }
    }
    void FixedUpdate()
    {
        Vector3 position = rigidBody.position;
        position.x = position.x + speed/2 * horizontal * Time.deltaTime;
        position.y = position.y + speed/2 * vertical * Time.deltaTime;
        position.z = position.z + speed * (-1) * Time.deltaTime;

        rigidBody.MovePosition(position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            GameObject e = Instantiate(explosion);
            e.transform.position = other.transform.position;

            audioSource.PlayOneShot(explosionSound);

            Fracture asteroid = other.gameObject.GetComponent<Fracture>();
            asteroid.FractureObject();
            health--;

            UIHealthBar.Instance.SetValue(health / (float)maxHealth);

            Destroy(e, 2);
        }
    }
    void Launch()
    {
        if (ammo >= 1)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, rigidBody.position + new Vector3(0, 0, -7), Quaternion.identity);
            projectileObject.transform.Rotate(new Vector3(90, 0, 0));

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(new Vector3(0, 0, -1), bulletSpeed);
            audioSource.PlayOneShot(laserShoot);
            ammo--;
            UIAmmoBar.Instance.SetValue(ammo / (float)maxAmmo);
        }
        

    }
}
