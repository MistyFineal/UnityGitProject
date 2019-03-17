using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionItem : MonoBehaviour
{
  [SerializeField] private GameObject paricleObj;
  GameController gameController;

    void Start()
    {
      gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter(Collider other){
      if(other.gameObject.tag == "Player"){
        GameObject obj = (GameObject)Instantiate(paricleObj, transform.position, Quaternion.identity);
        obj.transform.parent = other.transform;
        gameController.SetCollectionText(gameController.GetNextCounter());
        Destroy(obj, 3f);
        Destroy(this.gameObject);
      }
    }
}
