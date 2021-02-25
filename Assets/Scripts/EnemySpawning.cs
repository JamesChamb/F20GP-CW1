using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{

    public GameObject enemy;
    public float xPos;
    public float zPos;
    public int enemySpawned;
    public int enemyLimit;

    void Start()
    {
        enemyLimit = Random.Range(13, 20);
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (enemySpawned < enemyLimit)
        {
            xPos = Random.Range(22, 37);
            zPos = Random.Range(35, 49);
            Instantiate(enemy, new Vector3(xPos, 1, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemySpawned += 1;
        }
    }
}
