using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public GameObject player;

    void OnTriggerEnter(Collider other) {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player>().Death();
    }
}