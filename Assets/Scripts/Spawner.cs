using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    private Transform spawnerTransform;

    public Rigidbody fodder;
    public Rigidbody chaser;

    public float fodderRate = 0.1f;
    public float chaserRate = 1.0f;

    void Start()
    {
        spawnerTransform = GetComponent<Transform>();
        InvokeRepeating("spawnFodder", fodderRate, fodderRate);
        InvokeRepeating("spawnChaser", chaserRate, chaserRate);
    }

    void Update()
    {
    }

    void spawnFodder()
    {
        Vector2 randomVector2 = Random.insideUnitCircle.normalized;
        Rigidbody fodderClone = (Rigidbody) Instantiate(fodder, spawnerTransform.position + new Vector3(randomVector2.x * 50.0f + 1.0f, 0.0f, randomVector2.y * 50.0f + 1.0f), new Quaternion());
        fodderClone.velocity = new Vector3 (randomVector2.x, 0.0f, randomVector2.y) * 200;
    }

    void spawnChaser()
    {
        Vector2 randomVector2 = Random.insideUnitCircle.normalized;
        Rigidbody chaserClone = (Rigidbody)Instantiate(chaser, spawnerTransform.position + new Vector3(randomVector2.x * 50.0f + 1.0f, 0.0f, randomVector2.y * 50.0f + 1.0f), new Quaternion());
        chaserClone.velocity = new Vector3(randomVector2.x, 0.0f, randomVector2.y) * 30;
    }
}
