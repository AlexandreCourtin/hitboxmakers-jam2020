using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    public AudioSource sound_shoot;
    public AudioSource sound_explosion;
    public AudioSource sound_hit;
    public AudioSource sound_scream;

    public void PlaySound(int i) {
        if (i == 0) sound_shoot.Play();
        else if (i == 1) sound_explosion.Play();
        else if (i == 2) sound_hit.Play();
        else if (i == 3) sound_scream.Play();
    }
}
