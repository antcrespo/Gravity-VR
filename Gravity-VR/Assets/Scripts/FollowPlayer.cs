using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public float distance = 5.0f;
	private GameObject player;
	private Vector3 relPos;
	private GameObject planet;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		planet = GameObject.FindGameObjectWithTag ("Finish");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 dir = (player.transform.position - planet.transform.position);
		transform.position = dir - distance * dir.normalized + planet.transform.position;
		transform.LookAt (player.transform);
	}
}
