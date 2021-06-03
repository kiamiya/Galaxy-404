using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScript : MonoBehaviour
{
    public PlayerData player;
    public BonusData Bonus;
    private Transform _transform;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (player.ShieldMode != true)
        {
            if (other.gameObject.tag == "Player")
            {
                player.health += Bonus.life;
                player.updateLife();
                player.speed += Bonus.speed;
                player.updateSpeed();
                player.delayShoot += Bonus.delayShoot;
                player.updateDelay();
                player.ShieldMode = Bonus.shield;
                player.bomb += Bonus.ammo;
                player.updateAmmo();
                player.score += 20;
                Destroy(this.gameObject);
            }
            if (other.gameObject.tag == "LaserPlayer")
            {
                Bonus.health--;
                player.score -= 10;
                AudioSource audio = GetComponent<AudioSource>();

                audio.Play();
            }

        }
        
    }
    private void Start()
    {
        _transform = GetComponent<Transform>();
        Bonus.health = 3;
        Destroy(gameObject, 10);
    }
    private void Update()
    {
        _transform.Translate(Vector3.left * Time.deltaTime * 1);
        if (Bonus.health < 0)
        {
            Destroy(gameObject);
        }
    }
}
