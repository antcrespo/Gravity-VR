using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class WedgeLook : MonoBehaviour
{

    void Update()
    {
        LookAtCenter();
    }


    public void LookAtCenter()
    {
        transform.LookAt(Vector3.zero);
        transform.Rotate(45, 90, 0, Space.Self);
    }
}
