using UnityEngine;
using System.Collections;

public class CodeSnippets : MonoBehaviour {

	void Start () {
        
    }
	

	void Update () {
        /* from Chaser code
        if (playerIsEdibleInRange())
        {
            Vector3 heading = targetMovingObject(player, chaser);
            chaserRigidBody.AddForce(heading * speed * speedModifier * Time.smoothDeltaTime);
        }
        else if (playerIsDangerousInRange())
        {
            Vector3 heading = targetMovingObject(player, chaser);
            chaserRigidBody.AddForce(-heading * speed * speedModifier * Time.smoothDeltaTime);
        }
        else if (fodderIsEdibleInRange())
        {
            GameObject fodder = findClosestTarget("Fodder");
            Vector3 heading = targetMovingObject(fodder, chaser);
            chaserRigidBody.AddForce(heading * speed * speedModifier * Time.smoothDeltaTime);
        }
        else
        {
            Vector3 heading = randomHeading;
            chaserRigidBody.AddForce(heading * speed / 4 * speedModifier * Time.smoothDeltaTime);
        }
        */

    }

    /*from chaser
        bool playerIsEdibleInRange()
    {
            if (player != null & chaser != null)
            {
                return (chaserRigidBody.mass > playerRigidBody.mass && Vector3.Distance(playerTransform.position, chaserTransform.position) < seekRange);
            }
            return false;
        }

        bool playerIsDangerousInRange()
    {
            if (player != null & chaser != null)
            {
                return (chaserRigidBody.mass < playerRigidBody.mass && Vector3.Distance(playerTransform.position, chaserTransform.position) < seekRange);
            }
            return false;
        }

        bool fodderIsEdibleInRange()
    {
            GameObject fodder = findClosestTarget("Fodder");
            if (fodder != null & chaser != null)
            {
                return ((Vector3.Distance(fodder.transform.position, chaserTransform.position) < seekRange * 1000) && (chaserRigidBody.mass > fodder.GetComponent<Rigidbody>().mass));
            }
            return false;
        }
        */
}
