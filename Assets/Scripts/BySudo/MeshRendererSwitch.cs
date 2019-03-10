using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRendererSwitch : MonoBehaviour {
    public bool isRendering;

	// Use this for initialization
	void Start () {

        //MovePointのレンダリングをするかしないか
        foreach (MeshRenderer mr in this.GetComponentsInChildren<MeshRenderer>())
        {
            mr.enabled = isRendering;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
