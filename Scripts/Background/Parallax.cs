using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lenght;
    public GameObject cam;
    public float parallaxEffect;
    public float offsetX;

    void Start()
    {
       
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
       
        transform.position = new Vector3(lenght + offsetX, transform.position.y, transform.position.z);
    }

    void Update()
    {   
        float dist = transform.position.x * parallaxEffect;
        if (transform.position.x < -lenght)
        {
        transform.position = new Vector3(lenght + offsetX, transform.position.y, transform.position.z);
        }
        else
        {
            float temp = transform.position.x + (parallaxEffect*Time.deltaTime);
            transform.position = new Vector3(temp, transform.position.y, transform.position.z);
        }
    
        /*if (temp > startPos + lenght)
        {
            startPos += lenght;
        }
        else if (temp < startPos - lenght)
        {
            startPos -= lenght;
        }*/
    }
}
