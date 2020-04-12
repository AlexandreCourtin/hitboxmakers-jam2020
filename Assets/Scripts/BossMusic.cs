using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
    bool isPlaying = false;

    void FixedUpdate() {
        if (Game_Manager.GM.phase == 3 && !isPlaying) {
            GetComponent<AudioSource>().Play();
            isPlaying = true;
        } else if (Game_Manager.GM.phase > 3) {
            GetComponent<AudioSource>().Stop();
            Destroy(this);
        }
    }
}
