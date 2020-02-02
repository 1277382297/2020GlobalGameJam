using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flap : MonoBehaviour
{
    public float downwardGravityForce;
    public float flapForce;
    public KeyCode flapKey;
   
    Rigidbody rb;
    StickCollision stickCollision;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stickCollision = GetComponent<StickCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stickCollision.stuck == false)
        {
            rb.AddForce(0, (-1 * downwardGravityForce), 0, ForceMode.Force);
            if (Input.GetKeyDown(flapKey))
            {
                rb.AddRelativeForce(0, flapForce, 0, ForceMode.Impulse);
            }
        } else if (stickCollision.readyToUnstick == true)
        {
            if (Input.GetKeyDown(flapKey))
            {
                stickCollision.stuck = false;
                stickCollision.readyToUnstick = false;
                rb.AddRelativeForce(0, flapForce, 0, ForceMode.Impulse);
            }
        }
        
    }
}
