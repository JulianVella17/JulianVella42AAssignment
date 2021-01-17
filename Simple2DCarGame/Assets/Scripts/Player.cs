using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float health = 100f;

    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float padding = 0.5f;

    [SerializeField] AudioClip playerCollisionSound;
    //allows to change volume accordingly from 0 to 1
    [SerializeField] [Range(0, 1)] float playerCollisionSoundVolume = 0.75f;

    [SerializeField] AudioClip backgroundSound;
    //allows to change volume accordingly from 0 to 1
    [SerializeField] [Range(0, 1)] float backgroundSoundVolume = 0.75f;

    float xMin, xMax;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(backgroundSound, Camera.main.transform.position, backgroundSoundVolume);
        SetUpMoveBoundaries();
    }

    //set up boundaries of camera
    private void SetUpMoveBoundaries()
    {
        //get the camera from unity
        Camera gameCamera = Camera.main;

        //xMin = 0 xMax = 1
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    //Moves player
    private void Move()
    {
       //var changes its variable type
        //depending on what I save in it
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

        //newXPos = current x-position  + difference in x
        var newXpos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        
        //Move the Player car to newXPos
        this.transform.position = new Vector2(newXpos, transform.position.y);

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

        AudioSource.PlayClipAtPoint(playerCollisionSound, Camera.main.transform.position, playerCollisionSoundVolume);
    }

    private void ProcessHit(DamageDealer dmgDealer)
    {
        health -= dmgDealer.GetDamage();

        if (health <= 0)
        {
            Destroy(gameObject);

            FindObjectOfType<Level>().LoadGameOver();
        }
    }
}
