using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LookAt : MonoBehaviour
{

    void Update()
    {
        transform.LookAt(Vector3.zero);
    }

}