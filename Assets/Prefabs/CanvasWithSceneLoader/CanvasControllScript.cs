using UnityEngine;
 using System.Collections;
  
 public class CanvasControllScript : MonoBehaviour {
  
     public GameObject menu; // Assign in inspector
     private bool isShowing=true;
 
      void Update() {
         if (Input.GetKeyDown("escape")) {
             Debug.Log("trying to hide / unhide canvas");
             isShowing = !isShowing;
             menu.SetActive(isShowing);
         }
     }
 }
