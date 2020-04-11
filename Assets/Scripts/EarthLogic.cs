using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthLogic : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 10.0f;
    public int life = 5;
    private float x = 0;
    private float y = 0;
    Vector3 trans;
    Vector2 screenBounds;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update() {
        if (life > 0) {
            // MOVE
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                x -= 1f;
            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                x += 1f;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                y += 1f;
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                y -= 1f;
            trans = new Vector3(x, y, 0);

            x = 0;
            y = 0;

            Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }

    void FixedUpdate()
    {
        float mvtX = Mathf.Clamp(transform.position.x - trans.x * speed * Time.fixedDeltaTime, - screenBounds.x, screenBounds.x);
        float mvtY = Mathf.Clamp(transform.position.y - trans.y * speed * Time.fixedDeltaTime, - screenBounds.y, screenBounds.y);
        Vector3 tmp = new Vector3(mvtX, mvtY, 0);
        rb.MovePosition(tmp);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "asteroid") {
            Destroy(other.gameObject);
            
            life -= 1;
            if (life < 1) {
                GameObject.Find("Heart1").SetActive(false);
<<<<<<< HEAD
                this.gameObject.SetActive(false);
=======
                GameObject.Find("Sounds").GetComponent<SoundMaker>().PlaySound(1);
                transform.Find("EarthSprite").gameObject.SetActive(false);
            } else {
                GameObject.Find("Sounds").GetComponent<SoundMaker>().PlaySound(2);
                if (life < 2) GameObject.Find("Heart2").SetActive(false);
                else if (life < 3) GameObject.Find("Heart3").SetActive(false);
                else if (life < 4) GameObject.Find("Heart4").SetActive(false);
                else if (life < 5) GameObject.Find("Heart5").SetActive(false);
>>>>>>> fec129828842f875ea32da9331ab0f41650ee01b
            }
        }
    }
}
