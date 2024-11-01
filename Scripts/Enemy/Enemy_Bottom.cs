﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bottom : MonoBehaviour {
    private Enemy parentCode;
    void Start() {
        try {
            parentCode = (Enemy)gameObject.transform.parent.GetComponent(typeof(Enemy));
        }
        catch { }

    }
    void OnTriggerStay2D(Collider2D col) {
        try {

            if ((col.tag == "Floor") || (col.tag == "Enemy")) {
                if (parentCode.getIsGround() == false)
                    parentCode.doSplash();
                parentCode.setIsGround(true);
            }
        }
        catch { }
    }
    void OnTriggerExit2D(Collider2D col) {
        try {
            if ((col.tag == "Floor") || (col.tag == "Enemy"))
                parentCode.setIsGround(false);
        }
        catch { }
    }
}
