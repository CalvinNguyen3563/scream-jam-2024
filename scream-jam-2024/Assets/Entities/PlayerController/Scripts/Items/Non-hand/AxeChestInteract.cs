using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeChestInteract : MonoBehaviour, Interactable
{
    public Item item;
    public Interactable.itemState state = Interactable.itemState.interactable;
    public Animation animation;
    public Collider triggerCollider;

    public GameObject itemInside;

    public bool opened = false;

    private void Awake()
    {
        itemInside.SetActive(false);
    }

    public Item GetItemInfo()
    {
        return null;
    }

    public void PerformSpecialAction()
    {
        if (opened)
        {
            return; 
        }
        animation.Play();
        itemInside.SetActive(true);
        StartCoroutine(WaitForAnimation(2f));
        opened = true;
    }

    public IEnumerator WaitForAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        triggerCollider.enabled = false;
    }

    public Interactable.itemState GetItemState()
    {
        return state;
    }
    public void DestroyItem()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
