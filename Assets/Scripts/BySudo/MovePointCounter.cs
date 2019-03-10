using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointCounter : MonoBehaviour {

    private GolemMove owner;

    // Use this for initialization
    void Start () {
        owner = this.transform.parent.gameObject.GetComponent<GolemMove>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.gameObject.tag == "MovePoint" && owner.GetGolemState() == "Loitering")
        {
            int pointNum = other.transform.parent.transform.childCount;
            int pointer = owner.GetGolemMovePointer();
            //往路復路のスイッチ
            if (pointer == pointNum)
            {
                owner.SetGolemIsOutward(false);
            }
            else if (pointer == 1)
            {
                owner.SetGolemIsOutward(true);
            }

            //ポインタを次に進める
            if (owner.GetGolemIsOutward())
            {
                pointer++;
                owner.SetGolemMovePointer(pointer);
            }
            else
            {
                pointer--;
                owner.SetGolemMovePointer(pointer);
            }
        }
    }
}
