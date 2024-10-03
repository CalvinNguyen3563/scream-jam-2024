using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    public Transform orientation;
    public Vector3 offset;

    public Transform cam;



    private void Update()
    {
        transform.position = player.position + offset;
        orientation.localRotation = Quaternion.Euler(0f, cam.localEulerAngles.y, 0f);
    }
}
