using UnityEngine;

namespace RPG.Core
{
    public class CameraFacing : MonoBehaviour
    {
        [SerializeField] GameObject mainCamera = null;

        private void Start()
        {
            mainCamera = GameObject.Find("Main Camera"); ;
        }

        void LateUpdate()
        {
            transform.LookAt(transform.position - mainCamera.transform.position);
        }
    }
}