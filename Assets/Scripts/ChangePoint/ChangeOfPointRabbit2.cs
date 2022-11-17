using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOfPointRabbit2 : MonoBehaviour
{
    public Transform nextPoint;

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<PatrolMoveRabbit2>().objetive = nextPoint;
    }
}
