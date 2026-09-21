using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosion;

    private float speed = 0f;
    private float yspeed = 0f;
    private float mass = 10f;
    private float force = 100f;
    private float drag = 1f;
    private float gravity = -9.8f;
    private float gAcell;
    private float acceleration;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        acceleration = force / mass;
        speed += acceleration * 1;
        gAcell = gravity / mass;
    }

    void LateUpdate()
    {
        speed *= (1 - Time.deltaTime * drag);
        yspeed += gAcell * Time.deltaTime;
        transform.Translate(0, yspeed , speed * Time.deltaTime);
    }
}
