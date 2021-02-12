using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    const int PARALLAX = 3;
    const int PARALLAX_SLOW = 6;
    GameObject player;
    Transform back1;
    Transform back2;
    public float SPEED = 2;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        back1 = transform.GetChild(0);
        back2 = transform.GetChild(1);
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        back1.position = new Vector3(player.transform.position.x / PARALLAX, player.transform.position.y / PARALLAX - 2 , 0);
        back2.position = new Vector3(player.transform.position.x / (PARALLAX_SLOW), player.transform.position.y / (PARALLAX_SLOW), 0);

    }
}
