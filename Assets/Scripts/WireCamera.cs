using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireCamera : MonoBehaviour
{
  private Transform verticalRotation, horizontalRotation;
  private float maxAngle = 60f, minAngle = -60f; // 45とかにすると挙動おかしいから プログラム間違ってるぽい
  private float rotateSpeed = 60.0f;
  private PlayerOperation playerOpeScript;

    void Start()
    {
        verticalRotation = transform.parent;
        horizontalRotation = GetComponent<Transform>();
        playerOpeScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerOperation>();
    }

    void LateUpdate() // ここ うまく書けなくて 泣いた
    {
      if(!playerOpeScript.IsWireAction()){
        Vector3 xRotation = verticalRotation.transform.eulerAngles;
        Vector3 yRotation = horizontalRotation.transform.eulerAngles; // なんか この値使えないんだけど…
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Vertical") > 0){
          horizontalRotation.transform.eulerAngles -= new Vector3(rotateSpeed * Time.deltaTime,0,0);
          if(horizontalRotation.transform.eulerAngles.x <= 300f)
            horizontalRotation.transform.eulerAngles = new Vector3(AdjustAngle(), horizontalRotation.transform.eulerAngles.y, horizontalRotation.transform.eulerAngles.z);
        }else if(Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("Vertical") < 0){
          horizontalRotation.transform.eulerAngles += new Vector3(rotateSpeed * Time.deltaTime,0,0);
          if(horizontalRotation.transform.eulerAngles.x >= 60f)
            horizontalRotation.transform.eulerAngles = new Vector3(AdjustAngle(), horizontalRotation.transform.eulerAngles.y, horizontalRotation.transform.eulerAngles.z);
        }else if(Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Horizontal") > 0){
          verticalRotation.transform.eulerAngles += new Vector3(0,rotateSpeed * Time.deltaTime,0);
        }else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") < 0){
          verticalRotation.transform.eulerAngles -= new Vector3(0,rotateSpeed * Time.deltaTime,0);
        }
      }
    }

    float AdjustAngle(){
      float rotateX = horizontalRotation.transform.eulerAngles.x > 180 ?
                      horizontalRotation.transform.eulerAngles.x -360 : horizontalRotation.transform.eulerAngles.x;
      float angleX = Mathf.Clamp(rotateX, minAngle, maxAngle);
      return angleX < 0 ? angleX + 360 : angleX;
    }
}
