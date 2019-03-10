using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSearch : MonoBehaviour {

    private GolemMove owner;
    private Transform playerTransform;
    
	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        owner = this.transform.parent.gameObject.GetComponent<GolemMove>() ;
	}
	
	// Update is called once per frame
	void Update () {

	}

    //他のCollisionと衝突したときの処理
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //プレイヤーと敵の間にレイを飛ばし、Filedレイヤーに触れずにプレイヤーに衝突したらChaseモードにする
            if (!Physics.Linecast(transform.parent.transform.position, playerTransform.position, LayerMask.GetMask("Field")))
            {
                //本体にプレイヤー感知メッセージを送る
                owner.SetGolemState("Chase");
            }
        }
    }
}
