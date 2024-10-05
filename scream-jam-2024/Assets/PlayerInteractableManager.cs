using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractableManager : MonoBehaviour
{
    public PlayerManager player;

    [Header("Pick Up Settings")]
    [Range(0, 10f)]
    public float range = 2f;

    private void Awake()
    {
        player = WorldGameObjectStorage.Instance.player;
    }
}
