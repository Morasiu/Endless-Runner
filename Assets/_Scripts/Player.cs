using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public KeyCode kcJump;
    public KeyCode kcLeft;
    public KeyCode kcRight;

    CharacterController controller;
    Vector3 v3Move;
    Vector3 v3Swipe;
    Vector3 v3Position;
    float fSpeed = 0;
    float fSpeedStart = 10f;
    float fMaxSpeed = 50f;
    float fVerticalVelocity = 0.0f;
    float fGravity = 12f;

    bool isDead = false;
    public bool jump = false;

	void Start () {
        controller = GetComponent<CharacterController>();
	}

	void Update () {
        if (isDead)
            return;

        if(fSpeed < fMaxSpeed)
            fSpeed = fSpeedStart + GetPlayerPosition() / 100;
        
        if (transform.position.y < 0)
            Death();
        v3Move = Vector3.zero;
        if (Time.timeSinceLevelLoad < CameraMotor.fAnimDuration) {
            controller.Move(Vector3.forward * fSpeed * Time.deltaTime);
            return;
        }

        if (controller.isGrounded) {
            fVerticalVelocity = -0.5f;
        } else {
            fVerticalVelocity -= fGravity * Time.deltaTime;
            fSpeed = 10f;
        }

        if (jump) {
            Jump();
            jump = false;
        }

        //X Left and right
        if (Input.GetKeyDown(kcLeft))
            Left();

        if (Input.GetKeyDown(kcRight))
            Right();

        //Y Up and doww
        if (Input.GetKeyDown(kcJump))
            Jump();
           
        v3Move.y = fVerticalVelocity;

        //Z Forward and Backward
        v3Move.z = fSpeed;

        controller.Move(v3Move * Time.deltaTime);
	}

    public int GetPlayerPosition() {
        return (int)GameObject.FindGameObjectWithTag("Player").transform.position.z;
    }

    public bool GetPlayerCondition() {
        return isDead;
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
            
    }

    public void Left() {
        if (transform.position.x >= 0 && controller.isGrounded) {
            v3Position = transform.position;
            v3Swipe = transform.position;
            v3Swipe.x = transform.position.x - 2.5f;
            transform.position = Vector3.Lerp(v3Position, v3Swipe, 1f);
        }
    }

    public void Right() {
        if (transform.position.x <= 0 && controller.isGrounded) {
            v3Position = transform.position;
            v3Swipe = transform.position;
            v3Swipe.x = transform.position.x + 2.5f;
            transform.position = Vector3.Lerp(v3Position, v3Swipe, 1f);
        }
    }

   public void Jump() {
        if (controller.isGrounded) {
            fVerticalVelocity += 5f;
        }
    }

   public void Death() {
        isDead = true;
    }

    public void Reset() {
        isDead = false;
    }
}