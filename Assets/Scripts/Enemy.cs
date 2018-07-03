using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int pointsValue = 100;
    public GameObject projectile;
    public float shotsPerSecond = 0.5f;
    private float shotDelay;
    public int hitPoints = 2;
    private PlayerLaser incoming;
    private ScoreKeeper scoreKeeper;
  	// Use this for initialization
	void Start ()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float probability = Time.deltaTime * shotsPerSecond;
        if (Random.value < probability)
        {
            Fire();
        }
    }

    private void Fire()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerLaser>())
        {
            incoming = collision.gameObject.GetComponent<PlayerLaser>();
            Debug.Log("HIT!!");
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        hitPoints = hitPoints - incoming.getDamage();
        incoming.hit();
        if(hitPoints <=0)
        {
            DestroyShip();
        }
    }

    private void DestroyShip()
    {
        scoreKeeper.Score(pointsValue);
        Destroy(this.gameObject);       
    }
}
