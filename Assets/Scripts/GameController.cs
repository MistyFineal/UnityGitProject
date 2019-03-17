using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
  public static readonly int totalCollectionItems = 100;
  private static int collectionCounter = 0;
  [SerializeField] private Text collectionCounterText;

  void Start(){
    SetCollectionText(collectionCounter);
  }

  public int GetNextCounter(){
    return ++collectionCounter;
  }

  public void SetCollectionText(int num){
    // 総数の100はGameControllerから取得したり...
    collectionCounterText.text = string.Format("ちくわ：{0,3} / 100", num);
  }
}
