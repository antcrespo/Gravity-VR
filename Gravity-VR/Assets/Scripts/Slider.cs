using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour {
    //public Vector3 movement;
    public int movementDimension; // 0 = x, 1 = y
    public float radius = 20f;
    public float len;
    public float angularSpeed;

    private bool selected = false;
    private float curDegreesMoved = 0;
    private SphericalCoordinates startPosition;
    private SphericalCoordinates curPosition;


	// Use this for initialization
	void Start () {
        startPosition = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, 0, 2 * Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        startPosition.FromCartesian(transform.position);
        curPosition = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, 0, 2 * Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        curPosition.FromCartesian(transform.position);
    }

    // Update is called once per frame
    void LateUpdate () {
        if (selected)
            ProcessMovement();
        
	}

    void ProcessMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (movementDimension == 0 && horizontal != 0)
        {

            float degrees = angularSpeed * Time.deltaTime;
            //Debug.Log(degrees);
            //startPostion.polar - curPosition.polar + degrees;
            curPosition.RotatePolarAngle(degrees);
            Debug.Log("ENTERED MOVEMENT CONDITION");
            Debug.Log(curPosition.toCartesian);
            transform.position = curPosition.toCartesian;
            transform.LookAt(Vector3.zero);

        } else if (vertical !=0)
        {
            float degrees = angularSpeed * Time.deltaTime;
            curPosition.RotateElevationAngle(degrees);
            transform.position = curPosition.toCartesian;
            transform.LookAt(Vector3.zero);
        }

        
    }

    void OnMouseDown()
    {
        selected = !selected;
    }
}
