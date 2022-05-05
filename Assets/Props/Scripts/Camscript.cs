
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camscript : MonoBehaviour
{
    public GameObject[] Cameras;
    public GameObject[] Figures;
    public bool Nextcam;

    [SerializeField]
    private GameMap _gameMapRef;

    public Vector3 CamOffset;
    public Vector3 CamRotate;

    // Start is called before the first frame update
    void Start() {
        Nextcam = false;
        foreach (var cam in Cameras) {
            cam.SetActive(false);
        }
        //Only enable this camera
        Cameras[0].SetActive(true);

        CamOffset.x = 0;
        CamOffset.y = 30;
        CamOffset.z = -20;

        CamRotate.x = 40;
        CamRotate.y = 0;
        CamRotate.z = 0;

        Cameras[0].transform.eulerAngles = CamRotate;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            foreach (var cam in Cameras) {
                if (cam.activeSelf == false) {
                    if (Nextcam == true) {
                        cam.SetActive(true);
                        Nextcam = false;
                    }
                    continue;
                }
                else {
                    cam.SetActive(false);
                    Nextcam = true;
                }
            }

            if (Nextcam == true) {
                Cameras[0].SetActive(true);
                Nextcam = false;
            }
        }

        if (Cameras[0].activeSelf == true) {
            if ((_gameMapRef.isPlaying >= 0) && (_gameMapRef.isPlaying < Figures.Length)) {
                Cameras[0].transform.position = Figures[_gameMapRef.isPlaying].transform.position + CamOffset;
                Cameras[0].transform.eulerAngles = CamRotate;
            }
            else {
                Cameras[0].transform.position = new Vector3(-75, 15, 45);
                Cameras[0].transform.eulerAngles = new Vector3(15, -90, 0);
            }
        }
    }
}
