using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMoveRabbit1 : MonoBehaviour
{
    public float velocity;
    public Transform objetive;
    public float rotationSpeed;

    void Start()
    {
        objetive = GameObject.Find("PP1R1").transform;
    }

    void Update()
    {
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * velocity);

        var rotation = Quaternion.LookRotation(objetive.transform.position - gameObject.transform.position);
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
}
