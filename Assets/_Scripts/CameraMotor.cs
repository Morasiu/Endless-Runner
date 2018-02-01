using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {

    private Transform trnLookAt;
    private Vector3 v3Offset;
    private Vector3 v3Move;

    private float fTransition = 0f;
    public static float fAnimDuration = 1f;
    private Vector3 v3AnimationOffset = new Vector3(0, 5, 5);

	void Start () {
        trnLookAt = GameObject.FindGameObjectWithTag("Player").transform;
        v3Offset = transform.position - trnLookAt.position;
	}

	void Update () {
        v3Move = trnLookAt.position + v3Offset;
        v3Move.x = 0;

        if(fTransition > 1f) {
            transform.position = v3Move;
        } else {
            transform.position = Vector3.Lerp(v3Move + v3AnimationOffset, v3Move, fTransition);
            fTransition += Time.deltaTime * 1 / fAnimDuration;
            transform.LookAt(trnLookAt.position + Vector3.up);
        }
	}
}