using UnityEngine;
using System.Collections;

public class ColliderController : MonoBehaviour
{
    public Rigidbody chaser;
    private GameController gameControllerClass;

    void Start()
    {
        gameControllerClass = (GameController)GameObject.FindGameObjectWithTag("GameController").GetComponent("GameController");
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        GameObject otherGameObject = other.gameObject;
        GameObject thisGameObject = gameObject;

        if (otherGameObject.CompareTag("Spawner") || thisGameObject.CompareTag("Spawner"))
        {
            return;
        } else if(otherGameObject.CompareTag("Player") && thisGameObject.CompareTag("Player"))
        {
            return;
        }
        else if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Fodder") || other.gameObject.CompareTag("Chaser"))
        {
            if (otherGameObject.GetComponent<Rigidbody>().mass < thisGameObject.GetComponent<Rigidbody>().mass)
            {
                if(thisGameObject != null && otherGameObject != null)
                {
                    gameControllerClass.addMassFromObject(thisGameObject, otherGameObject);
                    Destroy(otherGameObject);
                }
                
            }
        }
    }
}
