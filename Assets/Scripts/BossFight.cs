using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public int life = 5;

    void FixedUpdate() {
        if (GetComponent<Animator>().GetBool("isHit")) {
            GetComponent<Animator>().SetBool("isHit", false);
        }
    }

    public void isHit() {
        life -= 1;

        if (life > 0) {
            GetComponent<Animator>().SetBool("isHit", true);
        } else {
            GetComponent<Animator>().SetBool("isDead", true);
            Game_Manager.GM.phase = 4;
        }
    }
}
