using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    Vector2 screenBounds;
    public Rigidbody2D rb;

    public float freq;
    public float speed;
    public float shotLength;
    public Vector3 earthRotation;
    public Vector2 direction;
    public Vector2 target;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb = GetComponent<Rigidbody2D>();
        transform.Rotate(new Vector3(0, 0, 90 + earthRotation.z), Space.Self);
        direction = (target - new Vector2(transform.position.x, transform.position.y)).normalized;
    }

    void Update()
    {
        if (transform.position.x < -screenBounds.x ||
            transform.position.x > screenBounds.x ||
            transform.position.y < -screenBounds.y ||
            transform.position.y > screenBounds.y)
        {
            Destroy(transform.gameObject);
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + direction * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "asteroid") {
            other.gameObject.GetComponent<AsteroidLogic>().TakeHit();
            Destroy(this.gameObject);
        }
    }
}
