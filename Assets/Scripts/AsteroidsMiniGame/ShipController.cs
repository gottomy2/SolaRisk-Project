using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    public GameObject explosion;
    public GameObject projectilePrefab;
    public AudioClip explosionSound;
    public AudioClip laserShoot;
    AudioSource audioSource;

    private const float SECONDS_BEFORE_SCENE_CHANGE = 3f;
    private const float GAME_LENGTH = 30f;

    private Text timerText;

    public float speed = 3.0f;
    public int maxHealth = 5;
    public float bulletSpeed = 1500;
    public int maxAmmo = 5;
    public int ammoReload = 2;

    private static Difficulty difficulty;

    float horizontal;
    float vertical;

    bool isDead;
    bool canShoot;

    Rigidbody rigidBody;
    Animator animator;

    int health;
    float ammo;
    float ammoTimer = 0;

    private float gameTimer;
    private bool canSeeEnd;
    private bool hasFinished;

    void Start()
    {

        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        SceneShader.GetInstance().SetIsLighting(true);
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        isDead = false;
        canSeeEnd = false;
        hasFinished = false;
        canShoot = true;
        health = maxHealth;
        ammo = maxAmmo;
        Debug.Log("Health: " + health + "/" + maxHealth);
        StartCounting();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * -1;
        vertical = Input.GetAxis("Vertical");

        if (!isDead)
        {
            UpdateTimer();
            CheckTimer();
        }

        //DEBUG_OPTIONS();

        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            AsteroidDataHandler.GetInstance().RegisterClick();
            Launch();
        }

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        if (ammo < maxAmmo)
        {
            ammoTimer += Time.deltaTime;
            ammo += Time.deltaTime;
            UIAmmoBar.Instance.SetValue(ammo / (float) maxAmmo);
            if (ammoTimer >= ammoReload)
            {
                ammoTimer = 0;
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 position = rigidBody.position;
        position.x += speed / 2 * horizontal * Time.deltaTime;
        position.y += speed / 2 * vertical * Time.deltaTime;
        position.z += speed * (-1) * Time.deltaTime;

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

            UIHealthBar.Instance.SetValue(health / (float) maxHealth);
            CheckHealth();
            Destroy(e, 2);
        }
    }

    private void CheckHealth()
    {
        if (health <= 0 && !isDead)
        {
            TriggerDeath();
        }
    }

    private void TriggerDeath()
    {
        isDead = true;
        canShoot = false;
        AsteroidDataHandler.GetInstance().SetIsFailed(true);
        AsteroidDataHandler.GetInstance().RegisterMeasureEnd();
        StartCoroutine(TriggerSceneChange());
    }

    private void TriggerFinish()
    {
        canShoot = false;
        hasFinished = true;
        AsteroidDataHandler.GetInstance().RegisterMeasureEnd();
        StartCoroutine(TriggerSceneChange());
    }

    void Launch()
    {
        if (ammo >= 1)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, rigidBody.position + new Vector3(0, 0, -7),
                Quaternion.identity);
            projectileObject.transform.Rotate(new Vector3(90, 0, 0));

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(new Vector3(0, 0, -1), bulletSpeed);
            audioSource.PlayOneShot(laserShoot);
            ammo--;
            UIAmmoBar.Instance.SetValue(ammo / (float) maxAmmo);

            AsteroidDataHandler.GetInstance().RegisterShoot();
        }

    }

    private void StartCounting()
    {
        AsteroidDataHandler.GetInstance().RegisterMeasureStart();
        gameTimer = 0f;
    }

    private void UpdateTimer()
    {
        gameTimer += Time.deltaTime;

        timerText.text = GAME_LENGTH - gameTimer < 0f ? "0" : (GAME_LENGTH - gameTimer).ToString();
        Debug.Log("Timer: " + gameTimer);
    }

    private void CheckTimer()
    {
        if (gameTimer >= (GAME_LENGTH - 7) && !canSeeEnd)
        {
            canSeeEnd = true;
        }

        if (!isDead && gameTimer >= GAME_LENGTH)
        {
            TriggerFinish();
        }
    }

    private IEnumerator TriggerSceneChange()
    {
        yield return new WaitForSeconds(SECONDS_BEFORE_SCENE_CHANGE);
        SceneShader.GetInstance().SetShadeSpeed(2f);
        SceneShader.GetInstance().SetIsShading(true);
        yield return new WaitForSeconds(SECONDS_BEFORE_SCENE_CHANGE);
        //change scene here
    }

    public bool CanSeeEnd()
    {
        return canSeeEnd;
    }

    public static Difficulty GetDifficulty()
    {
        return difficulty;
    }

    public static void SetDifficulty(Difficulty d)
    {
        difficulty = d;
    }

    private void DEBUG_OPTIONS()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

}
