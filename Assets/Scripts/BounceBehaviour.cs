using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBehaviour : MonoBehaviour
{

    public float bounceAmount = 20f;
    public Rigidbody playerRb;
    public EnemyController Enemy;
    public GameResult result;

    private bool bounce = false;

    void Start()
    {
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
        result = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameResult>();
    }

    void Update()
    {
        if (bounce)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0.0f, playerRb.velocity.z);
            playerRb.AddForce(0, bounceAmount, 0, ForceMode.Impulse);
            bounce = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PlayerLower")
        {
            if (playerRb.velocity.y < 0)
            {
                bounce = true;
                if (Enemy.stunned && !Enemy.dead)
                {
                    Enemy.dead = true;
                    result.score += 100;
                }
                else
                {
                    Enemy.stunned = true;
                }
            }
        }
    }
}
