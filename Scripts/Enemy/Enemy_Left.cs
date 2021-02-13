using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Left : MonoBehaviour
{
    private Enemy parentCode;
    void Start()
    {
        parentCode = (Enemy)gameObject.transform.parent.GetComponent(typeof(Enemy));
        //if (parentCode == null) Debug.Log("Код не найден Left");
    }
    void OnTriggerStay2D(Collider2D col)
    {

    }
    void OnTriggerExit2D(Collider2D col)
    {
        parentCode.setTriggerLeft(false);
    }
}
