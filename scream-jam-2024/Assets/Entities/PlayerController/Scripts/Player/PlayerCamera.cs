using Cinemachine;
using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCamera : MonoBehaviour
{
    public PlayerManager playerManager;
    public Transform player;
    public Transform orientation;
    public Vector3 offset;
    public Transform cam;

    public CinemachineVirtualCamera currentVirtualCamera;
    public CinemachineBasicMultiChannelPerlin channelPerlin;


    [Header("Movement FOV")]
    public float walkingFOV;
    public float sprintFOV = 95f;
    public float camLerpSpeed = 3f;

    [Header("HeadBobbing")]
    public float idleAMP = 1f;
    public float idleFREQ = 1f;

    [Header("Camera Modes")]
    public GameObject idleCam;
    public GameObject walkCam;
    public GameObject runCam;
    public GameObject[] cinemachineVirtualCameras;
    public int currentVirtualCameraIndex = 0;

    private bool[] isLerping;

    private void Awake()
    {
        isLerping = new bool[3];
        cinemachineVirtualCameras = new GameObject[] { idleCam, walkCam, runCam };
    }

    private void Start()
    {
        playerManager = player.gameObject.GetComponent<PlayerManager>();
        currentVirtualCamera = idleCam.GetComponent<CinemachineVirtualCamera>();
        walkingFOV = currentVirtualCamera.m_Lens.FieldOfView;
    }

    private void Update()
    {
        transform.position = player.position + offset;
        orientation.localRotation = Quaternion.Euler(0f, cam.localEulerAngles.y, 0f);
    }

    private void LateUpdate()
    {
        HandleSprintingFOV();
        HandleHeadBobbing();
    }
    private void HandleSprintingFOV()
    {
        float dampVel = 0;
        if (playerManager.playerLocomotionManager.isSprinting)
        {
            float lerpFOV = Mathf.SmoothDamp(currentVirtualCamera.m_Lens.FieldOfView, sprintFOV, ref dampVel, camLerpSpeed);
            currentVirtualCamera.m_Lens.FieldOfView = lerpFOV;
        }
        else
        {
            float lerpFOV = Mathf.SmoothDamp(currentVirtualCamera.m_Lens.FieldOfView, walkingFOV, ref dampVel, camLerpSpeed);
            currentVirtualCamera.m_Lens.FieldOfView = lerpFOV;
        }
    }

    private void HandleHeadBobbing()
    {
        if (playerManager.playerLocomotionManager.isSprinting)
        {
            SwitchCamera(2);
        }
        else if (playerManager.rb.velocity.magnitude <= 0.2f)
        {
            SwitchCamera(0);
        }
        else
        {
            SwitchCamera(1);
        }
    }

    private void SwitchCamera(int camIndex)
    {
        for (int i = 0; i < cinemachineVirtualCameras.Length; i++)
        {
            if (camIndex != i)
            {
                if (!isLerping[i])
                {
                    isLerping[i] = true;
                    StartCoroutine(LerpBackToOriginCoroutine(cinemachineVirtualCameras[i], i));
                    cinemachineVirtualCameras[i].SetActive(false);
                }
            }
        }

        cinemachineVirtualCameras[camIndex].SetActive(true);
        currentVirtualCamera = cinemachineVirtualCameras[camIndex].GetComponent<CinemachineVirtualCamera>();
        currentVirtualCameraIndex = camIndex;
    }

   
    public IEnumerator LerpBackToOriginCoroutine(GameObject cam, int camIndex)
    {
        Vector3 startPosition = cam.transform.localPosition;
        Vector3 targetPosition = Vector3.zero;
        float timeElapsed = 0f;
        float lerpDuration = 1f;

        while (timeElapsed < lerpDuration)
        {
            cam.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;

            // Wait for next frame
            yield return null;
        }

        cam.transform.localPosition = targetPosition;
        isLerping[camIndex] = false;
    }


}
