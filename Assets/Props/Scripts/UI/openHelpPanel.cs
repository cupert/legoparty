using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openHelpPanel : MonoBehaviour
{
    public GameObject panel;
    //public GameObject closeButton;
    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    public void OpenAndClose()
    {
        if(!active) {
            // open help panel
            panel.transform.gameObject.SetActive(true);
            //closeButton.transform.gameObject.SetActive(true);
            active = true;
        } else {
            // close help panel
            panel.transform.gameObject.SetActive(false);
            //closeButton.transform.gameObject.SetActive(false);
            active = false;
        }
    }
}
