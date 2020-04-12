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
        if (Input.GetMouseButton(0) && !Game_Manager.GM.isLose) {
            if ((Time.time - t) >= projectile[power].GetComponent<Shot>().freq)
            {
                t = Time.time;
                GameObject proj = Instantiate(projectile[power]);
                proj.transform.position = transform.position;
                proj.GetComponent<Shot>().earthRotation = transform.localRotation.eulerAngles;
                GameObject.Find("Sounds").GetComponent<SoundMaker>().PlaySound(0);
            }
        } else if (Input.GetMouseButtonDown(0) && Game_Manager.GM.isLose) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
