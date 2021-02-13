using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_head : MonoBehaviour
{
    private Player parentCode;
    void Start()
    {
        try
        {
            parentCode = (Player)gameObject.transform.parent.GetComponent(typeof(Player));
        }
        catch { }
        if (parentCode == null) Debug.Log("Код не найден Player_head");
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Floor")
        {
            parentCode.setRoof(true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Floor")
        {
            parentCode.setRoof(false);
        }
    }
}
