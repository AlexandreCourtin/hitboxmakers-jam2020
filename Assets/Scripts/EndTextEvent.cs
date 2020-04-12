using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTextEvent : MonoBehaviour
{
    public void triggerEvent() {
        GameObject.Find("FinalBoss").GetComponent<Animator>().SetBool("bossFight", true);
    }
}
