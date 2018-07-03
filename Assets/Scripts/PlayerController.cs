using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public bool invicible = false;
    public int hitPoints = 20;
    public float speed = 15.0f;
    float xMin;
    float xMax;
    public float padding = 0.5f;
    public float rateOfFire = 0.2f;
    public GameObject projectile;
    public ParticleSystem engine;
    public AudioClip fire;
    public AudioClip explode;

    private LevelManager levelManager;
    private EnemyLaser incoming;

	// Use this for initialization
	void Start ()
    {
        ScoreKeeper.Reset();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distance));
        xMin = leftmost.x + padding;
        xMax = rightmost.x - padding;       
	}

    void Fire()
    {
        AudioSource.PlayClipAtPoint(fire, transform.position);
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, rateOfFire);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {

        }
        // restricting playyer to gamespace
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyLaser>())
        {
            incoming = collision.gameObject.GetComponent<EnemyLaser>();
            Debug.Log("Player HIT!!");
            if (!invicible)
            {
                TakeDamage();
            }
        }
    }

    private void TakeDamage()
    {
        hitPoints = hitPoints - incoming.getDamage();
        incoming.hit();
        if (hitPoints <= 0)
        {
            DestroyShip();
        }
    }

    private void DestroyShip()
    {
        AudioSource.PlayClipAtPoint(explode, transform.position);
        Destroy(this.gameObject);
        levelManager.LoadLevel("Lose");
    }
}
