using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotateBaseCamera : MonoBehaviour {

    public float rotateSpeed = 10.0F;

    private Transform player;
    private GameObject pc;
    // Use this for initialization
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //見つからない場合は自身を設定
        if (player == null)     player = transform;
    }

    // Update is called once per frame
    void Update()
    {

        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");
        //アナログスティックのグラつきを想定して±0.01以下をはじく
        if (Mathf.Abs(inputHorizontal) + Mathf.Abs(inputVertical) > 0.01F)
        {
            //カメラからみたプレイヤーの方向ベクトル
            Vector3 camToPlayer = player.position - pc.transform.position;
            // π/2 - atan2(x,y) == atan2(y,x)
            float inputAngle = Mathf.Atan2(inputHorizontal, inputVertical) * Mathf.Rad2Deg;
            float cameraAngle = Mathf.Atan2(camToPlayer.x, camToPlayer.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, inputAngle + cameraAngle, 0);
            //deltaTimeを用いることで常に一定の速度になる
             player.rotation = Quaternion.Slerp(player.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
    }
}
