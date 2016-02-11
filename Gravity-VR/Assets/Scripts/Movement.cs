using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	
	public float MoveForce = 5.0f;
	private Rigidbody rb;
	Transform watcher;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		watcher = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetAxis (KeyCode.UpArrow)) {
			rb.velocity = MoveForce * watcher.TransformDirection(Vector3.up);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			rb.velocity =  (MoveForce * watcher.TransformDirection(Vector3.down));
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			rb.velocity =  (MoveForce * watcher.TransformDirection(Vector3.right));
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			rb.velocity = (MoveForce * watcher.TransformDirection(Vector3.left));
		}*/

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal != 0)
            rb.velocity =  Mathf.Sign(horizontal) * MoveForce * watcher.TransformDirection(Vector3.right);
        if (vertical != 0)
            rb.velocity = Mathf.Sign(vertical) * MoveForce * watcher.TransformDirection(Vector3.up);
    }
}
