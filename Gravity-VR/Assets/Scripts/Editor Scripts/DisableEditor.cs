using UnityEngine;
using System.Collections;

public class DisableEditor : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<LookAt>().enabled = false;
    }
}
