using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    void Update()
    {
        Ray ray = new Ray(GetComponent<Camera>().transform.position, GetComponent<Camera>().transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                //Destroy(hit.collider.gameObject);
            }
        }
    }
}
