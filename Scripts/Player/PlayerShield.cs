using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    public PlayerData player;
    public ShieldData shield;
    private void Start()
    {
        shield.health = 1;
    }
    void OnCollisionEnter2D(Collision2D other)
    {

    
        if (other.gameObject.tag == "LaserEnemy")
        {
            shield.health--;
            shield.updateShield();
            Destroy(other.gameObject);
        }
    }
    private void Update()
    {
        if (shield.health <1)
        {
            player.ShieldMode = false;
            Destroy(gameObject);
        }   
    }
}
