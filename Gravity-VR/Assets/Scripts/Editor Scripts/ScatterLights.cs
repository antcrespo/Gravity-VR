using UnityEngine;
using System.Collections;

public class ScatterLights : MonoBehaviour {

    public float sphereRadius = 30;
    public int total;

	// Use this for initialization
	public void CreateLights() {
        for (int i=0; i<total; i++)
        {
            float polarAngle = Random.Range(-Mathf.PI, Mathf.PI);
            float elevationAngle = Random.Range (- Mathf.PI / 2, Mathf.PI / 2);
            float radius = sphereRadius - 0.6f + Random.Range(0, 0.4f);

            SphericalCoordinates sc = new SphericalCoordinates(radius, polarAngle, elevationAngle, 1, sphereRadius, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
            //sc.SetRotation(polarAngle * Mathf.Deg2Rad, elevationAngle * Mathf.Deg2Rad);

            GameObject cur = new GameObject("The Light " + i);
            cur.transform.position = sc.toCartesian;
            cur.transform.parent = transform;

            Light lightComp = cur.AddComponent<Light>();
            lightComp.type = LightType.Point;
            cur.isStatic = true;
            
            //GameObject cur = (GameObject) Instantiate(lightPrefab, sc.toCartesian, Quaternion.identity);
            
        }
	
	}
}


/*
public float radius = 0;
public float polarAngle = 0;
public float elevationAngle = 0;
private SphericalCoordinates sc;
private float maxRadius = 30f;

void Start()
{
    sc = new SphericalCoordinates(radius, 0, 0, 1, maxRadius, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
}

// Update is called once per frame
void Update()
{
    if (sc == null)
        sc = new SphericalCoordinates(radius, 0, 0, 1, maxRadius, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);

    sc.SetRadius(radius);+
    sc.SetRotation(polarAngle * Mathf.Deg2Rad, elevationAngle * Mathf.Deg2Rad);

    transform.position = sc.toCartesian;
}

*/
