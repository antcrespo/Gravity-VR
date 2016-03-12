using UnityEngine;
using System.Collections;
using VolumetricLines;

/// <summary>
/// Create a spiral shaped line strip using a volumetric line strip
/// </summary>
/// 
//[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(VolumetricLineStripBehavior))]
public class CircleVolumetric : MonoBehaviour
{
    public int m_numVertices = 50;
    public Material m_volumetricLineStripMaterial;
    public Material m_volumetricLineMaterial;
    public Color m_color;
    public float radius = 1f;
    public float lineWidth = .5f;
    public float m_offset = .01f;
    public float polar;
    public float elevation;
    

    public bool isSelected = false;

    void Start()
    {
 
    }


    public void BuildCircle()
    {
        // check if game object exists as child
        MeshRenderer meshRenderer;
        VolumetricLineStripBehavior volLineStrip;
        GameObject go;

        var childExists = transform.Find("Child Circle");

        if (childExists == null)
        {
            // Create an empty game object
            go = new GameObject();
            go.name = "Child Circle";
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = new Vector3(1, 1);

            // Add the MeshFilter component, VolumetricLineStripBehavior requires it
            go.AddComponent<MeshFilter>();

            // Add a MeshRenderer, VolumetricLineStripBehavior requires it, and set the material
            meshRenderer = go.AddComponent<MeshRenderer>();

            // Add the VolumetricLineStripBehavior and set parameters, like color and all the vertices of the line
            volLineStrip = go.AddComponent<VolumetricLineStripBehavior>();
        }
        else {
            go = childExists.gameObject;
            meshRenderer = go.GetComponent<MeshRenderer>();
            volLineStrip = go.GetComponent<VolumetricLineStripBehavior>();
        }

        meshRenderer.material = m_volumetricLineStripMaterial;

        volLineStrip.SetLineColorAtStart = true;
        volLineStrip.LineColor = m_color;
        volLineStrip.LineWidth = lineWidth;


        float dist = Mathf.Sqrt(900 - Mathf.Pow(radius, 2)) - m_offset;
        SphericalCoordinates sc = new SphericalCoordinates(dist, polar * Mathf.Deg2Rad, elevation * Mathf.Deg2Rad, 1, 30.3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        Vector3 position = sc.toCartesian;


        var lineVertices = new Vector3[m_numVertices];
        for (int i = 0; i < m_numVertices; ++i)
        {
            float t = Mathf.Lerp(0f, 2 * Mathf.PI, (float)i / (float)(m_numVertices - 1));
            float x = radius * Mathf.Cos(t);
            float y = radius * Mathf.Sin(t);
            lineVertices[i] = new Vector3(x, y, 0f);
        }

        volLineStrip.UpdateLineVertices(lineVertices);


        transform.position = position;
    }

    public void BuildX()
    {
        // check if game object exists as child
        MeshRenderer meshRenderer1, meshRenderer2;
        VolumetricLineBehavior volLineStrip1, volLineStrip2;
        GameObject go1, go2;

        var childExists = transform.Find("Child X 1");

        if (childExists == null)
        {
            // Create an empty game object
            go1 = new GameObject();
            go1.name = "Child X 1";
            go1.transform.parent = transform;
            go1.transform.localPosition = Vector3.zero;
            go1.transform.localRotation = Quaternion.identity;
            go1.transform.localScale = new Vector3(1, 1);

            // Add the MeshFilter component, VolumetricLineStripBehavior requires it
            go1.AddComponent<MeshFilter>();

            // Add a MeshRenderer, VolumetricLineStripBehavior requires it, and set the material
            meshRenderer1 = go1.AddComponent<MeshRenderer>();

            // Add the VolumetricLineStripBehavior and set parameters, like color and all the vertices of the line
            volLineStrip1 = go1.AddComponent<VolumetricLineBehavior>();

            // Create an empty game object
            go2 = new GameObject();
            go2.name = "Child X 1";
            go2.transform.parent = transform;
            go2.transform.localPosition = Vector3.zero;
            go2.transform.localRotation = Quaternion.identity;
            go2.transform.localScale = new Vector3(1, 1);

            // Add the MeshFilter component, VolumetricLineStripBehavior requires it
            go2.AddComponent<MeshFilter>();

            // Add a MeshRenderer, VolumetricLineStripBehavior requires it, and set the material
            meshRenderer2 = go2.AddComponent<MeshRenderer>();

            // Add the VolumetricLineStripBehavior and set parameters, like color and all the vertices of the line
            volLineStrip2 = go2.AddComponent<VolumetricLineBehavior>();
        }
        else {
            go1 = childExists.gameObject;
            meshRenderer1 = go1.GetComponent<MeshRenderer>();
            volLineStrip1 = go1.GetComponent<VolumetricLineBehavior>();

            go2 = childExists.gameObject;
            meshRenderer2 = go2.GetComponent<MeshRenderer>();
            volLineStrip2 = go2.GetComponent<VolumetricLineBehavior>();
        }

        meshRenderer1.material = m_volumetricLineMaterial;
        meshRenderer2.material = m_volumetricLineMaterial;

        volLineStrip1.LineColor = m_color;
        volLineStrip1.LineWidth = lineWidth;

        volLineStrip2.LineColor = m_color;
        volLineStrip2.LineWidth = lineWidth;

        volLineStrip1.StartPos = new Vector3(radius, radius, 0f);
        volLineStrip1.EndPos = new Vector3(-radius, -radius, 0f);
        volLineStrip2.StartPos = new Vector3(-radius, radius, 0f);
        volLineStrip2.EndPos = new Vector3(radius, -radius, 0f);

    }
}

