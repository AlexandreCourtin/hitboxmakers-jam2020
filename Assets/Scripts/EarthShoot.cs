using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EarthShoot : MonoBehaviour
{
    private float t;

    public int power = 0;
    public float shotFreq = 0.3f;
    public GameObject[] projectile;

    void Update()
    {
        if (Game_Manager.GM.score > 30)
            shotFreq = 0.2f;
        if (Game_Manager.GM.score > 100)
            shotFreq = 0.17f;
        if (Game_Manager.GM.score > 250)
            shotFreq = 0.15f;
        if (Game_Manager.GM.score > 450)
            shotFreq = 0.12f;
        if (Game_Manager.GM.score > 750)
            shotFreq = 0.10f;
        if (Game_Manager.GM.score > 1000)
            shotFreq = 0.09f;
        if (Game_Manager.GM.score > 2000)
            shotFreq = 0.08f;

        // LEFT CLICK
        int life = GetComponent<EarthLogic>().life;
        if (Input.GetMouseButtonDown(0) && !Game_Manager.GM.showEnd && (Game_Manager.GM.isWin || life <= 0)) {
            // SHOW LEADERBOARD AFTER GAMEOVER
            UpdateUiForLeaderboard();
        }
        else if (Input.GetMouseButton(0) && life > 0 && !Game_Manager.GM.showEnd) {
            // SHOOT
            if ((Time.time - t) >= shotFreq)
            {
                t = Time.time;
                GameObject proj = Instantiate(projectile[power]);
                proj.transform.position = transform.position;
                proj.GetComponent<Shot>().earthRotation = transform.localRotation.eulerAngles;
                GameObject.Find("Sounds").GetComponent<SoundMaker>().PlaySound(0);
            }
        }
    }

    void UpdateUiForLeaderboard() {
        Game_Manager.GM.showEnd = true;
        GameObject.Find("DeathMenu").SetActive(false);
    }
}
