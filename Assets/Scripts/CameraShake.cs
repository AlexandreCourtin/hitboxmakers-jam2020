using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    float timer = 1.5f;
    bool shake = false;
    Camera cam;

    void Start() {
        cam = Camera.main;
    }

    void FixedUpdate() {
        if (shake && timer > 0f) {
            timer -= Time.fixedDeltaTime;
            cam.transform.position = new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), -10f);
        } else if (shake) {
            cam.transform.position = new Vector3(0f, 0f, -10f);
            Destroy(this);
        }
    }

    public void shakeCamera() {
        shake = true;
    }
}
