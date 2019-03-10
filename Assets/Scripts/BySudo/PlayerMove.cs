using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float speed;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () { 

        //座標移動
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))       this.transform.position += this.transform.forward.normalized * speed ;
    }
}
