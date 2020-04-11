using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLogic : MonoBehaviour
{
    [HideInInspector]
    public float speed = 1f;
    [HideInInspector]
    public float rotSpeed = 1f;
    public int life = 3;

    public Vector3 direction = Vector3.up;
    public float boundLimit = 7f;

    void FixedUpdate() {
        transform.position = transform.position + (direction.normalized * speed * Time.fixedDeltaTime);
        transform.Rotate(0f, 0f, rotSpeed, Space.Self);
        CheckBounds();
    }

    void CheckBounds() {
        // X
        if (transform.position.x > boundLimit) {
            Destroy(this.gameObject);
        } else if (transform.position.x < -boundLimit) {
            Destroy(this.gameObject);
        }
        // Y
        if (transform.position.y > boundLimit) {
            Destroy(this.gameObject);
        } else if (transform.position.y < -boundLimit) {
            Destroy(this.gameObject);
        }
    }

    public void TakeHit() {
        life -= 1;
        if (life <= 0) {
            GameManager.GM.updateScore(1.0f);
            Destroy(this.gameObject);
        }
    }
}
