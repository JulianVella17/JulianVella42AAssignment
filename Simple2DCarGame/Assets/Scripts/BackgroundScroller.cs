using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    [SerializeField] float backgroundScrollerSpeed = 0.5f;

    //the Material from the texture
    Material myMaterial;

    //movement offset
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        //get the material of the background from Render component
        myMaterial = GetComponent<Renderer>().material;

        //move in the y-axis at the given speed
        offset = new Vector2(0f, backgroundScrollerSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //move the texture of the material by offset evertframe
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}