using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private GameController gameControllerClass;
    private GameObject player;
    private Rigidbody playerRigidBody;
    private Transform playerTransform;

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

    }
	
	void Update () {
        speed = Mathf.RoundToInt((float)(gameControllerClass.getSpeed() * Mathf.Pow(gameControllerClass.getSpeedDissipationFactor(), playerRigidBody.mass)));
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 heading = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;
        playerRigidBody.AddForce(heading * speed * speedModifier * Time.smoothDeltaTime);

    }

    void dissipateMass()
    {
        gameControllerClass.dissipateMass(player);
    }
}
