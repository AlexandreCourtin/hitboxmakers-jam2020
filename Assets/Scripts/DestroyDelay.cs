﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelay : MonoBehaviour
{
    public float delay;

    void Start() {
        Destroy(this.gameObject, delay);
    }
}
