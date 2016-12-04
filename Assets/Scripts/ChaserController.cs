using UnityEngine;
using System.Collections;

public class ChaserController : MonoBehaviour
{

    private GameController gameControllerClass;
    private Rigidbody chaserRigidBody;
    private Transform chaserTransform;
    private Rigidbody playerRigidBody;
    private Transform playerTransform;
    private GameObject player;
    private GameObject chaser;
    private Vector3 randomHeading;

    private float speed;

    public float speedModifier = 1.0f;
    public float seekRange = 50.0f;
    

    void Start()
    {
        chaser = gameObject;
        chaserRigidBody = chaser.GetComponent<Rigidbody>();
        chaserTransform = chaser.GetComponent<Transform>();

        player = GameObject.FindGameObjectWithTag("Player");
        gameControllerClass = (GameController)GameObject.FindGameObjectWithTag("GameController").GetComponent("GameController");

        InvokeRepeating("getRandomHeading", 0.0f, 10.0f);
        InvokeRepeating("dissipateMass", 1.0f, 1.0f);

        if (player != null)
        {
            playerRigidBody = player.GetComponent<Rigidbody>();
            playerTransform = player.GetComponent<Transform>();
        }

        
    }

    void Update()
    {
        GameObject nearbyObject = null;
        Vector3 heading = randomHeading;
        speed = gameControllerClass.getSpeed() * Mathf.Pow(gameControllerClass.getSpeedDissipationFactor(), chaserRigidBody.mass);
        

        nearbyObject = gameControllerClass.findClosestObjectByTagsIgnoreEquivalentMass(chaser, new string[] {"Player", "Fodder", "Chaser"});

        if (nearbyObject != null && nearbyObject.GetComponent<Rigidbody>().mass < chaserRigidBody.mass)
        {
            heading = gameControllerClass.targetMovingObject(nearbyObject, chaser);
            chaserRigidBody.AddForce(heading * speed * speedModifier * Time.smoothDeltaTime);
        }
        else if (nearbyObject != null && nearbyObject.GetComponent<Rigidbody>().mass > chaserRigidBody.mass)
        {
            heading = gameControllerClass.targetMovingObject(nearbyObject, chaser);
            chaserRigidBody.AddForce(-heading * speed * speedModifier * Time.smoothDeltaTime);
        }
        else
        {
            nearbyObject = gameControllerClass.findClosestObjectByTag(chaser, "Fodder");
            if (nearbyObject != null)
            {
                heading = gameControllerClass.targetMovingObject(nearbyObject, chaser);
            }
            else
            {
                heading = randomHeading;
            }

            chaserRigidBody.AddForce(heading * speed * speedModifier * Time.smoothDeltaTime);
        }
    }

    Vector3 getRandomHeading ()
    {   randomHeading = Random.insideUnitSphere.normalized;
        return randomHeading;
    }

    void dissipateMass()
    {
        gameControllerClass.dissipateMass(chaser);
    }
}

