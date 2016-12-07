using UnityEngine;
using System.Collections;

public class FodderController : MonoBehaviour {
    private GameController gameControllerClass;
    private GameObject fodder;
    private Rigidbody fodderRigidBody;
    private Transform fodderTransform;

    private float attractionDistance;

    void Start () {
        fodder = gameObject;
        fodderRigidBody = gameObject.GetComponent<Rigidbody>();

        gameControllerClass = (GameController)GameObject.FindGameObjectWithTag("GameController").GetComponent("GameController");
        attractionDistance = gameControllerClass.getAttractionDistance();
    }
	
	void Update ()
    {
        GameObject nearbyObject;
        nearbyObject = gameControllerClass.findClosestObjectByTags(fodder, new string[] { "Player", "Chaser" }, true);
        if (nearbyObject != null)
        {
            if(Vector3.Distance(nearbyObject.GetComponent<Transform>().position, fodder.GetComponent<Transform>().position) < attractionDistance) {
                fodderRigidBody.AddForce(gameControllerClass.targetMovingObject(nearbyObject, fodder) * 5.0f);

            }
        }
	
	}
}
