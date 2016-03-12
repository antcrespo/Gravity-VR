using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {
    public float radius;
    private float speed = 0;
    private float elevationAngle;
    private float polarAngle;
    private bool activeAdjustment;
    private int direction;
    private int sign;
	// Use this for initialization
	void Start () {
        activeAdjustment = false;
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Slider"))
        {
            Slider s = collision.gameObject.GetComponent<Slider>();
            if (s != null)
            {
                direction = s.movementDimension;
                speed = s.angularSpeed;
                sign = s.getSign();
            }
        } else if (collision.gameObject.CompareTag("Wedge"))
        {
            //Debug.Log("Hit Wedge");
            direction = direction == 1 ? 0 : 1;
            if (collision.collider.Equals(collision.gameObject.GetComponent<WedgeBehavior>().top))
            {
                sign = 1;
                //Debug.Log("Top");
            } else
            {
                sign = -1;
                //Debug.Log("Bottom");
            }

        }

        Vector3 startPos = gameObject.transform.position;
        SphericalCoordinates sc = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        sc.FromCartesian(startPos);
        elevationAngle = sc.elevation;
        polarAngle = sc.polar;
        activeAdjustment = true;
    }

    void Update()
    {
        if (activeAdjustment)
        {
            Vector3 currentPos = gameObject.transform.position;

            SphericalCoordinates sc = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
            sc.loopPolar = true;
            sc.FromCartesian(currentPos);
            //sc.SetRadius(radius);
            float change = sign * speed * Time.deltaTime;
            if (direction == 0)
            {
                sc.RotatePolarAngle(change);
                gameObject.transform.position = sc.toCartesian;
            } else
            {
                sc.RotateElevationAngle(change);
                gameObject.transform.position = sc.toCartesian;
            }
        }
    }
}
