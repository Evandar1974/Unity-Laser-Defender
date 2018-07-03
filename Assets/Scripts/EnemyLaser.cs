using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour {
    public float speed = 10f;
    public int damage = 1;
    private float yMin;
    // Use this for initialization
    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 bottomMost = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distance));
        yMin = bottomMost.y;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if (transform.position.y <= yMin)
        {
            DestroyImmediate(this.gameObject);
        }
    }

    public int getDamage()
    {
        return damage;
    }

    public void hit()
    {
        Destroy(this.gameObject);
    }
}
