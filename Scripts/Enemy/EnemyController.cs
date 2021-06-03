using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameObject SPLAlien;

    public EnemyData enemy;
    public PlayerData player;
    public LaserData laser;
    public EnemyData mine;
    public ShieldData shield;
    Transform _transform;
    private float startTime;
    private float elapsedTime;

    private int health;
    private int speed;
    private float delayShoot;
    private int lifeTime;
    private int scoreOnDestruction;
    private bool canShoot;
    private float roll;
    private int moveSpeed;
    
    void rollDice()
    {
       roll = Random.Range(0, 100);
    }

    private void Awake()
    {
        health = enemy.health;
        speed = enemy.speed;
        delayShoot = enemy.delayShoot;        
        scoreOnDestruction = enemy.scoreOnDestruction;
        canShoot = enemy.canShoot;
        lifeTime = enemy.lifeTime;
        moveSpeed = Random.Range(speed, speed*2);

    }
    void Start()
    {        
        startTime = Time.time;
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        #region ==========================MOVE==========================
       
            _transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
            Destroy(this.gameObject, lifeTime);       

        #endregion
        #region ==========================SHOOT=========================
        if (canShoot == true)
        {
            elapsedTime = Time.time - startTime;
            if (elapsedTime >= delayShoot)
            {
                startTime = Time.time;
                Instantiate(laser.prefab, SPLAlien.transform.position, Quaternion.identity);
            }
        }
        #endregion
        #region==========================DEATH=========================
        if (health <1)
        {
            #region======Mine======
            if (enemy.spawnOnDeath != null)
            {
                GameObject visuals = Instantiate(enemy.spawnOnDeath);
                visuals.transform.localPosition = transform.position;
                visuals.transform.rotation = Quaternion.identity;
            }
            #endregion
            #region======Loot======
            if (enemy.loot != null)
            {
         
                rollDice();
                if (enemy.lootChance >= 1)
                {
                    if (roll >50)
                    {
                        rollDice();
                        if (roll >= 95 && roll <= 100)
                        {
                            GameObject visuals = Instantiate(enemy.loot.life);
                            visuals.transform.localPosition = transform.position;
                            visuals.transform.rotation = Quaternion.identity;
                        }
                        else if (roll >= 85 && roll < 95)
                        {
                            GameObject visuals = Instantiate(enemy.loot.speed);
                            visuals.transform.localPosition = transform.position;
                            visuals.transform.rotation = Quaternion.identity;
                        }
                        else if (roll >= 75 && roll < 85)
                        {
                            GameObject visuals = Instantiate(enemy.loot.delayShoot);
                            visuals.transform.localPosition = transform.position;
                            visuals.transform.rotation = Quaternion.identity;
                        }
                        else if (roll >= 60 && roll < 70)
                        {
                            GameObject visuals = Instantiate(enemy.loot.ammo);
                            visuals.transform.localPosition = transform.position;
                            visuals.transform.rotation = Quaternion.identity;
                        }
                        /*else if (rollDice >= 81 && rollDice < 86 )
                        {
                            GameObject visuals = Instantiate(enemy.loot.shield);
                            visuals.transform.localPosition = transform.position;
                            visuals.transform.rotation = Quaternion.identity;
                        }*/
                    }
                    

                }
                else if (enemy.lootChance < 1)
                {
                    rollDice();
                    if (roll > 50)
                    {
                        rollDice();
                        if (roll >= 95 && roll < 100)
                        {
                            GameObject visuals = Instantiate(enemy.loot.delayShoot);
                            visuals.transform.localPosition = transform.position;
                            visuals.transform.rotation = Quaternion.identity;
                        }
                        else if (roll >= 85 && roll < 90)
                        {
                            GameObject visuals = Instantiate(enemy.loot.speed);
                            visuals.transform.localPosition = transform.position;
                            visuals.transform.rotation = Quaternion.identity;
                        }
                        else if (roll >= 75 && roll < 80)
                        {
                            GameObject visuals = Instantiate(enemy.loot.ammo);
                            visuals.transform.localPosition = transform.position;
                            visuals.transform.rotation = Quaternion.identity;
                        }
                    }
                }

            }
            #endregion
            player.score += scoreOnDestruction;            
            Destroy(gameObject);
           
        }
        #endregion
    }
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            health -= 5;
            player.health -= enemy.damage;
        }
        if (other.gameObject.tag == "LaserPlayer")
        {
          
            health--;
            Destroy(other.gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            health -= 5;
            player.health -= enemy.damage;
        }
        if (other.gameObject.tag == "LaserPlayer")
        {
            health--;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "PlayerShield")
        {
            shield.health--;
            shield.updateShield();
            health -= 5;
        }
    }
}
