using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOperation : MonoBehaviour
{
  private CharacterController characterController;
  private Animator animator;

  private Vector3 velocity;
  [SerializeField] private float walkSpeed = 3.2f;
  [SerializeField] private float runSpeed = 6.5f;
  private float rotateSpeed = 10.0f;
  private float moveSpeed;
  private float inputHorizontal;
  private float inputVertical;
  [SerializeField] private GameObject playerCamera;
  // jump
  private const float JUMP_POWER = 6.0f;
  private const float JUMP_POWER_RUNNING = 7.5f;
  private float jumpPower;
  // wire
  private bool isWireMode = false;
  private bool isWireAction = false;
  private PlayerWireAction wireScript;
  private CameraChange cameraChangeScript;
  private Vector3 moveDirection;
  private Vector3 movePoint;

  void Start()
  {
    characterController = GetComponent<CharacterController>();
    animator = GetComponent<Animator>();
    moveSpeed = walkSpeed;
    jumpPower = JUMP_POWER;
    wireScript = GetComponent<PlayerWireAction>();
    cameraChangeScript = GameObject.Find("CameraChanger").GetComponent<CameraChange>();
  }

  void Update()
  {
    inputHorizontal = Input.GetAxisRaw("Horizontal"); // W A S D のボタン配置設定 気をつける
    inputVertical = Input.GetAxisRaw("Vertical");
    //Debug.Log("isWire:"+isWireMode+"  isGrounded:"+characterController.isGrounded);

    /*** 地面かそれ以外で分けるつもりのところ ***/
    if(characterController.isGrounded){
      if(!isWireMode){
        PrepareMove();
      }else if(!isWireAction){ // WireMode中
        velocity = Vector3.zero;
        SetIdleState(animator);
        moveDirection = wireScript.SelectMovePoint(out movePoint);
        //Debug.Log("POINT "+movePoint);
        if(moveDirection != Vector3.zero){
          //Debug.Log(moveDirection);
          isWireAction = true;
        }
      }
      }/*else{ 落下中のアニメーション
        animator.SetBool("isAir", true);
        }*/

        /*** 実際の移動に関するコード ***/
        if(!isWireAction){ // default move
          velocity.y += Physics.gravity.y * Time.deltaTime;
          characterController.Move(
          new Vector3(velocity.x * moveSpeed * Time.deltaTime,
          velocity.y * Time.deltaTime,
          velocity.z * moveSpeed * Time.deltaTime));
        }else{ // wire move
          WireAction();
        }
      }

      void FixedUpdate(){

      }

      void PrepareMove(){
        //velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        velocity = moveForward;

        /*** PlayerRotateBaseCameraスクリプトを有効にするときは以下のif文はコメントアウト  byくそざこひなち ***/
        /*if(moveForward != Vector3.zero){
          transform.rotation = Quaternion.LookRotation(moveForward);
        }*/
        if (Mathf.Abs(inputHorizontal) + Mathf.Abs(inputVertical) > 0.01F)
        {
          //カメラからみたプレイヤーの方向ベクトル
          Vector3 cameraToPlayer = transform.position - playerCamera.transform.position;
          // π/2 - atan2(x,y) == atan2(y,x)
          float inputAngle = Mathf.Atan2(inputHorizontal, inputVertical) * Mathf.Rad2Deg;
          float cameraAngle = Mathf.Atan2(cameraToPlayer.x, cameraToPlayer.z) * Mathf.Rad2Deg;
          Quaternion targetRotation = Quaternion.Euler(0, inputAngle + cameraAngle, 0);
          //deltaTimeを用いることで常に一定の速度になる
          transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }


        /*** PlayerのanimationStateを決める ***/
        if (velocity.magnitude > 0.01f){
          if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetButton("Run")){
            moveSpeed = runSpeed;
            jumpPower = JUMP_POWER_RUNNING;
            animator.SetBool("isRunning", true);
          }else{
            moveSpeed = walkSpeed;
            jumpPower = JUMP_POWER;
            animator.SetBool("isRunning", false);
            animator.SetFloat("speed", velocity.magnitude);
          }
          //stransform.LookAt(transform.position + velocity); jumpモーション時に x:-90度を向いちゃう原因
        }else{
          animator.SetBool("isRunning", false);
          SetIdleState(animator);
        }

        /*** Jumpがあったら、Stateに応じたJumpをする ***/
        velocity.y = 0f;
        if(Input.GetKeyDown("space") || Input.GetButtonDown("Jump")){
          animator.SetBool("isJumping", true);
          velocity.y = jumpPower;
        }else{
          animator.SetBool("isJumping", false);
          //velocity.y -= Physics.gravity.y * Time.deltaTime;
        }
      }

      private void WireAction(){
        //Debug.Log("WIRE ACTION");
        this.transform.position = Vector3.MoveTowards(this.transform.position, movePoint, Time.deltaTime * wireScript.moveSpeed);
        if(this.transform.position == movePoint){ // 到着判定が危なそう & 移動中 wireCameraの操作できなくしたい
          wireScript.DestroyWire();
          isWireAction = false;
          SetWireMode(false);
          cameraChangeScript.ChangeActiveCamera();
        }
      }

      public void SetWireMode(bool _isWireMode){
        isWireMode = _isWireMode;
      }

      private void SetIdleState(Animator _animator){
        _animator.SetFloat("speed", 0f);
      }

      public bool IsWireAction(){
        return isWireAction;
      }

      public void ReceivedDamage(){
        // ダメージのアニメーションに遷移
        // のけぞり等で ダメージを受けた後の座標を考慮
        // 他の処理はPlayerReaction.csに任せる
        Debug.Log("<color=red>DAMAGE!!</color>");
      }

      public void TellTheDeath(){
        Debug.Log("やられた...");
      }
    }
