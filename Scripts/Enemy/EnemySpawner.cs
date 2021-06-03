using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Vector3 spawnRange;
    public int ennemyParVague;

    public float waveTime;
    public float spawnTime;
    public EnemyData enemy;
    public EnemyData enemy_2;
    public bool isIngame = false;
    private float roll;
    
    void Start()
    {
        Debug.Log(isIngame);
        StartCoroutine(countdown());
       
    }

    IEnumerator generateWave(EnemyData data)
    {
        Debug.Log(isIngame);
        while (isIngame)
        {
            waveTime = Random.Range(4, 12);
            
            
            if (enemy_2 == null)
            {
                for (int i = 0; i < ennemyParVague; i++)
                {
            
                GameObject visuals = Instantiate(enemy.prefab);
                visuals.transform.SetParent(transform);
                visuals.transform.localPosition = Vector3.zero;
                visuals.transform.rotation = Quaternion.identity;

                yield return new WaitForSeconds(spawnTime);
                }

            }
            else if (enemy_2 != null && roll <5)
            {
                for (int i = 0; i < ennemyParVague; i++)
                {

                    GameObject visuals = Instantiate(enemy_2.prefab);
                    visuals.transform.SetParent(transform);
                    visuals.transform.localPosition = Vector3.zero;
                    visuals.transform.rotation = Quaternion.identity;

                    yield return new WaitForSeconds(spawnTime);
                }
            }
            else if (enemy_2 != null && roll >=5)
            {
                GameObject visuals = Instantiate(enemy.prefab);
                visuals.transform.SetParent(transform);
                visuals.transform.localPosition = Vector3.zero;
                visuals.transform.rotation = Quaternion.identity;

                yield return new WaitForSeconds(spawnTime);
            }
        yield return new WaitForSeconds(waveTime);
        }

    }
    IEnumerator countdown()
    {
        roll = Random.Range(0,8);
        for (int i = 0; i < roll; i++)
        {
            yield return new WaitForSeconds(1);
        }
        isIngame = true;
        StartCoroutine(generateWave(enemy));        
    }

}
