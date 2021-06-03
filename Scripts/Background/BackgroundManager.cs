using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{   
    private float speed = 0.01f;
    private MeshRenderer rend;
    
    // Start is called before the first frame update
    void Start()
    {
       rend = GetComponent<MeshRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * speed;
        rend.material.mainTextureOffset = new Vector2(offset,0);
     
        //transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}
