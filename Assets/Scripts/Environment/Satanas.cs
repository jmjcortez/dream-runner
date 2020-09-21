using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satanas : MonoBehaviour
{
    public float maxY, minY, speed;

    private Rigidbody2D satanasRigidBody;

    private bool isGoingDown;

    private void Start() {
        satanasRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Hover();
    }

    void Hover() {
        float step = speed * Time.deltaTime;

        if (transform.position.y < minY) {
            isGoingDown = false;
        }

        if (transform.position.y > maxY) {
            isGoingDown = true;
        }

        if (isGoingDown) {
            satanasRigidBody.velocity = new Vector2(0, -1 * speed);
        }
        else {
            satanasRigidBody.velocity = new Vector2(0, speed);
        }
    }
}
