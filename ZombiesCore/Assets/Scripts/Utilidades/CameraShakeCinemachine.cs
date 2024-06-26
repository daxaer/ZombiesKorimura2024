using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

public class CameraShakeCinemachine : MonoBehaviour
{

    public float ShakeDuration = 0.3f;          // Time the Camera Shake effect will last
    public float ShakeAmplitude = 1.2f;         // Cinemachine Noise Profile Parameter
    public float ShakeFrequency = 2.0f;         // Cinemachine Noise Profile Parameter

    private float ShakeElapsedTime = 0f;

    // Cinemachine Shake
    //public CinemachineVirtualCamera VirtualCamera;
    //private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    private static CameraShakeCinemachine _instance;
    public static CameraShakeCinemachine Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }


    // Use this for initialization
    private void Start()
    {
        // Get Virtual Camera Noise Profile
        //if (VirtualCamera != null)
            //virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    private void Update()
    {
        // TODO: Replace with your trigger
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    ShakeElapsedTime = ShakeDuration;
        //}

        // If the Cinemachine componet is not set, avoid update
        //if (VirtualCamera == null || virtualCameraNoise == null) return;      No se ocupa esta condicion realmente
        // If Camera Shake effect is still playing
        ShakeCameraCinemachine(ShakeElapsedTime);
    }

    public void ShakeCameraCinemachine(float shakeDuration)
    {
        ShakeElapsedTime = shakeDuration;
        if (ShakeElapsedTime > 0)
        {
            // Set Cinemachine Camera Noise parameters
            //virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
            //virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

            // Update Shake Timer
            ShakeElapsedTime -= Time.deltaTime;
        }
        else
        {
            // If Camera Shake effect is over, reset variables
            //virtualCameraNoise.m_AmplitudeGain = 0f;
            ShakeElapsedTime = 0f;
        }
    }
}
