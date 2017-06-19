using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notefalling : MonoBehaviour {

	float noteSpeed;

	// Use this for initialization
	void Start () {
		Debug.Log ("Top highway "+ this.transform.localPosition.y);
	}
	
	// Update is called once per frame
	void Update () {
		//this.transform.Translate(0,noteSpeed*Time.deltaTime,0);
	}
}
