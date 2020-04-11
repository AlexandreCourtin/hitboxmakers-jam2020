using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthShoot : MonoBehaviour
{
    private float t;

    // projectile type
    public int power = 0; // num dans projectile []
    public GameObject[] projectile;

    void Start()
    {
        
    }

    void Update()
    {
        // SHOOT
        if (Input.GetMouseButton(0))
        {
            if ((Time.time - t) >= projectile[power].GetComponent<Shot>().freq)
            {
                t = Time.time;
                GameObject proj = Instantiate(projectile[power]);
                proj.transform.position = transform.position;
                proj.GetComponent<Shot>().earthRotation = transform.localRotation.eulerAngles;
            }
        }
    }
}
