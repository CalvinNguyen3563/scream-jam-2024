using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKitUse : MonoBehaviour, UseItem
{
    public float invisibleTime = 0.3f;
    public GameObject model;
    public float healValue;
    private bool used = false;
    public Animator animator;
    private void Awake()
    {
        StartCoroutine(DisableObject(invisibleTime));
    }
    public void UseItem()
    {
        if (!used)
        {
            animator.SetBool("use", true);
        }
    }

    private void UseEffect()
    {
        PlayerStatsManager.Instance.IncreaseHealth(healValue);
        WorldGameObjectStorage.Instance.player.playerInteractableManager.DestroyEquippedItem();
    }

    public IEnumerator DisableObject(float time)
    {
        model.SetActive(false);
        yield return new WaitForSeconds(time);
        model.SetActive(true);
    }
}
