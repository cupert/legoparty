
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camscript : MonoBehaviour
{
    public Camera[] Cameras;
    public GameObject[] Figures;
    public bool Nextcam;

    public Vector3 CamOffset;
    public Vector3 CamRotate;

    public int Figurenextindex = 0;

    // Start is called before the first frame update
    void Start() {
        Nextcam = false;
        foreach (var cam in Cameras) {
            cam.enabled = false;
        }
        //Only enable this camera
        Cameras[0].enabled = true;

        CamOffset.x = -20;
        CamOffset.y = -40;
        CamOffset.z = 10;

        CamRotate.x = 40;
        CamRotate.y = 0;
        CamRotate.z = 0;

        Cameras[0].transform.Rotate(CamRotate);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            foreach (var cam in Cameras) {
                if (cam.enabled == false) {
                    if (Nextcam == true) {
                        cam.enabled = true;
                        Nextcam = false;
                    }
                    continue;
                }
                else {
                    cam.enabled = false;
                    Nextcam = true;
                }
            }

            if (Nextcam == true) {
                Cameras[0].enabled = true;
                Nextcam = false;
            }
        }

        if (Cameras[0].enabled == true) {
            Cameras[0].transform.position = Figures[Figurenextindex].transform.position - CamOffset;
        }

        if (Input.GetKeyDown(KeyCode.P) && (Cameras[0].enabled == true)) {
            Figurenextindex++;
            if (Figurenextindex >= Figures.Length) {
                Figurenextindex = 0;
            }
        }
    }
}
