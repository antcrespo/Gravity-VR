using UnityEngine;
using System.Collections;
using VolumetricLines;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class BarrierEditor : MonoBehaviour
{
    public int segments = 4;
    public float polarSize = 20f;
    public float elevationSize = 10f;
    public float radius = 20f;
    public float height = 3f;
    public float elevationAngle;
    public float polarAngle;
    private Vector3 topLeft, bottomLeft, topRight, bottomRight;
    public GameObject edgePrefab;
    private GameObject top, bottom, left, right;

    public void BuildWall()
    {

        SphericalCoordinates p1 = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        SphericalCoordinates p2 = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        SphericalCoordinates p3 = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        SphericalCoordinates p4 = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        int triBase;
        int numVertices = (segments) * 4 + 28;
        int numTriangles = (4 * segments) + 16;
        float polarDif = (polarSize * Mathf.Deg2Rad) / 2;
        float eleDif = (elevationSize * Mathf.Deg2Rad) / 2f;

        p1.SetRotation(-polarDif, eleDif);
        p2.SetRotation(-polarDif, -eleDif);
        p3.SetRotation(polarDif, eleDif);
        p4.SetRotation(polarDif, -eleDif);
        float polarStep = (polarSize * Mathf.Deg2Rad) / segments;

        float eleStep = 0;//(p1.elevation - p4.elevation) / segments;

        Vector3 p1Cart, p2Cart, p3Cart, p4Cart;

        p1Cart = p1.toCartesian;
        p2Cart = p2.toCartesian;
        p3Cart = p3.toCartesian;
        p4Cart = p4.toCartesian;

        //Vector3[] normals = new Vector3[numVertices];
        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[3 * numTriangles];
        Vector3 leftMid = (p1Cart + p3Cart) / 2;
        Vector3 rightMid = (p2Cart + p4Cart) / 2;
        Vector3 center = (leftMid + rightMid) / 2;
        //int v = 0;
        vertices[0] = p1Cart - center;
        vertices[1] = p2Cart - center;
        vertices[2] = p3Cart - center;
        vertices[3] = p4Cart - center;

        vertices[4] = leftMid - center;
        vertices[5] = rightMid - center;

        //Vector3 verticalNorm = -center.normalized;
        for (int i = 0; i < 6; i++)
        {
            //vertices[i + 6] = vertices[i] + height * norm;
            vertices[i + 6] = vertices[i+12] = vertices[i] - height * (vertices[i] + center).normalized;
            //normals[i + 6] = verticalNorm;
        }
        topLeft = vertices[6];
        topRight = vertices[7];
        bottomLeft = vertices[8];
        bottomRight = vertices[9];
        Vector3 farTopLeft = vertices[0];
        Vector3 farTopRight = vertices[1];
        Vector3 farBottomLeft = vertices[2];
        Vector3 farBottomRight = vertices[3];
        //front rectangle
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
        triangles[12] = 16;
        triangles[13] = 4;
        triangles[14] = 12;

        triangles[15] = 12;
        triangles[16] = 4;
        triangles[17] = 0;

        triangles[18] = 2;
        triangles[19] = 4;
        triangles[20] = 16;

        triangles[21] = 14;
        triangles[22] = 2;
        triangles[23] = 16;

        //right side rectangle
        for (int i = 0; i < 12; i += 3)
        {
            triangles[i + 24] = triangles[i + 12] + 1;
            triangles[i + 25] = triangles[i + 14] + 1;
            triangles[i + 26] = triangles[i + 13] + 1;
        }

        vertices[18] = farTopLeft;
        vertices[19] = farTopRight;
        vertices[20] = farBottomLeft;
        vertices[21] = farBottomRight;
        vertices[22] = topLeft;
        vertices[23] = topRight;
        vertices[24] = bottomLeft;
        vertices[25] = bottomRight;

        //top rectangle
        triangles[36] = 23;
        triangles[37] = 22;
        triangles[38] = 19;

        triangles[39] = 19;
        triangles[40] = 22;
        triangles[41] = 18;

        //bottom rectangle
        triangles[42] = 24;
        triangles[43] = 25;
        triangles[44] = 20;

        triangles[45] = 20;
        triangles[46] = 25;
        triangles[47] = 21;

        vertices[26] = farTopLeft;
        vertices[27] = farTopRight;
        int roundleftIdx = 26;
        int roundrightIdx = 27;
        int flatLeftIdx = 0;
        int flatRightIdx = 1;
        for (int i = 1; i < segments; i++)
        {
            SphericalCoordinates newLeft = p1.Rotate(polarStep, eleStep);
            SphericalCoordinates newRight = p2.Rotate(polarStep, eleStep);
            //Debug.Log(newLeft.ToString());
            //Debug.Log(newRight.ToString());
            Vector3 nRCart = newRight.toCartesian - center;
            Vector3 nLCart = newLeft.toCartesian - center;

            int nLIdx = 4 * i + 26;
            int nRIdx = nLIdx+1;
            int bLIdx = nRIdx + 1;
            int bRIdx = bLIdx + 1;
            vertices[nLIdx] = vertices[bLIdx] = nLCart;
            vertices[nRIdx] = vertices [bRIdx] = nRCart;

            triBase = 12 * i + 36;

            triangles[triBase] = 4;
            triangles[triBase + 1] = bLIdx;
            triangles[triBase + 2] = flatLeftIdx;

            triangles[triBase + 3] = roundleftIdx;
            triangles[triBase + 4] = nLIdx;
            triangles[triBase + 5] = nRIdx;

            triangles[triBase + 6] = roundleftIdx;
            triangles[triBase + 7] = nRIdx;
            triangles[triBase + 8] = roundrightIdx;

            triangles[triBase + 9] = 5;
            triangles[triBase + 10] = flatRightIdx;
            triangles[triBase + 11] = bRIdx;

            roundleftIdx = nLIdx;
            roundrightIdx = nRIdx;
            flatLeftIdx = bLIdx;
            flatRightIdx = bRIdx;
        }

        vertices[flatRightIdx + 1] = farBottomLeft;
        vertices[flatRightIdx + 2] = farBottomRight;
        Debug.Log(flatRightIdx + 2);
        triBase = (3 * numTriangles) - 12;
        triangles[triBase] = 4;
        triangles[triBase + 1] = 2;
        triangles[triBase + 2] = flatLeftIdx;

        triangles[triBase + 3] = flatRightIdx + 2;
        triangles[triBase + 4] = roundleftIdx;
        triangles[triBase + 5] = flatRightIdx + 1;

        triangles[triBase + 6] = roundleftIdx;
        triangles[triBase + 7] = flatRightIdx + 2;
        triangles[triBase + 8] = roundrightIdx;

        triangles[triBase + 9] = 5;
        triangles[triBase + 10] = flatRightIdx;
        triangles[triBase + 11] = 3;

        //state = 'i';
        Mesh mesh = new Mesh();
        mesh.name = "Barrier";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        //GameObject next = Instantiate(prefab, center, Quaternion.identity) as GameObject;
        p1.SetRotation(polarAngle * Mathf.Deg2Rad, elevationAngle * Mathf.Deg2Rad);
        p1.SetRadius(center.magnitude);
        transform.position = p1.toCartesian;
        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        CreateGlow();
    }

    public void CreateGlow()
    {
        Vector3 length = topRight - topLeft;
        Vector3 height = bottomLeft - topLeft;

        if (top == null)
        {
            top = Instantiate(edgePrefab);
            top.name = "top";
            top.transform.parent = transform;

            bottom = Instantiate(edgePrefab);
            bottom.name = "bottom";
            bottom.transform.parent = transform;

            left = Instantiate(edgePrefab);
            left.name = "left";
            left.transform.parent = transform;

            right = Instantiate(edgePrefab);
            right.name = "right";
            right.transform.parent = transform;
        }

        
        top.transform.localPosition = topLeft - .25f * Vector3.right;
        top.transform.localRotation = Quaternion.identity;
        top.GetComponent<VolumetricLineBehavior>().EndPos = length;
       
        bottom.transform.localPosition = bottomLeft - .25f * Vector3.right;
        bottom.transform.localRotation = Quaternion.identity; 
        bottom.GetComponent<VolumetricLineBehavior>().EndPos = length;
       
        left.transform.localPosition = topLeft - .25f * Vector3.right;
        left.transform.localRotation = Quaternion.identity;
        left.GetComponent<VolumetricLineBehavior>().EndPos = height;
       
        right.transform.localPosition = topRight - .25f * Vector3.right;
        right.transform.localRotation = Quaternion.identity;
        right.GetComponent<VolumetricLineBehavior>().EndPos = height;

        return;
    }
}
