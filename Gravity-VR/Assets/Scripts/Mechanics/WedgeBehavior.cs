using UnityEngine;
using System.Collections;
using VolumetricLines;

public class WedgeBehavior : MonoBehaviour {

    public BoxCollider bottom;
    public int sign;
    public Color startColor;

    public bool selected = false;
    private bool seen = false;



    // Use this for initialization
    void Start () {
        
    }


    void Update()
    {
        if (Input.GetButtonDown("X"))
        {
            //Debug.Log("X pressed");
            ProcessRotation();
        }

    }


    public void ProcessRotation()
    {
        if (seen)
        {
            transform.rotation = transform.rotation * Quaternion.Euler(0f, 0f, 90f);
        }
    }


    public void OnPointer()
    {
        seen = !seen;
        Color color = seen ? Color.yellow : startColor;
        setLineColors(color);
    }

    private void setLineColors(Color color)
    {
        VolumetricLineBehavior[] lines = GetComponentsInChildren<VolumetricLineBehavior>();
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i].LineColor = color;
        }
    }


}
