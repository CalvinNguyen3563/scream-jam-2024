using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkLeftIK : MonoBehaviour
{
    public GameObject leftHandIKTarget;
    public string itemName;

    private void Awake()
    {
        gameObject.name = itemName;
        WorldGameObjectStorage.Instance.player.playerAnimatorManager.leftIKTarget = leftHandIKTarget;
    }
    private void OnEnable()
    {
        
    }
}
