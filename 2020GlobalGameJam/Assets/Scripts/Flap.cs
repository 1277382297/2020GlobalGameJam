using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flap : MonoBehaviour
{
    public float downwardGravityForce;
    public float flapForce;
    public KeyCode flapKey;
   
    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(0, (-1 * downwardGravityForce), 0, ForceMode.Force);
        if (Input.GetKeyDown(flapKey))
        {
            rb.AddRelativeForce(0, flapForce, 0, ForceMode.Impulse);
        }
    }
}
