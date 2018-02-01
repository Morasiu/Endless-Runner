using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public GameObject player;
    public int score;

    bool isDead = false;
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        isDead = player.GetComponent<Player> ().GetPlayerCondition();
        if (isDead)
            return;

        GetComponent<Text> ().text = "" + player.GetComponent<Player>().GetPlayerPosition()/2 + " m";
    }
}