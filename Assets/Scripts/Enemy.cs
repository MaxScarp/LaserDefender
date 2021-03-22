using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Object laserPrefab;
    [SerializeField] int health = 300;
    [SerializeField] float minShootingRate = 0.3f;
    [SerializeField] float maxShootingRate = 3f;
    [SerializeField] float laserSpeed = 5f;
    [SerializeField] GameObject explosionVFX;

    float shotCounter;

    private void Start()
    {
        shotCounter = Random.Range(minShootingRate, maxShootingRate);
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
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
        Destroy(gameObject);
        GameObject explosionVFXClone = Instantiate(explosionVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosionVFXClone, 0.25f);
    }
}
