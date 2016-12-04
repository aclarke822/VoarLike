using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransform;
    private Rigidbody playerRigidBody;
    private Transform cameraTransform;
    public float orthographicSizeFactor = 0.9f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        playerRigidBody = player.GetComponent<Rigidbody>();
        cameraTransform = GetComponent<Transform>();

    }

    void Update()
    {
        if (playerTransform != null)
        {
            cameraTransform.position = new Vector3(playerTransform.position.x, 100.0f, playerTransform.position.z);
            gameObject.GetComponent<Camera>().orthographicSize = playerRigidBody.mass * orthographicSizeFactor * 10;


        }

    }
}
