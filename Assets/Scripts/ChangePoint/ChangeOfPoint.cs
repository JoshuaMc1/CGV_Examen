using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOfPoint : MonoBehaviour
{
    public Transform nextPoint;

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<PatrolMove>().objetive = nextPoint;
    }
}
