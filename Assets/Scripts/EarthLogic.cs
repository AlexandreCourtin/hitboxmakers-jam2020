using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthLogic : MonoBehaviour
{
    public GameObject earthExpl;
    public Rigidbody2D rb;
    public float speed = 10.0f;
    public int life = 5;
    Vector3 movements;
    Vector2 screenBounds;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update() {
        if (life > 0) {
            // MOVEMENTS
            movements = Vector3.zero;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                movements.x -= 1f;
            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                movements.x += 1f;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                movements.y += 1f;
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                movements.y -= 1f;

            Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        float mvtX;
        float mvtY = Mathf.Clamp(transform.position.y - movements.y * speed * Time.fixedDeltaTime, - screenBounds.y, screenBounds.y);

        if (Game_Manager.GM.phase == 1) {
            mvtX = Mathf.Clamp(transform.position.x - movements.x * speed * Time.fixedDeltaTime, - screenBounds.x, screenBounds.x);
        } else {
            mvtX = Mathf.Clamp(transform.position.x - movements.x * speed * Time.fixedDeltaTime, - screenBounds.x, 0f);
        }
        Vector3 tmp = new Vector3(mvtX, mvtY, 0);
        rb.MovePosition(tmp);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "asteroid") { // IF EARTH TOUCH ASTEROID
            Destroy(other.gameObject);

            life -= 1;
            if (life < 1) { // FATAL HIT
                GameObject.Find("Sounds").GetComponent<SoundMaker>().PlaySound(1);
                transform.Find("EarthSprite").gameObject.SetActive(false);
                GameObject effect = Instantiate(earthExpl, transform.position, Quaternion.identity);
                effect.transform.eulerAngles = new Vector3(-90f, 0f, 0f);
                GetComponent<Collider2D>().enabled = false;
                GameObject.Find("DeathMenu").GetComponent<Animator>().SetBool("isDead", true);
                GameObject.Find("FinalBoss").GetComponent<Animator>().enabled = false;
                GameObject.Find("EndText").SetActive(false);
                UpdateLifeBar();
            } else { // NORMAL HIT
                GameObject.Find("Sounds").GetComponent<SoundMaker>().PlaySound(2);
                UpdateLifeBar();
            }
        }
    }

    void UpdateLifeBar() {
        GameObject.Find("Heart1").GetComponent<Image>().enabled = !(life < 1);
        GameObject.Find("Heart2").GetComponent<Image>().enabled = !(life < 2);
        GameObject.Find("Heart3").GetComponent<Image>().enabled = !(life < 3);
        GameObject.Find("Heart4").GetComponent<Image>().enabled = !(life < 4);
        GameObject.Find("Heart5").GetComponent<Image>().enabled = !(life < 5);
    }
}
