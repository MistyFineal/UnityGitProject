using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWireAction : MonoBehaviour
{
  //[SerializeField] private GameObject wireCamera;
  [SerializeField]private float rayRange = 9.0f;
  private Vector3 targetPosition; // 移動する位置
  private Vector3 velocity;
  public float moveSpeed = 8.5f; // 移動速度
  [SerializeField] private GameObject testObj;
  [SerializeField] private GameObject wirePrefab;
  private GameObject wire;
  [SerializeField] private GameObject reticleImage;
  private Vector3 moveDirection; // Wire方向のベクトル

  public Vector3 SelectMovePoint(out Vector3 _movePoint){
    velocity = Vector3.zero;
    _movePoint = Vector3.zero;
    reticleImage.GetComponent<Image>().color = Color.black;

    // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // マウス操作
    // レティクルの位置は、画面サイズの変更を考慮した時に 対応しやすいように 中央に配置
    Ray ray = Camera.main.ScreenPointToRay(new Vector3((Camera.main.pixelWidth-1)/2, (Camera.main.pixelHeight-1)/2, 0));
    RaycastHit hit;

    //int layerMask = 0;
    if(Physics.Raycast(ray, out hit, rayRange)){ // layerMaskは指定しないでおく
      reticleImage.GetComponent<Image>().color = Color.red;
      if(/*Input.GetButton("Fire1") || */Input.GetButtonDown("Ok") || Input.GetKeyDown(KeyCode.T)){
        targetPosition = hit.point;
        _movePoint = targetPosition;
        // デバッグ用
        //GameObject testObj = (GameObject)Instantiate(this.testObj, targetPosition, Quaternion.identity);
        //Destroy(testObj, 5f);
        //Debug.DrawRay(ray.origin, ray.direction * rayRange, Color.red, 3, false);
        moveDirection = targetPosition - this.transform.position;
        wire = (GameObject)Instantiate(wirePrefab, (targetPosition + transform.position) / 2, Quaternion.FromToRotation(Vector3.up, moveDirection));
        wire.transform.localScale = new Vector3(wire.transform.localScale.x, moveDirection.magnitude / 2, wire.transform.localScale.z);
        reticleImage.SetActive(false);
        return moveDirection;
      }
    }

    return Vector3.zero;
  }

  public void DestroyWire(){
    if(wire != null)
      Destroy(wire);
  }
}
