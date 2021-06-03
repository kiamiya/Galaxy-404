using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovePlayer : MonoBehaviour
{
    public LaserData laser;
    private Transform _transform;
    
    void Start()
    {
        _transform = GetComponent<Transform>();    
    }

    // Update is called once per frame
    void Update()
    {
        _transform.Translate(Vector3.right * Time.deltaTime * laser.speed);
        Destroy(this.gameObject, 1f);
    }
}
