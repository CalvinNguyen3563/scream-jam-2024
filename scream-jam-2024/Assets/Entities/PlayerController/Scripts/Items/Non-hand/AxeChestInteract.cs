using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxeChestInteract : MonoBehaviour, Interactable
{
    public Item item;
    public Interactable.itemState state = Interactable.itemState.interactable;
    public Animation animation;
    public Collider triggerCollider;

    public Outline outline;

    public GameObject itemInside;

    public bool opened = false;

    private void Start()
    {
        itemInside.SetActive(false);
        outline.interactMsg = "You require " + PlayerInventoryUIManager.Instance.keyCount + " / " + PlayerInventoryUIManager.Instance.keysRequired + " keys to open.";
    }

    private void Update()
    {
        if (PlayerInventoryUIManager.Instance.keyCount >= PlayerInventoryUIManager.Instance.keysRequired)
        {
            outline.interactMsg = "Open chest"; 
        }
        else
        {
            outline.interactMsg = "You require " + PlayerInventoryUIManager.Instance.keyCount + " / " + PlayerInventoryUIManager.Instance.keysRequired + " keys to open.";
        }
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

        if (PlayerInventoryUIManager.Instance.keyCount >= PlayerInventoryUIManager.Instance.keysRequired)
        {
            
            animation.Play();
            itemInside.SetActive(true);
            StartCoroutine(WaitForAnimation(2f));
            opened = true;
        }
        
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
