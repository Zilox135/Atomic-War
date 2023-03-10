using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
   [SerializeField] GameObject deathFX;
   [SerializeField] GameObject hitVFX;
   [SerializeField] int scorePerHit = 1;
   [SerializeField] int hitPoints = 20;

   ScoreBoard scoreBoard;
   GameObject parentGameObject;

   void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        //AddRigidbody();
    }

    private void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnParticleCollision()
    {
        ProcessHit();
        KillEnemy();
    }

    private void KillEnemy()
    {
        hitPoints --;

    if (hitPoints < 1)
        {
            GameObject vfx = Instantiate(deathFX, transform.localPosition, Quaternion.identity);
            vfx.transform.parent = parentGameObject.transform;
            scoreBoard.UpdateScore(scorePerHit);
            Destroy(gameObject);

        }
    }

    private void ProcessHit()
    {
        GameObject fx = Instantiate(hitVFX, transform.localPosition,  Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;
    }
}
