using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteFalling : MonoBehaviour {

	public float noteSpeed;
    public float resetYLocation = 0.5f;
    public float destroyYLocation = -0.5f;

	// Use this for initialization
	void Start () {
        //top 0.5
        //bottom -0.5
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(0,-1*noteSpeed*Time.deltaTime,0);
        if (this.transform.localPosition.y < destroyYLocation) {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, resetYLocation, this.transform.localPosition.z);
        }
	}
}
