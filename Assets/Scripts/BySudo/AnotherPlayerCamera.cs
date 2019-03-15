using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherPlayerCamera : MonoBehaviour {

    public float rotateSpeed;

    private GameObject targetObj;
    private Vector3 targetPos;
    private const float maxAngle = 50.0f;
    private const float minAngle = 3.0f;

    private Vector3 roteuler;

    // Use this for initialization
    void Start()
    {
        targetObj = GameObject.FindGameObjectWithTag("Player");
        targetPos = targetObj.transform.position;
        roteuler = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // targetの移動量分、自分（カメラ）も移動する
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;

        // マウスの右クリックを押している間(コントローラ設定はしらんめう)
        if (Input.GetMouseButton(1))
        {
            // マウスの移動量
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");

            //roteuler = new Vector3(Mathf.Clamp(roteuler.x - mouseInputY, -80, 80), roteuler.y + mouseInputX, 0f);

            //transform.localEulerAngles = roteuler;

            
            // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * rotateSpeed);
            // カメラの縦回転（※角度制限なし、調整よろ）
            
            if ( (minAngle < transform.localEulerAngles.x && transform.localEulerAngles.x < maxAngle)
                || (transform.localEulerAngles.x <= minAngle && mouseInputY < 0) 
                || (transform.localEulerAngles.x >= maxAngle && mouseInputY >0) )
            {
                transform.RotateAround(targetPos, transform.right, -mouseInputY * Time.deltaTime * rotateSpeed);
            }
            
            
            //調整なんとかする
            if (transform.localEulerAngles.x <= minAngle)
            {
                transform.localEulerAngles.Set(minAngle, transform.localEulerAngles.y, 0f);
            }
            else if (transform.localRotation.x >= maxAngle) {
                transform.localEulerAngles.Set(maxAngle, transform.localEulerAngles.y, 0f);
            }
            

            //Debug.Log("nowAngle: " + nowAngle);
            Debug.Log("localEulerAngles: " + transform.localEulerAngles);
        }
    }
}
