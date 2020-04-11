using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class AsteroidSpawner : MonoBehaviour
{
    public int angleDispertion = 50;
    public float gameTimer;
    public GameObject asteroidObj;

    float radius = 9f;

    void Start() {
        gameTimer = 0f;

        StartCoroutine(BeginSpawn());
    }

    void FixedUpdate() {
        if (gameTimer < 120f) gameTimer += Time.fixedDeltaTime;
    }

    void SpawnAsteroid(int angle) {
        Vector3 pos = new Vector3(radius * Mathf.Sin(angle * 1.8f * Mathf.Deg2Rad), radius * Mathf.Cos(angle * 1.8f * Mathf.Deg2Rad), 0f);
        GameObject obj = Instantiate(asteroidObj, pos, Quaternion.identity);

        // RANDOM ANGLE
        obj.transform.eulerAngles = new Vector3(0f, 0f, -(angle + Random.Range(-angleDispertion, angleDispertion)) * 1.8f);
        obj.GetComponent<AsteroidLogic>().direction = -obj.transform.up;
        obj.transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));

        // RANDOM SCALE
        float xScale = Random.Range(1f, 1f + gameTimer * .03f);
        obj.transform.localScale = new Vector3(xScale, 1f, 1f);
        obj.transform.Find("EarthSprite").transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));
        obj.GetComponentInChildren<Light2D>().pointLightOuterRadius = xScale * 1.5f;
        // obj.GetComponentInChildren<Light2D>().pointLightInnerRadius = xScale * .25f;

        // RANDOM SPEED
        float speed = 1f + gameTimer * .05f;
        obj.GetComponent<AsteroidLogic>().speed = speed;
        obj.GetComponent<AsteroidLogic>().rotSpeed = Random.Range(-speed * .25f, speed * .25f);
    }

    IEnumerator BeginSpawn() {
        while(true) {
            if (gameTimer > 2f) {
                SpawnAsteroid(Random.Range(0, 200));
            }
            yield return new WaitForSeconds(1f - (gameTimer * .004f));
        }
    }
}
