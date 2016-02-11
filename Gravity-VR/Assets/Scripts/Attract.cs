using UnityEngine;
using System.Collections;

public class Attract : MonoBehaviour {

	public float attraction = 9.8f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody[] childRbs = gameObject.GetComponentsInChildren<Rigidbody> ();
		for (int i = 0; i < childRbs.Length; i++) {
			Vector3 direction = (transform.position - childRbs [i].position).normalized;
			childRbs [i].AddForce (direction * attraction);
		}
	}
}
