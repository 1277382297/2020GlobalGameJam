using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    [SerializeField]
    string objectName;

    SystemControl theSystemController;
    // Start is called before the first frame update
    void Start()
    {
        theSystemController = FindObjectOfType<SystemControl>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(theSystemController.totaltapelength); //Just to make sure we actually have the object.
    }

    public void IsHit()
    {
        Debug.Log("I am hit");
        theSystemController.SticktoObj(objectName);
        gameObject.GetComponent<Collider>().enabled = false;
        //DoSomething
    }
}
