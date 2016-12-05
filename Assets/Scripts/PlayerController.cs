using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private GameController gameControllerClass;
    private GameObject player;
    private Rigidbody playerRigidBody;
    private Transform playerTransform;
    private GameObject[] playerBodies;
    private Vector3 centerOfMass;

    public Rigidbody playerSplit;

    private int speed;
    public int speedModifier = 1;

    public float massDissipationFactor = 0.99f;

    void Start () {
        player = gameObject;
        playerRigidBody = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();

        gameControllerClass = (GameController)GameObject.FindGameObjectWithTag("GameController").GetComponent("GameController");
        speed = gameControllerClass.getSpeed();

        InvokeRepeating("dissipateMass", 1.0f, 1.0f);

        centerOfMass = playerTransform.position;

    }
	
	void Update () {
        playerBodies = gameControllerClass.getPlayerBodies();
        centerOfMass = gameControllerClass.getCenterOfMass(playerBodies);

        speed = Mathf.RoundToInt((float)(gameControllerClass.getSpeed() * Mathf.Pow(gameControllerClass.getSpeedDissipationFactor(), playerRigidBody.mass)));

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 heading = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        playerRigidBody.AddForce(heading * speed * speedModifier * Time.smoothDeltaTime);

        if(Input.GetKeyUp(KeyCode.Z))
        {
            splitBodiesInHalf();
        }

        if (playerBodies.Length > 1)
        {
            playerRigidBody.AddForce((centerOfMass - playerTransform.position).normalized * gameControllerClass.getGroupForceFactor());
        }

    }

    void dissipateMass()
    {
        gameControllerClass.dissipateMass(player);
    }

    void splitBodiesInHalf()
    {
        if (playerRigidBody.mass >= 2)
        {
            gameControllerClass.addMass(player, -playerRigidBody.mass/2);
            Rigidbody playerClone = (Rigidbody)Instantiate(playerSplit, playerTransform.position, new Quaternion());
            playerClone.velocity = playerRigidBody.velocity;
        }
        
    }
}
