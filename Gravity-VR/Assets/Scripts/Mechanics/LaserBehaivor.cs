using UnityEngine;
using System.Collections;

public class LaserBehaivor : MonoBehaviour {
    public int direction;
    public int sign;
    public float radius;

    private SphericalCoordinates curPos;
    private float angularSpeed = .3f;

	// Use this for initialization
	void Start () {
        curPos = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        curPos.loopPolar = true;
        Destroy(gameObject, 6f);
    }

    // Update is called once per frame
    void Update () {

        curPos.FromCartesian(gameObject.transform.position);
        float degrees = angularSpeed * Time.deltaTime * sign;
        curPos.RotatePolarAngle(degrees);
        transform.position = curPos.toCartesian;
    }
}
