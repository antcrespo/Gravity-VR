using UnityEngine;
using System.Collections;

public class LaserBehaivor : MonoBehaviour {
    public int direction = 0;
    public int sign = 1;
    public float radius = 28f;
    public float angularSpeed = 6f;
    public float lifetime = 2f;

    private SphericalCoordinates curPos;

	// Use this for initialization
	void Start () {
        curPos = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        curPos.loopPolar = true;
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update () {

        curPos.FromCartesian(gameObject.transform.position);
        float radians = angularSpeed * Time.deltaTime * sign;
        if (direction == 0)
        {
            curPos.RotatePolarAngle(radians);
        } else
        {
            curPos.RotateElevationAngle(radians);
        }
        transform.position = curPos.toCartesian;
    }
}
