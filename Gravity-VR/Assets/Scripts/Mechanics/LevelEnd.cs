using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {
    private LevelInfo info;
	// Use this for initialization
	void Start () {
        info = GameObject.FindGameObjectWithTag("Info").GetComponent<LevelInfo>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();
            info.LoadNext();
        }
    }
}
