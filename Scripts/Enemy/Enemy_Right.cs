using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Right : MonoBehaviour {
    private Enemy parentCode;
    void Start() {
        try {
            parentCode = (Enemy)gameObject.transform.parent.GetComponent(typeof(Enemy));
        }
        catch { }
        if (parentCode == null)
            Debug.Log("Код не найден Right");

    }
    const float STACKSPEED = 0.5f;
    const float DIFERENCE_FOR_CRASH = 0.5f;
    void OnTriggerStay2D(Collider2D col) {

        if (col.tag == "Enemy") {
            float mySpeed = 0;
            float hisSpeed = 0;
            try {
                mySpeed = parentCode.GetComponent<Rigidbody2D>().velocity.x;
                hisSpeed = col.transform.GetComponent<Rigidbody2D>().velocity.x;

                if ((parentCode.gameObject.transform.localScale.x < 0) && (((mySpeed > -STACKSPEED) && (mySpeed < STACKSPEED)) || ((Math.Sign(mySpeed) != Math.Sign(hisSpeed)) && (parentCode.gameObject.transform.localScale.x < 0)))) {
                    parentCode.setTriggerRight(true);
                }
            }
            catch { }
        }
        else if ((col.tag != "Player") && (col.tag != "Untagged"))
            parentCode.setTriggerRight(true);


    }
    void OnTriggerExit2D(Collider2D col) {

        try {
            parentCode.setTriggerRight(false);
        }
        catch { }
    }
}
