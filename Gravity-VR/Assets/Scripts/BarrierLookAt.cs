using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BarrierLookAt : MonoBehaviour {

    void Update()
    {
        LookAtCenter();
    }

    
    public void LookAtCenter()
    {
        transform.LookAt(Vector3.zero);
        transform.Rotate(0, 90, 0, Space.Self);
    }
}
