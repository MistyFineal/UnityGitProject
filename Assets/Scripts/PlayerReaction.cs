using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReaction : MonoBehaviour
{
  [SerializeField] private Text playerHpText;
  private PlayerOperation operationScript;
  private PlayerStatus statusScript;

  void Start()
  {
    operationScript = GetComponent<PlayerOperation>();
    statusScript = GetComponent<PlayerStatus>();
  }

  void OnTriggerEnter(Collider other){
    // Enemyからダメージを受けたら
    if(other.gameObject.tag == "AttackArea"){
      operationScript.ReceivedDamage();
      statusScript.DecrementPlayerHP();
      SetPlayerHpText(statusScript.GetPlayerHP());
    }
  }

  void OnControllerColliderHit(ControllerColliderHit hit){
    // Enemyにダメージを与える
    if(hit.gameObject.tag == "DamageArea"){
      // Enemy側のScriptを呼んで ダメージを与える、とか
    }
  }

  public void SetPlayerHpText(int hp){
    ChangeHpText(hp); // 関数呼び出しとforループ どっちがあれなのかわからんめう
  }

  private void ChangeHpText (int hp){
    string hpText = "";
    switch (hp){
      case 5 :
        hpText = "■ ■ ■ ■ ■";
        break;
      case 4 :
        hpText = "■ ■ ■ ■";
        break;
      case 3 :
        hpText = "■ ■ ■";
      break;
      case 2 :
        hpText = "■ ■";
        break;
      case 1 :
        hpText = "■";
        break;
      // 死の処理は別でやる
    }
    playerHpText.text = hpText;
  }
}
