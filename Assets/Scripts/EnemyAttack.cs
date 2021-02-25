using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public PlayerController Player;
    public bool isAttacking = false;
    public BoxCollider boxCol;

    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        boxCol.enabled = isAttacking;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Player.Health--;
            if (Player.Health <= 0)
            {
                Player.dead = true;
            }
            Player.takenHit = true;
        }
    }
} 
