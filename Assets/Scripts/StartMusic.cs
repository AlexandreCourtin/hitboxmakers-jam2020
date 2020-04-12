using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusic : MonoBehaviour
{
    void FixedUpdate() {
        if (Game_Manager.GM.phase > 1) {
            GetComponent<AudioSource>().Stop();
            Destroy(this);
        }
    }
}
