using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float shotCounter;

    [SerializeField] float minTimeBetweenShots = 0.2f;

    [SerializeField] float maxTimeBetweenShots = 3f;

    [SerializeField] GameObject enemyLaserPrefab;

    [SerializeField] float enemyLaserSpeed = 0.3f;

    [SerializeField] float health = 500f;

    [SerializeField] GameObject deathVisualEffects;

    [SerializeField] float explosionDuration = 1f;

    [SerializeField] int scoreValue = 5;

    // Start is called before the first frame update
    void Start()
    {
        //generate a random number
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    public void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        //every frame reduce the ammount of time for shot
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0f)
        {
            EnemyFire();

            //reset shotCounter
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);


        }
    }

    private void EnemyFire()
    {
        //spawn an enemyLaser at enemyPosition
        GameObject enemyLaser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;

        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLaserSpeed);
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        DamageDealer dmgDealer = otherObject.gameObject.GetComponent<DamageDealer>();

        //if there is no dmgDealer in otherObject, end the method
        if (!dmgDealer) //if (dmgDealer == null)
        {
            return;
        }

        ProcessHit(dmgDealer);
    }

    private void ProcessHit(DamageDealer dmgDealer)
    {
        health -= dmgDealer.GetDamage();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVisualEffects, transform.position, Quaternion.identity);

        Destroy(explosion, explosionDuration);
    }
}