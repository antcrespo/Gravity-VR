using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PolarTransform : MonoBehaviour {
    public float radius = 0;
    public float polarAngle = 0;
    public float elevationAngle = 0;
    private SphericalCoordinates sc;
	private float maxRadius = 30f;

    /*void Start()
    {
        sc = new SphericalCoordinates(radius, 0, 0, 1, maxRadius, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
    }*/

	// Update is called once per frame
	void Update () {
        if (sc == null)
            sc = new SphericalCoordinates(radius, 0, 0, 1, maxRadius, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);

        sc.SetRadius(radius);
        sc.SetRotation(polarAngle * Mathf.Deg2Rad, elevationAngle * Mathf.Deg2Rad);

        transform.position = sc.toCartesian;
	}


}
