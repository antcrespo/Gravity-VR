using UnityEngine;
using System.Collections;
using VolumetricLines;

/// <summary>
/// Create a spiral shaped line strip using a volumetric line strip
/// </summary>
/// 
//[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(VolumetricLineStripBehavior))]
public class Spiral : MonoBehaviour
{
    public int m_numVertices = 50;
    public Material m_volumetricLineStripMaterial;
    public Color m_color;
    public float m_start = 0f;
    public float m_rotations = 3;
    public float width = .1f;
    public float lineWidth = .5f;
    public float m_offset = .01f;
    public float polar;
    public float elevation;
    public float radius
    {
        get { return width*m_rotations*2*Mathf.PI; }
    }

    public void BuildSpiral()
    {
        // Create an empty game object
        GameObject go = new GameObject();
        go.name = "Child Spiral";
        go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;

        // Add the MeshFilter component, VolumetricLineStripBehavior requires it
        go.AddComponent<MeshFilter>();

        // Add a MeshRenderer, VolumetricLineStripBehavior requires it, and set the material
        var meshRenderer = go.AddComponent<MeshRenderer>();
        meshRenderer.material = m_volumetricLineStripMaterial;

        // Add the VolumetricLineStripBehavior and set parameters, like color and all the vertices of the line
        var volLineStrip = go.AddComponent<VolumetricLineStripBehavior>();
        volLineStrip.SetLineColorAtStart = true;
        volLineStrip.LineColor = m_color;
        volLineStrip.LineWidth = lineWidth;

        float dist = Mathf.Sqrt(900 - Mathf.Pow(radius, 2)) - m_offset;
        SphericalCoordinates sc = new SphericalCoordinates(dist, polar*Mathf.Deg2Rad, elevation*Mathf.Deg2Rad, 1, 30.3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        Vector3 position = sc.toCartesian;
        var lineVertices = new Vector3[m_numVertices];
        for (int i = 0; i < m_numVertices; ++i)
        {
            float t = Mathf.Lerp(m_start, m_rotations * 2 * Mathf.PI, (float)i / (float)(m_numVertices - 1));
            float x = width * t * Mathf.Cos(t);
            float y = width * t * Mathf.Sin(t);
            lineVertices[i] = new Vector3(x, y, 0f);
        }

        volLineStrip.UpdateLineVertices(lineVertices);

        
        transform.position = position;
    }

    void Update()
    {
        transform.Rotate(0, 0, 100 * Time.deltaTime);
    }


    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < m_numVertices; ++i)
        {
            float t = Mathf.Lerp(m_start, m_end, (float)i / (float)(m_numVertices - 1));
            float x = width * t * Mathf.Cos(t);
            float y = width * t * Mathf.Sin(t);
            Gizmos.DrawSphere(gameObject.transform.TransformPoint(new Vector3(x, y, 0f)), 5f);
        }
    }*/
}

