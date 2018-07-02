using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 2f;
    private float xMin;
    private float xMax;
    public float padding;
    private bool direction = true;
    // Use this for initialization
    void Start ()
    {
        padding = width / 2f - 0.5f;
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftmost.x + padding;
        xMax = rightmost.x - padding;
        SpawnEnemies();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Update is called once per frame
    void Update()
    {
        if (AllMembersDead())
        {
            SpawnEnemies();
        }
        if (direction)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if(transform.position.x < xMin)
        {
            direction = false;
        }
        else if(transform.position.x > xMax)
        {
            direction = true;
        }
    }

    private bool AllMembersDead()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if(childPositionGameObject.childCount >0)
            {
                return false;
            }
        }
        return true;
    }

    private void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity);
            enemy.transform.parent = child;
        }
    }
}
