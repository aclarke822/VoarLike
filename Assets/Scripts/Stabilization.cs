using UnityEngine;
using System.Collections;

public class Stabilization : MonoBehaviour {

    private GameController gameControllerClass;
    private Rigidbody objectRigidBody;
    private Transform objectTransform;

    private float velocityCap = 500.0f;

	void Start () {
        objectRigidBody = GetComponent<Rigidbody>();
        objectTransform = GetComponent<Transform>();

        gameControllerClass = (GameController)GameObject.FindGameObjectWithTag("GameController").GetComponent("GameController");
        velocityCap = gameControllerClass.getVelocityCap();
    }
	
	void Update () {

        objectRigidBody.velocity.Set(Mathf.Clamp(objectRigidBody.velocity.x, 0.0f, velocityCap), 0, Mathf.Clamp(objectRigidBody.velocity.z, 0.0f, velocityCap));
        //objectTransform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        //objectTransform.position = new Vector3(objectRigidBody.position.x, 0.0f, objectRigidBody.position.z);

    }
}
