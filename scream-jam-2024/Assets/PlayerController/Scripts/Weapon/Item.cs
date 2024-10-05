using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Equippables")]
public class Item : ScriptableObject
{
    [Header("Item Info")]
    public GameObject itemPrefab;
    public Vector3 localPosition;
    public Vector3 localRotation;
    public Vector3 localScale = Vector3.one;

    [Header("Animation Info")]
    public AnimationClip equip;
    public AnimationClip idle;
    public AnimationClip run;
    public AnimationClip use;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
