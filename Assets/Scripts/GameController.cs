using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject spawner;
    public Rigidbody chaser;
    public Rigidbody player;

    private GameObject[] playerBodies;

    public int speed = 1000;
    public int gridSize = 1;

    public float velocityCap = 500.0f;
    public float massDissipationFactor = 0.9999f;
    public float speedDissipationFactor = 0.99f;
    public float maximumSizeClamp = 100000.0f;
    public float attractionDistance = 10.0f;
    public float groupForceFactor = 50.0f;

    private void Awake()
    {

    }
    void Start () {
        playerBodies = GameObject.FindGameObjectsWithTag("Player");
        GameObject spawnerClone1 = (GameObject) Instantiate(spawner, new Vector3(-500.0f, -5, -500.0f), new Quaternion());
        GameObject spawnerClone2 = (GameObject)Instantiate(spawner, new Vector3(-500.0f, -5, 0.0f), new Quaternion());
        GameObject spawnerClone3 = (GameObject)Instantiate(spawner, new Vector3(-500.0f, -5, 500.0f), new Quaternion());
        GameObject spawnerClone4 = (GameObject)Instantiate(spawner, new Vector3(0.0f, -5, -500.0f), new Quaternion());
        GameObject spawnerClone5 = (GameObject)Instantiate(spawner, new Vector3(0.0f, -5, 0.0f), new Quaternion());
        GameObject spawnerClone6 = (GameObject)Instantiate(spawner, new Vector3(0.0f, -5, 500.0f), new Quaternion());
        GameObject spawnerClone7 = (GameObject)Instantiate(spawner, new Vector3(500.0f, -5, -500.0f), new Quaternion());
        GameObject spawnerClone8 = (GameObject)Instantiate(spawner, new Vector3(500.0f, -5, 0.0f), new Quaternion());
        GameObject spawnerClone9 = (GameObject)Instantiate(spawner, new Vector3(500.0f, -5, 500.0f), new Quaternion());
    }
	
	void Update () {
        playerBodies = GameObject.FindGameObjectsWithTag("Player");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (playerBodies.Length > 0)
            {
                destroyAllSelectedBodies(playerBodies);
            }
            Vector2 randomVector2 = Random.insideUnitCircle.normalized;
            Rigidbody playerClone = (Rigidbody)Instantiate(player, new Vector3(randomVector2.x * 1000.0f - 500.0f, 0.0f, randomVector2.y * 1000.0f - 500.0f), new Quaternion());
            playerClone.GetComponent<Rigidbody>().mass = 1;
            
        }


    }

    public GameObject findClosestObjectByTag(GameObject selectedObject, string targetTag)
    {
        GameObject[] targets;
        GameObject closestObject = null;

        targets = GameObject.FindGameObjectsWithTag(targetTag);

        if (targets == null || targets.Length == 0)
        {
            return closestObject;
        }

        closestObject = targets[0];
        for (int i = 1; i < targets.Length; i++)
        {
            closestObject = returnClosestToSelected(selectedObject, closestObject, targets[i]);
        }
        return closestObject;
    }

    public GameObject findClosestObjectByTagIgnoreEquivalentMass(GameObject selectedObject, string targetTag)
    {
        GameObject[] targets;
        GameObject closestObject = null;

        targets = GameObject.FindGameObjectsWithTag(targetTag);

        if (targets == null || targets.Length == 0)
        {
            return closestObject;
        }

        closestObject = targets[0];
        for (int i = 1; i < targets.Length; i++)
        {
            if (targets[i].GetComponent<Rigidbody>().mass != selectedObject.GetComponent<Rigidbody>().mass)
            {
                closestObject = returnClosestToSelected(selectedObject, closestObject, targets[i]);
            }
        }
        return closestObject;
    }

    public GameObject findClosestObjectByTags(GameObject selectedObject, string[] targetTags)
    {
        GameObject closestObject = null;

        if (targetTags == null || targetTags.Length == 0)
        {
            return closestObject;
        }

        closestObject = findClosestObjectByTag(selectedObject, targetTags[0]);
        for (int i = 1; i < targetTags.Length; i++)
        {
            closestObject = returnClosestToSelected(selectedObject, closestObject, findClosestObjectByTag(selectedObject, targetTags[i]));
        }
        return closestObject;
    }

    public GameObject findClosestObjectByTagsIgnoreEquivalentMass(GameObject selectedObject, string[] targetTags)
    {
        GameObject closestObject = null;

        if (targetTags == null || targetTags.Length == 0)
        {
            return closestObject;
        }

        closestObject = findClosestObjectByTagIgnoreEquivalentMass(selectedObject, targetTags[0]);
        for (int i = 1; i < targetTags.Length; i++)
        {
            closestObject = returnClosestToSelected(selectedObject, closestObject, findClosestObjectByTagIgnoreEquivalentMass(selectedObject, targetTags[i]));
        }
        return closestObject;
    }

    public Vector3 targetMovingObject(GameObject target, GameObject selectedObject)
    {
        Rigidbody targetRigidBody = target.GetComponent<Rigidbody>();
        Rigidbody targeterRigidBody = selectedObject.GetComponent<Rigidbody>();

        Vector3 straightLine = target.transform.position - selectedObject.transform.position;
        Vector3 chaserVelocityComponent = -targeterRigidBody.velocity * Vector3.Angle(targeterRigidBody.velocity, straightLine) / 180;
        Vector3 playerVelocityComponent = targetRigidBody.velocity * (1 - Vector3.Angle(targetRigidBody.velocity, straightLine) / 180);

        return (straightLine + chaserVelocityComponent + playerVelocityComponent).normalized;
    }

    public GameObject returnClosestToSelected(GameObject selectedObject, GameObject object1, GameObject object2)
    {
        if ((selectedObject == null) || (object1 == null && object2 == null))
        {
            return null;
        }
        else if (object1 == null && object2 != null)
        {
            return object2;
        }
        else if (object2 == null && object1 != null)
        {
            return object1;
        }
        else
        {
            return Vector3.Distance(selectedObject.transform.position, object1.transform.position) < Vector3.Distance(selectedObject.transform.position, object2.transform.position) ? object1 : object2;
        }
    }

    public void dissipateMass(GameObject selectedObject)
    {
        addMass(selectedObject, -selectedObject.GetComponent<Rigidbody>().mass * (1 - massDissipationFactor));
    }

    public int getSpeed()
    {
        return speed;
    }

    public float getVelocityCap()
    {
        return velocityCap;
    }

    public float getSpeedDissipationFactor()
    {
        return speedDissipationFactor;
    }

    public float getMassDissipationFactor()
    {
        return massDissipationFactor;
    }

    public float getMaximumSizeClamp()
    {
        return maximumSizeClamp;
    }

    public float getAttractionDistance()
    {
        return attractionDistance;
    }

    public GameObject[] getPlayerBodies()
    {
        return playerBodies;
    }

    public float getGroupForceFactor()
    {
        return groupForceFactor;
    }

    public void addMassFromObject(GameObject massTo, GameObject massOf)
    {
        float newMass = Mathf.Clamp(massTo.GetComponent<Rigidbody>().mass + massOf.GetComponent<Rigidbody>().mass, 1.0f, maximumSizeClamp);
        massTo.GetComponent<Rigidbody>().mass = newMass;
        updateScale(massTo);
    }

    public void addMass(GameObject massTo, float mass)
    {
        float newMass = Mathf.Clamp(massTo.GetComponent<Rigidbody>().mass + mass, 1.0f, maximumSizeClamp);
        massTo.GetComponent<Rigidbody>().mass = newMass;
        updateScale(massTo);
    }

    public void updateScale(GameObject selectedObject)
    {
        selectedObject.GetComponent<Transform>().localScale = new Vector3(selectedObject.GetComponent<Rigidbody>().mass, 0.0f, selectedObject.GetComponent<Rigidbody>().mass);
    }

    public Vector3 getCenterOfMass(GameObject[] selectedObjects)
    {
        float totalMass = 0.0f;
        float xTotalMass = 0.0f;
        float yTotalMass = 0.0f;
        float zTotalMass = 0.0f;

        foreach (GameObject selectedObject in selectedObjects)
        {
            totalMass += selectedObject.GetComponent<Rigidbody>().mass;
            xTotalMass += selectedObject.GetComponent<Transform>().position.x * selectedObject.GetComponent<Rigidbody>().mass;
            yTotalMass += selectedObject.GetComponent<Transform>().position.y * selectedObject.GetComponent<Rigidbody>().mass;
            zTotalMass += selectedObject.GetComponent<Transform>().position.z * selectedObject.GetComponent<Rigidbody>().mass;
        }

        return new Vector3(xTotalMass / totalMass, yTotalMass / totalMass, zTotalMass / totalMass);
    }

    public float getTotalMass (GameObject[] selectedObjects)
    {
        float totalMass = 0.0f;
        foreach (GameObject selectedObject in selectedObjects)
        {
            totalMass += selectedObject.GetComponent<Rigidbody>().mass;
        }
        return totalMass;
    }

    public void destroyAllSelectedBodies(GameObject[] selectedObjects)
    {
        foreach (GameObject selectedObject in selectedObjects)
        {
            DestroyObject(selectedObject);
        }
    }

}
