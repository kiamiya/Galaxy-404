using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAlienMove : MonoBehaviour
{
    public LaserData laser;
    Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>(); // récup les coordonnées du Laser
    }

    // Update is called once per frame
    void Update()
    {
        _transform.Translate(Vector3.left * Time.deltaTime * laser.speed); // fait avancer le laser vers la droite
        Destroy(this.gameObject, 6f); // détruit le laser au bout de 3s
    }
}
