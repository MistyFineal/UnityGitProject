using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSkill1Area : MonoBehaviour {
    private GolemMove owner;
    // Transform playerTransform;

    // Use this for initialization
    void Start()
    {
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        owner = this.transform.parent.gameObject.GetComponent<GolemMove>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    //他のCollisionと衝突したときの処理
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            //敵がChaseモードであったら
            if (owner.GetGolemState() == "Chase")
            {
                //本体にSkill1メッセージを送る
                owner.SetGolemState("Skill1");
                owner.GetAnimator().SetTrigger("Skill1");
            }
        }
    }
}
