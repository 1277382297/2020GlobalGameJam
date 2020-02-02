using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickCollision : MonoBehaviour
{
    [SerializeField]
    float respawnDelay = 0;
    public float spawnX = 0;
    public float spawnY = 0;
    public float spawnZ = 0;
    public bool readyToStick = true;
    public bool stuck = false;
    Rigidbody rb;
    [SerializeField]
    GameObject tapeModelPrefab;
    LivesManager livesManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        livesManager = GetComponent<LivesManager>();
        SetSpawnPoint(); //Can disable this if we want the start to be somewhere other than where the player is originally positioned in the scene.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision otherCollision)
    {
        //This one sticks no matter what it hits, unless it's the starting platform.
        //Then check to see if the other thing was interactible, and if so, respond to that.
        //Then have the player go back to start and fly again.
        if (readyToStick == true && otherCollision.gameObject.tag != "StartingPlatform")
        {
            Stick();
            if (otherCollision.gameObject.GetComponent<TargetObject>() != null)
            {
                otherCollision.gameObject.GetComponent<TargetObject>().IsHit();
            }
            StartCoroutine(WaitThenRespawn());
        }

    }
    
    public void SetSpawnPoint()
    {
        spawnX = transform.position.x;
        spawnY = transform.position.y;
        spawnZ = transform.position.z;
        Debug.Log("Spawn point set to   X: " + spawnX + " Y: " + spawnY + " Z: " + spawnZ);
    }

    private void Stick()
    {
        readyToStick = false;
        stuck = true;
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        //Do any turning that needs to happen.
        //Spawn any particle effects
    }

    IEnumerator WaitThenRespawn() //For dramatic pause and to let any particle effects play out.
    {
        yield return new WaitForSeconds(respawnDelay);
        Respawn();
    }

    private void Respawn()
    {
        if (tapeModelPrefab != null)
        {
            Instantiate(tapeModelPrefab, gameObject.transform.position, gameObject.transform.rotation);
        }
        transform.position = new Vector3(spawnX, spawnY, spawnZ);
        readyToStick = true;
        stuck = false;
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;
        if (GetComponent<AirplaneFlight>() != null)
        {
            GetComponent<AirplaneFlight>().ResetPitchAngle();
        }
        livesManager.ChangeLives(-1);
    }
}
