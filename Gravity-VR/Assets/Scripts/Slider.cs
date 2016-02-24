using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour {
    //public Vector3 movement;
    public int movementDimension = 0; // 0 = x, 1 = y
    public float radius = 30f;
    public float minPolar = -15;
    public float maxPolar = 15;
    public float minElevation = -15;
    public float maxElevation = 15;
    public float angularSpeed = .1f;

    public bool selected = false;
    public float curDegreesMoved = 0;
    public SphericalCoordinates startPosition;
    public SphericalCoordinates curPosition;


	// Use this for initialization
	void Start () {
        startPosition = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, minPolar*Mathf.Deg2Rad, maxPolar * Mathf.Deg2Rad, minElevation * Mathf.Deg2Rad, maxElevation * Mathf.Deg2Rad);
        startPosition.loopPolar = false;
        startPosition.FromCartesian(transform.position);
        curPosition = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, minPolar * Mathf.Deg2Rad, maxPolar * Mathf.Deg2Rad, minElevation * Mathf.Deg2Rad, maxElevation * Mathf.Deg2Rad);
        curPosition.loopPolar = false;
        curPosition.FromCartesian(transform.position);
    }

    // Update is called once per frame
    void LateUpdate () {
        if (selected)
            ProcessMovement();
        
	}

    void ProcessMovement()
    {
        float horizontal = -Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (movementDimension == 0 && horizontal != 0)
        {
            int sign = horizontal > 0 ? 1 : -1;
            float degrees = angularSpeed * Time.deltaTime * sign;
            //Debug.Log(degrees);
            //startPostion.polar - curPosition.polar + degrees;
            curPosition.RotatePolarAngle(degrees);
            //Debug.Log("ENTERED MOVEMENT CONDITION");
            //Debug.Log(curPosition.toCartesian);
            transform.position = curPosition.toCartesian;
            //Debug.Log(transform.rotation);
            //transform.LookAt(Vector3.zero);
            //transform.Rotate(0, 90, 0, Space.Self);
            //Debug.Log(transform.rotation);

        } else if (movementDimension == 1 && vertical !=0)
        {
            int sign = vertical > 0 ? 1 : -1;
            float degrees = angularSpeed * Time.deltaTime * sign;
            curPosition.RotateElevationAngle(degrees);
            transform.position = curPosition.toCartesian;
            //transform.LookAt(Vector3.zero);
            //transform.Rotate(0, 90, 0, Space.Self);
        }

        
    }

    /*void OnMouseDown()
    {
        selected = !selected;
    }
    */

    public void OnPointerClick()
    {
        selected = !selected;
        Debug.Log("CLICK triggered");
    }
}
