using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOfPointRabbit1 : MonoBehaviour
{
    public Transform nextPoint;

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<PatrolMoveRabbit1>().objetive = nextPoint;
    }
}
