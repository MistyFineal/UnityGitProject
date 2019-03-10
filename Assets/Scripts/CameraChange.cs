using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraChange : MonoBehaviour
{
  [SerializeField] private GameObject mainCamera;
  [SerializeField] private GameObject wireCamera;
  [SerializeField] private GameObject player;
  [SerializeField] private GameObject reticleImage;

  void Start(){
    reticleImage.SetActive(false);
  }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) || Input.GetButtonDown("WireMode")){
          ChangeActiveCamera();
          if(wireCamera.activeSelf){
            player.GetComponent<PlayerOperation>().SetWireMode(true);
            reticleImage.SetActive(true);
          }else{
            player.GetComponent<PlayerOperation>().SetWireMode(false);
            reticleImage.SetActive(false);
          }
        }
    }

    public void ChangeActiveCamera(){
      mainCamera.SetActive(!mainCamera.activeSelf);
      wireCamera.SetActive(!wireCamera.activeSelf);
    }
}
