using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Equippables")]
public class Item : ScriptableObject
{
    [Header("Item Info")]
    public string itemName;
    public GameObject itemPrefab;
    public Vector3 localPosition;
    public Vector3 localRotation;
    public Vector3 localScale = Vector3.one;

    [Header("Animation Info")]
    public GameObject dropPrefab;
    public AnimationClip use;
    public AnimatorOverrideController overrideController;


}
