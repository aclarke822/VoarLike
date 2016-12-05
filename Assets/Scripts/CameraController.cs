using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private GameController gameControllerClass;
    private Transform cameraTransform;
    private Vector3 centerOfPlayerMass;
    private GameObject[] playerBodies;
    public float orthographicSizeFactor = 0.9f;

    void Start()
    {
        gameControllerClass = (GameController)GameObject.FindGameObjectWithTag("GameController").GetComponent("GameController");
        cameraTransform = GetComponent<Transform>();


    }

    void Update()
    {
        playerBodies = gameControllerClass.getPlayerBodies();
        if (playerBodies != null && playerBodies.Length > 0)
        {
            centerOfPlayerMass = gameControllerClass.getCenterOfMass(playerBodies);
            cameraTransform.position = new Vector3(centerOfPlayerMass.x, 100.0f, centerOfPlayerMass.z);
            gameObject.GetComponent<Camera>().orthographicSize = gameControllerClass.getTotalMass(playerBodies) * orthographicSizeFactor * 10;


        }

    }
}
