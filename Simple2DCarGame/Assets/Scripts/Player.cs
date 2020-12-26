using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        var deltaX = Input.GetAxis("Horizontal");

        //newXPos = current x-position  + difference in x
        var newXpos = transform.position.x + deltaX;
        
        //Move the Player car to newXPos
        this.transform.position = new Vector2(newXpos, transform.position.y);

    }
}
