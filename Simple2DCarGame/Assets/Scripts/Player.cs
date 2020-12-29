using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float test = 5f;

    [SerializeField] float moveSpeed = 20f;

    float xMin, xMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    //set up boundaries of camera
    private void SetUpMoveBoundaries()
    {
        //get the camera from unity
        Camera gameCamera = Camera.main;

        //xMin = 0 xMax = 1
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

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
}
