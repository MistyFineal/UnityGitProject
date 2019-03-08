using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWireAction : MonoBehaviour
{
  //[SerializeField] private GameObject wireCamera;
  [SerializeField]private float rayRange = 9.0f;
  private Vector3 targetPosition; // 移動する位置
  private Vector3 velocity;
  public float moveSpeed = 8.5f; // 移動速度
  [SerializeField] private GameObject testObj;
  [SerializeField] private GameObject reticleImage;
  private Vector3 moveDirection; // Wire方向のベクトル

    public Vector3 SelectMovePoint(out Vector3 _movePoint){
      velocity = Vector3.zero;
      _movePoint = Vector3.zero;
      if(/*Input.GetButton("Fire1") || */Input.GetButtonDown("Ok") || Input.GetKeyDown(KeyCode.T)){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //int layerMask = 0;
        if(Physics.Raycast(ray, out hit, rayRange)){ // layerMaskは指定しないでおく
          reticleImage.SetActive(false);
          targetPosition = hit.point;
          _movePoint = targetPosition;
          // デバッグ用
          GameObject testObj = Instantiate(this.testObj, targetPosition, Quaternion.identity);
          Destroy(testObj, 5f);
          Debug.DrawRay(ray.origin, ray.direction * rayRange, Color.red, 3, false);
          moveDirection = targetPosition - this.transform.position;
          //Debug.Log("WIRE-" + moveDirection);
          return moveDirection;
        }
      }
      return Vector3.zero;
    }
}
