using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEnemy : MonoBehaviour
{
    public EnemyData enemy;
    public PlayerData player;
    
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            enemy.health -= player.damage;
            player.health -= enemy.damage;
        }
        if (other.gameObject.tag == "LaserPlayer")
        {
            this.enemy.health--;
        }

    }
}
