using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {
    public float radius;
    
    private float elevationAngle;
    private float polarAngle;
    private bool activeAdjustment;
    private int direction;

	// Use this for initialization
	void Start () {
        activeAdjustment = false;
	}

    void OnCollisionEnter(Collision collision)
    {
        Slider s = collision.gameObject.GetComponent<Slider>();
        if (s != null)
        {
            Vector3 startPos = gameObject.transform.position;
            SphericalCoordinates sc = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
            sc.FromCartesian(startPos);
            elevationAngle = sc.elevation;
            polarAngle = sc.polar;
            activeAdjustment = true;
            direction = s.movementDimension;
        }
    }

    void Update()
    {
        if (activeAdjustment)
        {
            Vector3 currentPos = gameObject.transform.position;

            SphericalCoordinates sc = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
            sc.FromCartesian(currentPos);

            if (direction == 0)
            {
                sc.SetElevationAngle(elevationAngle);
                gameObject.transform.position = sc.toCartesian;
            } else
            {
                sc.SetPolarAngle(polarAngle);
                gameObject.transform.position = sc.toCartesian;
            }
        }
    }
}
