using UnityEngine;
using System.Collections;

public class BarrierEditor : MonoBehaviour
{
    public int segments = 4;
    public float polarAngle;
    public float elevationAngle;
    public float radius = 20f;
    public float height = 3f;
    public GameObject prefab;

    public void BuildWall()
    {

        SphericalCoordinates p1 = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        SphericalCoordinates p2 = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        SphericalCoordinates p3 = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        SphericalCoordinates p4 = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        int triBase;
        int numVertices = (segments - 1) * 2 + 12;
        int numTriangles = (4 * segments) + 16;
        float polarDif = (polarAngle*Mathf.Deg2Rad) / 2;
        float eleDif = (elevationAngle*Mathf.Deg2Rad) / 2f;
        //Debug.Log(polarDif);
        p1.SetRotation(-polarDif, eleDif);
        p2.SetRotation(-polarDif, -eleDif);
        p3.SetRotation(polarDif, eleDif);
        p4.SetRotation(polarDif, -eleDif);
        //.Log(p1.ToString());
        //Debug.Log(p2.ToString());
        //Debug.Log(p3.ToString());
        //Debug.Log(p4.ToString());
        float polarStep = (polarAngle * Mathf.Deg2Rad) / segments;
        //polarStep = polarStep > 0 ? polarStep - 2 * Mathf.PI : polarStep;

        float eleStep = 0;//(p1.elevation - p4.elevation) / segments;
        //set the smaller value to 0
        //polarStep = Mathf.Abs(polarStep) > Mathf.Abs(eleStep) ? 0 : polarStep;
        //eleStep = polarStep == 0 ? eleStep : 0;

        Vector3 p1Cart, p2Cart, p3Cart, p4Cart;

        p1Cart = p1.toCartesian;
        p2Cart = p2.toCartesian;
        p3Cart = p3.toCartesian;
        p4Cart = p4.toCartesian;

        Vector3[] normals = new Vector3[numVertices];
        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[3 * numTriangles];
        Vector3 leftMid = (p1Cart + p3Cart) / 2;
        Vector3 rightMid = (p2Cart + p4Cart) / 2;
        Vector3 center = (leftMid + rightMid) / 2;
        vertices[0] = p1Cart - center;
        vertices[1] = p2Cart - center;
        vertices[2] = p3Cart - center;
        vertices[3] = p4Cart - center;

        vertices[4] = leftMid - center;
        vertices[5] = rightMid - center;

        Vector3 norm = -center.normalized;// Vector3.Cross ((p1Cart - p2Cart), (p4Cart - p2Cart)).normalized;
        for (int i = 0; i < 6; i++)
        {
            vertices[i + 6] = vertices[i] + height * norm;
        }
        //top rectangle
        triangles[0] = 10;
        triangles[1] = 6;
        triangles[2] = 7;

        triangles[3] = 10;
        triangles[4] = 7;
        triangles[5] = 11;

        triangles[6] = 8;
        triangles[7] = 10;
        triangles[8] = 11;

        triangles[9] = 8;
        triangles[10] = 11;
        triangles[11] = 9;

        //left side rectangle
        triangles[12] = 10;
        triangles[13] = 4;
        triangles[14] = 6;

        triangles[15] = 6;
        triangles[16] = 4;
        triangles[17] = 0;

        triangles[18] = 2;
        triangles[19] = 4;
        triangles[20] = 10;

        triangles[21] = 8;
        triangles[22] = 2;
        triangles[23] = 10;

        //right side rectangle
        for (int i = 0; i < 12; i += 3)
        {
            triangles[i + 24] = triangles[i + 12] + 1;
            triangles[i + 25] = triangles[i + 14] + 1;
            triangles[i + 26] = triangles[i + 13] + 1;
        }

        //top rectangle
        triangles[36] = 7;
        triangles[37] = 6;
        triangles[38] = 1;

        triangles[39] = 1;
        triangles[40] = 6;
        triangles[41] = 0;

        //bottom rectangle
        triangles[42] = 8;
        triangles[43] = 9;
        triangles[44] = 2;

        triangles[45] = 2;
        triangles[46] = 9;
        triangles[47] = 3;

        int leftIdx = 0;
        int rightIdx = 1;
        for (int i = 1; i < segments; i++)
        {
            SphericalCoordinates newLeft = p1.Rotate(polarStep, eleStep);
            SphericalCoordinates newRight = p2.Rotate(polarStep, eleStep);
            Debug.Log(newLeft.ToString());
            Debug.Log(newRight.ToString());
            Vector3 nRCart = newRight.toCartesian - center;
            Vector3 nLCart = newLeft.toCartesian - center;

            int nLIdx = 2 * i + 10;
            int nRIdx = 2 * i + 11;
            vertices[nLIdx] = nLCart;
            vertices[nRIdx] = nRCart;

            triBase = 12 * i + 36;

            triangles[triBase] = 4;
            triangles[triBase + 1] = nLIdx;
            triangles[triBase + 2] = leftIdx;

            triangles[triBase + 3] = leftIdx;
            triangles[triBase + 4] = nLIdx;
            triangles[triBase + 5] = nRIdx;

            triangles[triBase + 6] = leftIdx;
            triangles[triBase + 7] = nRIdx;
            triangles[triBase + 8] = rightIdx;

            triangles[triBase + 9] = 5;
            triangles[triBase + 10] = rightIdx;
            triangles[triBase + 11] = nRIdx;

            leftIdx = nLIdx;
            rightIdx = nRIdx;
        }

        triBase = (3 * numTriangles) - 12;
        triangles[triBase] = 4;
        triangles[triBase + 1] = 2;
        triangles[triBase + 2] = leftIdx;

        triangles[triBase + 3] = 3;
        triangles[triBase + 4] = leftIdx;
        triangles[triBase + 5] = 2;

        triangles[triBase + 6] = leftIdx;
        triangles[triBase + 7] = 3;
        triangles[triBase + 8] = rightIdx;

        triangles[triBase + 9] = 5;
        triangles[triBase + 10] = rightIdx;
        triangles[triBase + 11] = 3;

        //state = 'i';
        Mesh mesh = new Mesh();
        mesh.name = "Barrier";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        //mesh.RecalculateNormals();

        GameObject next = Instantiate(prefab, center, Quaternion.identity) as GameObject;
        next.GetComponent<MeshFilter>().sharedMesh = mesh;
        next.GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}