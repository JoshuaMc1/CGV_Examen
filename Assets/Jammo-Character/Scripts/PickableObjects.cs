using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjetcs : MonoBehaviour
{
    public bool isPickable = true;

    //Cuando el Rigidbody entre al trigger este hara algo, detectara esa colisión
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerInteractionZone")
        {
            other.GetComponentInParent<PickObjects>().ObjectToPickUp = this.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerInteractionZone")
        {
            other.GetComponentInParent<PickObjects>().ObjectToPickUp = null;
        }
    }

}
