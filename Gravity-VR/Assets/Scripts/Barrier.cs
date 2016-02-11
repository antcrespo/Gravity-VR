using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MeshRenderer))]
public class Barrier : MonoBehaviour {
	
	public float radius = 20f; //radius of the circle this would fit in
	public int segments = 5; //number of triangles to split the long component into
	public GameObject template;
    private GameObject next;
	SphericalCoordinates p1, p2, p3, p4;
	//p1----p2
	//|     |
	//|     |
	//|     |
	//p3----p4
	//states are 'i'nactive, 'd'rawing
	private char state = 'i';
    void Start()
    {
        p1 = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, 0, 2 * Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        p2 = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, 0, 2 * Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        p3 = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, 0, 2 * Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        p4 = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, 0, 2 * Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
    }
	void OnMouseDown() {
		RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        switch (state) {
		case 'i':
                if (Physics.Raycast (ray, out hit))
                {
                    next = Instantiate(template);
                    p1.FromCartesian(hit.point);
				    state = 'd';
			}
			break;
		case 'd':
                if (Physics.Raycast (ray, out hit)) {
				    p4.FromCartesian(hit.point);
				    buildMesh (new Mesh());
                }
			break;
		default:
			break;
		}
	}

	private void buildMesh(Mesh mesh) {
        int triBase;
        int numVertices =  (segments - 1) * 2 + 6;
        int numTriangles =  (4 * segments) + 4;

		p2.SetRotation (p1.polar, p4.elevation);
		p3.SetRotation (p4.polar, p1.elevation);
        float polarStep =  (p4.polar - p1.polar) / segments;
        polarStep = polarStep > 0 ? polarStep - 2 * Mathf.PI : polarStep;
        Debug.Log(p1.polar);
        Debug.Log(p4.polar);
        Debug.Log(polarStep);
        float eleStep = 0;//(p1.elevation - p4.elevation) / segments;
        //set the smaller value to 0
        //polarStep = Mathf.Abs(polarStep) > Mathf.Abs(eleStep) ? 0 : polarStep;
        //eleStep = polarStep == 0 ? eleStep : 0;

        Vector3 p1Cart, p2Cart, p3Cart, p4Cart;
        p1Cart = p1.toCartesian;
        p2Cart = p2.toCartesian;
        p3Cart = p3.toCartesian;
        p4Cart = p4.toCartesian;
        
        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[3*numTriangles];
        Vector3 leftMid = (p1Cart+ p3Cart)/ 2;
        Vector3 rightMid = (p2Cart + p4Cart) / 2;
        Vector3 center = (leftMid + rightMid)/ 2;
        vertices[0] = p1Cart-center;
        vertices[1] = p2Cart-center;
        vertices[2] = p3Cart-center;
        vertices[3] = p4Cart - center;

        vertices[4] = leftMid - center;
        vertices[5] = rightMid - center;

        triangles[0] = 0;
        triangles[1] = 5;
        triangles[2] = 1;

        triangles[3] = 0;
        triangles[4] = 4;
        triangles[5] = 5;

        triangles[6] = 4;
        triangles[7] = 2;
        triangles[8] = 5;

        triangles[9] = 5;
        triangles[10] = 2;
        triangles[11] = 3;

        int leftIdx = 0;
        int rightIdx = 1;
        for (int i = 1; i < segments; i++) {
            SphericalCoordinates newLeft = p1.Rotate(polarStep, i*eleStep);
            Debug.Log(newLeft.polar);
            SphericalCoordinates newRight = p2.Rotate(polarStep, i*eleStep);
            Debug.Log(newRight.polar);
            Vector3 nRCart = newRight.toCartesian - center;
            Vector3 nLCart = newLeft.toCartesian - center;

            int nLIdx = 2 * i + 4;
            int nRIdx = 2 * i + 5;
            vertices[nLIdx] = nLCart;
            vertices[nRIdx] = nRCart;

            triBase = 12 * i;
            triangles[triBase] = nLIdx;
            triangles[triBase+1] = 4;
            triangles[triBase + 2] = leftIdx;

            triangles[triBase + 3] = nLIdx;
            triangles[triBase + 4] = leftIdx;
            triangles[triBase + 5] = nRIdx;

            triangles[triBase + 6] = nRIdx;
            triangles[triBase + 7] = leftIdx;
            triangles[triBase + 8] = rightIdx;

            triangles[triBase + 9] = nRIdx;
            triangles[triBase + 10] = rightIdx;
            triangles[triBase + 11] = 5;

            leftIdx = nLIdx;
            rightIdx = nRIdx;
        }

        triBase = (3*numTriangles)-12;
        triangles[triBase] = 2;
        triangles[triBase + 1] = 4;
        triangles[triBase + 2] = leftIdx;

        triangles[triBase + 3] = 2;
        triangles[triBase + 4] = leftIdx;
        triangles[triBase + 5] = 3;

        triangles[triBase + 6] = 3;
        triangles[triBase + 7] = leftIdx;
        triangles[triBase + 8] = rightIdx;

        triangles[triBase + 9] = 3;
        triangles[triBase + 10] = rightIdx;
        triangles[triBase + 11] = 5;
        
        state = 'i';
        mesh.name = "Barrier";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        next.GetComponent<MeshFilter>().sharedMesh = mesh;
        next.transform.position = center;
	}
}
