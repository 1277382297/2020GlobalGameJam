﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneFlight : MonoBehaviour
{
    public float baseMoveSpeed;
    public float moveSpeedModifier;
    public float moveSpeed;

    [SerializeField]
    float turnSpeed;
    [SerializeField]
    float upwardPitchMax = 80;
    [SerializeField]
    float downwardPitchMax = -80;

    [SerializeField]
    float initialPitchAngle;

    public int inverseControlsModifierVertical = 1;

    Vector3 newPosition;
    Rigidbody rb;
    StickCollision stickCollision;

    float currentPitchAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stickCollision = GetComponent<StickCollision>();
        moveSpeed = baseMoveSpeed;
        currentPitchAngle = initialPitchAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inverseControlsModifierVertical = inverseControlsModifierVertical * -1;
        }

        if (stickCollision.stuck == false)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0, Space.World);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, -1 * turnSpeed * Time.deltaTime, 0, Space.World);
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                float projectedAngle = (currentPitchAngle + (turnSpeed * Time.deltaTime * inverseControlsModifierVertical));
                if (projectedAngle <= upwardPitchMax && projectedAngle >= downwardPitchMax)
                {
                    transform.Rotate(turnSpeed * Time.deltaTime * inverseControlsModifierVertical, 0, 0, Space.Self);
                    currentPitchAngle += (turnSpeed * Time.deltaTime * inverseControlsModifierVertical);
                }
            }
            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                float projectedAngle = (currentPitchAngle - (turnSpeed * Time.deltaTime * inverseControlsModifierVertical));
                if (projectedAngle >= downwardPitchMax && projectedAngle <= upwardPitchMax)
                {
                    transform.Rotate(-1 * turnSpeed * Time.deltaTime * inverseControlsModifierVertical, 0, 0, Space.Self);
                    currentPitchAngle -= (turnSpeed * Time.deltaTime * inverseControlsModifierVertical);
                }
            }
            //Maybe at some point we need to figure out how to replace this with a velocity or force, but we can cross that bridge when we come to it.
            newPosition = (Vector3)gameObject.transform.position + transform.forward * moveSpeed * Time.deltaTime;
            rb.MovePosition(newPosition);
        }
    }

    public void ChangeSpeed(int positiveOrNegative)
    {
        moveSpeed = moveSpeed + (moveSpeedModifier * positiveOrNegative);
        if (moveSpeed <= 0)
        {
            moveSpeed = 0.1f;
        }
    }

    public void ResetPitchAngle()
    {
        currentPitchAngle = 0;
    }
}
