using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
  private GameObject player;
  [SerializeField] private Vector3 offset;

  private Transform cameraTrans;
  [SerializeField] private Transform playerTrans;
  private Vector3 playerPos;

  private float rotateSpeed = 60f;

  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
    playerPos = player.transform.position;
    transform.position = offset;
  }

  void LateUpdate()
  {
    //cameraTrans.position = playerTrans.position + cameraVec;
    transform.position += player.transform.position - playerPos;
    cameraTrans = this.transform;
    playerPos = player.transform.position;
    if(Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("CameraHorizontal") < 0){
      transform.RotateAround(player.transform.position, Vector3.up, -rotateSpeed * Time.deltaTime);
    }else if(Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("CameraHorizontal") > 0){
      transform.RotateAround(player.transform.position, Vector3.up, +rotateSpeed * Time.deltaTime);
    }else if(Input.GetButtonDown("ChangeFront")){
      // ここ 角度の正規化してないから、もしかしたら変な挙動するかも
      transform.RotateAround(player.transform.position, Vector3.up, player.transform.eulerAngles.y - cameraTrans.eulerAngles.y);
    }/*else if(Input.GetKey(KeyCode.UpArrow)){
      transform.RotateAround(player.transform.position, Vector3.right, -rotateSpeed * Time.deltaTime);
    }else if(Input.GetKey(KeyCode.DownArrow)){
      transform.RotateAround(player.transform.position, Vector3.right, +rotateSpeed * Time.deltaTime);
    }*/ // 上下のカメラ回転はめんどそう…
  }
}
