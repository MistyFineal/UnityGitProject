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
  private float[] verticalRotation = {0f, 11.45f, 22.9f, 34.35f}; // (x) default : 22.9
  private int verticalRotationIndex = 2; // verticalRotation[2] = 22.9f
  private float beforeTrigger;
  private float rotateSpeed = 60f;

  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
    playerPos = player.transform.position;
    transform.position = offset;
  }

  void LateUpdate()
  {
    float verticalRotateInput = Input.GetAxis("CameraVertical");

    transform.position += player.transform.position - playerPos;
    cameraTrans = this.transform;
    playerPos = player.transform.position;
    Debug.Log(verticalRotationIndex+"/"+verticalRotation[verticalRotationIndex]+"/"+cameraTrans.eulerAngles.x);
    if(Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("CameraHorizontal") < 0){
      cameraTrans.RotateAround(playerPos, Vector3.up, -rotateSpeed * Time.deltaTime);
    }else if(Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("CameraHorizontal") > 0){
      cameraTrans.RotateAround(playerPos, Vector3.up, +rotateSpeed * Time.deltaTime);
    }else if(Input.GetButtonDown("ChangeFront")){
      // ここ 角度の正規化してないから、もしかしたら変な挙動するかも
      cameraTrans.RotateAround(playerPos, Vector3.up, player.transform.eulerAngles.y - cameraTrans.eulerAngles.y);
    }

    // 上下のカメラ回転変化（４段階）
    if(Input.GetKeyDown(KeyCode.UpArrow) || (verticalRotateInput < 0 && beforeTrigger == 0f)){
      if(verticalRotationIndex < 3){
        cameraTrans.RotateAround(playerPos, cameraTrans.right, Mathf.Abs(verticalRotation[++verticalRotationIndex] - cameraTrans.eulerAngles.x));
      }
    }else if(Input.GetKeyDown(KeyCode.DownArrow) || (verticalRotateInput > 0 && beforeTrigger == 0f)){
      if(verticalRotationIndex > 0){
        cameraTrans.RotateAround(playerPos, cameraTrans.right, -Mathf.Abs(verticalRotation[--verticalRotationIndex] - cameraTrans.eulerAngles.x));
      }
    }
    beforeTrigger = verticalRotateInput;
  }
}
