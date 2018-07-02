using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour {
    public float speed = 10f;
    public int damage = 1;
    private float yMax;
    // Use this for initialization
    void Start ()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 topMost = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, distance));
        yMax = topMost.y;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        if(transform.position.y >= yMax)
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
