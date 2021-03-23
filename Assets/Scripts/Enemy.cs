using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int health = 300;
    [SerializeField] int points = 100;

    [Header("Shoot")]
    [SerializeField] Object laserPrefab;
    [SerializeField] float minShootingRate = 0.3f;
    [SerializeField] float maxShootingRate = 3f;
    [SerializeField] float laserSpeed = 5f;

    [Header("FXs")]
    [SerializeField] GameObject explosionVFX;
    [SerializeField] AudioClip laserSFX;
    [SerializeField] float laserVolume;
    [SerializeField] AudioClip dieSFX;
    [SerializeField] float dieVolume;

    float shotCounter;
    GameSession gameSession;

    private void Start()
    {
        shotCounter = Random.Range(minShootingRate, maxShootingRate);
        gameSession = FindObjectOfType<GameSession>();
    }

    private void Update()
    {
        CountAndShoot();
    }

    private void CountAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minShootingRate, maxShootingRate);
        }
    }

    private void Fire()
    {
        GameObject laserClone = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laserClone.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
        AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserVolume);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameSession.AddToScore(points);
        Destroy(gameObject);
        GameObject explosionVFXClone = Instantiate(explosionVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosionVFXClone, 0.25f);
        AudioSource.PlayClipAtPoint(dieSFX, Camera.main.transform.position, dieVolume);
    }
}
