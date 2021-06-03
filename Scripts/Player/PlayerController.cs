using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject SPL;

    public PlayerData player;
    public LaserData Laser;
    public ShieldData shield;
    private UIController gui;
    
    Transform _transform;

    private GameObject[] enemy;
    private GameObject[] tirs;

    private float _nxtShotTime;
    private Camera cam;
    private Animator shake;
    public Vector3 Point;

    private void CamCheck()
    {
        cam = Camera.main;
        Point = cam.ScreenToWorldPoint(new Vector3());
        shake = cam.GetComponent<Animator>();
    }
    private void Awake()
    {
        CamCheck();
    }
    void Start()
    {
        _transform = GetComponent<Transform>();
        gui = GameObject.Find("Interface").GetComponent<UIController>();
        gui.UpdateLife(player.health);
        gui.UpdateAmmo(player.bomb);
        gui.UpdateScore(player.score);
        player.limitPosX = -Point.x-0.5f;
        player.limitPosY = -Point.y;
    }

    
    void Update()
    {
        //Cheat();
        #region=======================DontKillMe=======================
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "GameOver")
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion
        #region ==========================Move==========================

        _transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * player.speed);
        _transform.position = new Vector3(Mathf.Clamp(_transform.position.x, -player.limitPosX, player.limitPosX), Mathf.Clamp(_transform.position.y, -player.limitPosY, player.limitPosY), _transform.position.z);

        #endregion
        #region ==========================Shoot=========================
        #region ===========================Ammo==========================
        _nxtShotTime += Time.deltaTime;
        if (Input.GetButtonDown("Fire1"))
        {
            CamCheck();
            if (player.bomb > 0)
            {
                shake.SetTrigger("Shake");
                enemy = GameObject.FindGameObjectsWithTag("Enemy");
                foreach(var gameObject in enemy)
                {
                    Destroy(gameObject);
                }
                tirs = GameObject.FindGameObjectsWithTag("LaserEnemy");
                foreach (var gameObject in tirs)
                {
                    Destroy(gameObject);
                }
                
                player.bomb--;
                gui.UpdateAmmo(player.bomb);
            }
        }
        #endregion
        #region ==========================Laser==========================
        if (Input.GetButton("Fire2") && _nxtShotTime >= player.delayShoot)
        {
            FireBullet();
            _nxtShotTime = 0;
        }
        #endregion
        #endregion
        #region==========================Death=========================
        
        if (player.health < 1)
        {
            Destroy(gameObject);
            Loader.Load(Loader.Scene.GameOver);
        }
        #endregion
        #region=======================Score&Save=======================
        gui = GameObject.Find("Interface").GetComponent<UIController>();
       
        gui.UpdateLife(player.health);
        gui.UpdateAmmo(player.bomb);
        gui.UpdateScore(player.score);

        #endregion
        #region=========================Shield=========================
        /*if (player.ShieldMode == true)
        {
            if (shield.health == 0)
            {
                shield.health++;
                GameObject visuals = Instantiate(player.shield.prefab);
                visuals.transform.localPosition = Vector3.zero;
            }
            
            //visuals.transform.SetParent(transform);
            //shield.updateShield();            
           // Debug.Log(shield.health);
        }*/
        #endregion
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            //player.health--;
            player.speed--;
            player.delayShoot += 0.1f;
            gui.UpdateLife(player.health);
            AudioSource audio = GetComponent<AudioSource>();
           
            audio.Play();
        }
        if (other.gameObject.tag == "LaserEnemy")
        {

            player.health--;
            player.speed--;
            player.delayShoot += 0.1f;
            Destroy(other.gameObject);
            gui.UpdateLife(player.health);
            AudioSource audio = GetComponent<AudioSource>();

            audio.Play();
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Mine")
        {
            player.health -= 2;
            player.speed--;
            player.delayShoot += 0.1f;
            gui.UpdateLife(player.health);
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();

        }
    }
    
    private void FireBullet()
    {
        Instantiate(Laser.prefab, SPL.transform.position, Quaternion.identity);
    }
    private void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Loader.Load(Loader.Scene.Level_1_2);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Loader.Load(Loader.Scene.Level_1_3);
        }
    }
}
