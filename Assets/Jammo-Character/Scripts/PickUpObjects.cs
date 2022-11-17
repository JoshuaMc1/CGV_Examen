using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObjects : MonoBehaviour
{
        public GameObject ObjectToPickUp;
        public GameObject PickedObject;
        public Transform interactionZone;


        // Update is called once per frame
        void Update()
        {
            if (ObjectToPickUp != null && ObjectToPickUp.GetComponent<PickableObjetcs>().isPickable == true && PickedObject == null)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    PickedObject = ObjectToPickUp;
                    PickedObject.GetComponent<PickableObjetcs>().isPickable = false;
                    PickedObject.transform.SetParent(interactionZone);
                    PickedObject.transform.position = interactionZone.position;
                    PickedObject.GetComponent<Rigidbody>().useGravity = false;
                    PickedObject.GetComponent<Rigidbody>().isKinematic = true;
                }

            }
            else if (PickedObject != null)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    PickedObject.GetComponent<PickableObjetcs>().isPickable = true;
                    PickedObject.transform.SetParent(null);
                    PickedObject.GetComponent<Rigidbody>().useGravity = true;
                    PickedObject.GetComponent<Rigidbody>().isKinematic = false;
                    PickedObject = null;
                }
            }
        }

    }

