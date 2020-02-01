using System.Collections;
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
        if (stickCollision.stuck == false)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0, Space.World);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(0, -1 * turnSpeed * Time.deltaTime, 0, Space.World);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if ((currentPitchAngle + (turnSpeed * Time.deltaTime)) <= upwardPitchMax)
                {
                    transform.Rotate(turnSpeed * Time.deltaTime, 0, 0, Space.Self);
                    currentPitchAngle += (turnSpeed * Time.deltaTime);
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if ((currentPitchAngle - (turnSpeed * Time.deltaTime)) >= downwardPitchMax)
                {
                    transform.Rotate(-1 * turnSpeed * Time.deltaTime, 0, 0, Space.Self);
                    currentPitchAngle -= (turnSpeed * Time.deltaTime);
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
