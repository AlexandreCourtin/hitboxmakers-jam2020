using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class AsteroidLogic : MonoBehaviour
{
    [HideInInspector]
    public float speed = 1f;
    [HideInInspector]
    public float rotSpeed = 1f;

    public GameObject astExpl;
    public int life = 3;
    public bool isDarkroid = false;

    public Vector3 direction = Vector3.up;
    public float boundLimit = 7f;

    bool isGoingForBoss = false;

    void FixedUpdate() {
        if (!isGoingForBoss) {
            transform.position = transform.position + (direction.normalized * speed * Time.fixedDeltaTime);
        } else {
            float t = Time.fixedDeltaTime * 10f;
            Vector3 pos = GameObject.Find("FinalBossMark").transform.position;
            transform.position = Vector3.MoveTowards(transform.position, pos, t);

            if (Vector3.Distance(transform.position, pos) < 1f) {
                GameObject.Find("Sounds").GetComponent<SoundMaker>().PlaySound(1);
                GameObject.Find("FinalBoss").GetComponent<BossFight>().isHit();
                Destroy(this.gameObject);
            }
        }
        transform.Rotate(0f, 0f, rotSpeed, Space.Self);

        if (GetComponentInChildren<Light2D>().intensity > 2) {
            GetComponentInChildren<Light2D>().intensity -= Time.fixedDeltaTime * 50f;
        } else if (GetComponentInChildren<Light2D>().intensity < 2) {
            GetComponentInChildren<Light2D>().intensity = 2;
        }
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
            if (!isDarkroid) {
                Game_Manager.GM.updateScore(1.0f);
                GameObject.Find("Sounds").GetComponent<SoundMaker>().PlaySound(1);
                GameObject effect = Instantiate(astExpl, transform.position, Quaternion.identity);
                effect.transform.eulerAngles = new Vector3(-90f, 0f, 0f);
                Destroy(this.gameObject);
            } else if (!isGoingForBoss) {
                Game_Manager.GM.updateScore(1.5f);
                GameObject.Find("Sounds").GetComponent<SoundMaker>().PlaySound(2);
                GetComponentInChildren<ParticleSystem>().Play();
                GetComponentInChildren<Renderer>().enabled = false;
                isGoingForBoss = true;
            }
        } else {
            GameObject.Find("Sounds").GetComponent<SoundMaker>().PlaySound(2);
            GetComponentInChildren<Light2D>().intensity = 10;
        }
    }
}
