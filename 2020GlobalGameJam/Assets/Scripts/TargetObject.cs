using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    [SerializeField]
    string objectName;
    [SerializeField]
    int scoreValue;
    [SerializeField]
    GameObject particleBurst;

    SystemControl theSystemController;

    // Start is called before the first frame update
    void Start()
    {
        theSystemController = FindObjectOfType<SystemControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IsHit()
    {
        Debug.Log("I am hit");
        if (particleBurst != null)
        {
            GameObject newParticle = Instantiate(particleBurst, transform.position, Quaternion.identity);
            Destroy(newParticle, 10);
        }
        theSystemController.SticktoObj(objectName);
        theSystemController.changescore(scoreValue);
        gameObject.GetComponent<Collider>().enabled = false;
    }
}
