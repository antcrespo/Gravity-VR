using UnityEngine;
using System.Collections;

public class PortalIn : MonoBehaviour {

    public GameObject outPortal;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("PortalIn Enter");
        if (other.CompareTag("Player"))
            //other.gameObject.GetComponent<Rigidbody>().MovePosition(outPortal.transform.position);
            other.transform.position = outPortal.transform.position;
    }
}
