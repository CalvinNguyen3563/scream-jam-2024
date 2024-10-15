using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    public PlayerManager player;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }

    public void Update()
    {
        
    }

    private void SearchForInteractables()
    {

    }
}
