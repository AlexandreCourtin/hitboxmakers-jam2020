using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            Game_Manager.GM.isWin = true;
            GameObject.Find("DeathMenu").GetComponent<Animator>().SetBool("isDead", true);
            GameObject.Find("tein").GetComponent<Text>().text = "The End is No More !";
            Game_Manager.GM.destroyAllAsteroids();
        }
    }
}
