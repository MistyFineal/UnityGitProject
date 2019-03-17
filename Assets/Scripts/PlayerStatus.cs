using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
  private static int playerHP = 5;
  private PlayerOperation operationScript;

  void Start(){
    operationScript = GetComponent<PlayerOperation>();
  }

  public void IncrementPlayerHP(){
    playerHP++;
  }

  public void DecrementPlayerHP(){
    playerHP--;
    if(playerHP < 0){
      operationScript.TellTheDeath();
    }
  }

  public int GetPlayerHP(){
    return playerHP;
  }
}
