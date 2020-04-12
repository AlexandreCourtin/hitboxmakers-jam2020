using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EarthShoot : MonoBehaviour
{
    private float t;

    // projectile type
    public int power = 0; // num dans projectile []
    public GameObject[] projectile;

    void Update()
    {
        // SHOOT
        int life = GetComponent<EarthLogic>().life;
        if (Input.GetMouseButtonDown(0) && Game_Manager.GM.isWin && !Game_Manager.GM.showEnd) {
            UpdateUiForLeaderboard();
        }
        else if (Input.GetMouseButton(0) && life > 0 && !Game_Manager.GM.showEnd) {
            if ((Time.time - t) >= projectile[power].GetComponent<Shot>().freq)
            {
                t = Time.time;
                GameObject proj = Instantiate(projectile[power]);
                proj.transform.position = transform.position;
                proj.GetComponent<Shot>().earthRotation = transform.localRotation.eulerAngles;
                GameObject.Find("Sounds").GetComponent<SoundMaker>().PlaySound(0);
            }
        } else if (Input.GetMouseButtonDown(0) && life <= 0 && !Game_Manager.GM.showEnd) {
            UpdateUiForLeaderboard();
        }
    }

    void UpdateUiForLeaderboard() {
        Game_Manager.GM.showEnd = true;
        GameObject.Find("DeathMenu").SetActive(false);
    }
}
