using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_move : MonoBehaviour
{
    GameObject player;
    private Player qwe;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


        //qwe = (Player)GameObject.Find("SLime").GetComponent(typeof(Player)); // Доступ к классу Player в SLime 
        
    }


    void Update()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        //Debug.Log(qwe.getCharacter().doSplash());
        //qwe.getCharacter().doSplash(); ?/ Вызов
    }
}
