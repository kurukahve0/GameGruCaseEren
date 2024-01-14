using System;
using Cinemachine;
using UnityEngine;


namespace Case_1
{
    public class CameraOrthoController : MonoBehaviour
    {
        #region Variable

        [SerializeField] private CinemachineVirtualCamera vCam;

        #endregion

        #region MonoBehaviour

        void OnEnable()
        {
            EventManager.OnChangeSizeValue += SetCamera;
        }

        void OnDisable()
        {
            EventManager.OnChangeSizeValue -= SetCamera;
        }

        #endregion
        
        #region Camera

        void SetCamera(int size)
        {
            size -= 3; // 3 referens aldığım değer
            vCam.m_Lens.OrthographicSize = 1.4f + size * .45f; // Karenin boyutlarına göre ayarlama
     
        }
        
 
        #endregion
    }
}
