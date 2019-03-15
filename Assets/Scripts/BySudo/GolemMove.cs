using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMove : MonoBehaviour
{
    public float localGravity;
    public float speed;

    //敵の状態
    public enum GolemState {
        Loitering,      //徘徊モード
        Chase,            //追跡モード
        Skill1              //通常攻撃モード
    };


    private int movePointer;
    private int flameCounter;
    private float distanceForMovePointer;       //MovePointerまでの距離
    private float searchLength;                            //敵の索敵範囲
    private bool isOutward;     //往路か復路か(Trueで往路)
    private GolemState state;
    private Animator anim;
    private Rigidbody rb;
    private GameObject movePoints;
    private Transform playerPosition;
    

    // Use this for initialization
    void Start()
    {
        //speed = 0.1f;
        flameCounter = 0;
        searchLength = 20.0f;
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = true;
        anim = this.GetComponent<Animator>();
        anim.SetBool("Walking", true);
        movePointer = 1;    //1で初期化
        movePoints = GameObject.Find("MovePoints");
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        isOutward = true;
        state = GolemState.Loitering;
        //Debug.Log("GolemState: " + this.state);
    }

    // Update is called once per frame
    void Update()
    {

        switch(this.state)
        {
            case GolemState.Loitering:
                LoiteringMove();
                break;

            case GolemState.Chase:
                ChaseMove();
                break;

            case GolemState.Skill1:
                Skill1Move();
                break;

            default:
                break;
        }

    }

    public void SetGolemMovePointer(int p)
    {
        this.movePointer = p;
        //Debug.Log("MovePointer: " + movePointer);
    }
    public int GetGolemMovePointer()
    {
        return this.movePointer;
    }

    public void SetGolemIsOutward(bool iot)
    {
        this.isOutward = iot;
    }
    public bool GetGolemIsOutward()
    {
        return this.isOutward;
    }

    public void SetGolemState(string mode)
    {
        switch (mode)
        {
            case "Loitering":
                this.state = GolemState.Loitering;
                break;

            case "Chase":
                this.state = GolemState.Chase;
                break;

            case "Skill1":
                this.state = GolemState.Skill1;
                break;

            default:
                break;

        }
        //Debug.Log("GolemState: " + this.state);
    }

    public string GetGolemState()
    {
        switch (this.state)
        {
            case GolemState.Loitering:
                return "Loitering";

            case GolemState.Chase:
                return "Chase";

            case GolemState.Skill1:
                return "Skill1";

            default:
                return "NotFound";
        }
    } 


    public Animator GetAnimator()
    {
        return this.anim;
    }



    //状態別の挙動関数
    private void LoiteringMove()    //徘徊時の挙動
    {
        Transform target = movePoints.transform.Find(movePointer.ToString());
        Vector3 targetPositon = target.position;

        targetPositon = new Vector3(target.position.x, transform.position.y, target.position.z);
        Quaternion targetRotation = Quaternion.LookRotation(targetPositon - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2);

        this.transform.position += this.transform.forward.normalized * speed;
    }

    private void ChaseMove()    //追跡時の挙動
    {
        distanceForMovePointer = Vector3.Distance(this.transform.position, movePoints.transform.Find(movePointer.ToString()).transform.position);
        if (distanceForMovePointer > searchLength)
        {
            SetGolemState("Loitering");
        } else
        {
            Transform target = playerPosition.transform;
            Vector3 targetPositon = target.position;

            targetPositon = new Vector3(target.position.x, transform.position.y, target.position.z);
            Quaternion targetRotation = Quaternion.LookRotation(targetPositon - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2);

            this.transform.position += this.transform.forward.normalized * speed;
        }
    }

    private void Skill1Move()   //通常攻撃の挙動
    {
        flameCounter++;
        if (flameCounter > 120)
        {
            flameCounter = 0;
            SetGolemState("Chase");
        }
    }

    //他のCollisionと衝突したときの処理
    private void OnCollisionEnter(Collision collision)
    {

    }

    //他のCollisionと離れたときの処理
    private void OnCollisionExit(Collision collision)
    {

    }

    //他のコライダーとのトリガーが作動したときの処理
    private void OnTriggerEnter(Collider other)
    {

    }

}