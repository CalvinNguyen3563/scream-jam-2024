using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteractableManager : MonoBehaviour
{
    public PlayerManager player;
    public GameObject cam;
    public GameObject rightHand;

    [Header("Pick Up Settings")]
    public LayerMask whatIsInteractable;
    [Range(0, 10f)]
    public float range = 2f;
    private Outline previousOutline;
    private List<Outline> outlines = new();
    public float itemSwitchCooldown = 0.4f;
    public bool canSwitch = true;

    [Header("Inventory")]
    public int itemSlots = 5;
    public Item[] items;
    public int itemCount = 0;
    public Item currentEquippedItem = null;
    public GameObject rightHandObject = null;



    private void Awake()
    {
        items = new Item[itemSlots];
    }

    private void Start()
    {
        player = WorldGameObjectStorage.Instance.player;
    }

    private void Update()
    {
        DetectInteractables();
        HandleItemPickup();
        HandleInventoryInput();
    }

    private void DetectInteractables()
    {
        RaycastHit[] hits = Physics.RaycastAll(cam.transform.position, cam.transform.forward, range, whatIsInteractable);

        if (hits.Length > 0)
        {
            GameObject closestItem = hits[0].collider.gameObject;
            RaycastHit closestHit = hits[0];
            Vector3 rayHitPosition = hits[0].point;
            float closestDistance = Vector3.Distance(hits[0].collider.transform.position, rayHitPosition);

            foreach (RaycastHit hit in hits)
            {
                // Compare the hit distances from the ray origin
                if (Vector3.Distance(hit.collider.transform.position, rayHitPosition) < closestDistance)
                {
                    closestItem = hit.collider.gameObject;
                    closestDistance = hit.distance; // Update the closest distance
                }
            }

            if (closestItem.TryGetComponent<Outline>(out Outline outline))
            {
                if (outline != previousOutline)
                {
                    if (previousOutline != null)
                    {
                        previousOutline.enabled = false;
                    }
                    outline.enabled = true;
                    previousOutline = outline;
                }
            }
        }
        else
        {
            if (previousOutline != null)
            {
                previousOutline.enabled = false;
                previousOutline = null;
            }
        }
    }

    public void HandleInventoryInput()
    {
        for (int i = 1; i < itemSlots + 1; i++)
        {



            if (Input.GetKeyDown(i.ToString()))
            {
                if (canSwitch)
                {
                    canSwitch = false;
                    StartCoroutine(ResetSwitchCooldown());
                }
                else
                {
                    return;
                }
                Item referencedItem = items[i - 1];
                if (((currentEquippedItem != null && referencedItem != null) && currentEquippedItem.name == referencedItem.name) || referencedItem == null)
                {
                    player.playerAnimatorManager.leftIKTarget = null;
                    Destroy(rightHandObject);
                    StartCoroutine(player.playerAnimatorManager.LinkItemAnimationProfile(null)) ;    
                    rightHandObject = null;
                    currentEquippedItem = null;
                    return;
                }
                //Destroy(rightHandObject);
                EquipItem(referencedItem);
               // player.playerAnimatorManager.LinkItemAnimationProfile(referencedItem);
                
                currentEquippedItem = referencedItem;
            }
        }
    }

    public IEnumerator ResetSwitchCooldown()
    {
        yield return new WaitForSeconds(itemSwitchCooldown);
        canSwitch = true;
    }

    public void HandleItemPickup()
    {
        if (itemCount >= itemSlots)
            return;

        if (Input.GetKeyDown(KeyCode.E) && previousOutline != null)
        {
            Interactable interactable = previousOutline.GetComponent<Interactable>();
            items[itemCount] = interactable.GetItemInfo();
            interactable.DestroyItem();
            ++itemCount;
        }
    }

    public void EquipItem(Item item)
    {
        if (item != null)
        {
            if (item.itemPrefab != null)
            {
                GameObject previous = rightHandObject;
                player.playerAnimatorManager.leftIKTarget = null;
                rightHandObject = Instantiate(item.itemPrefab, rightHand.transform, false);
                rightHandObject.transform.localRotation = Quaternion.Euler(item.localRotation);
                rightHandObject.transform.localPosition = item.localPosition;
                rightHandObject.transform.localScale = item.localScale;
                StartCoroutine(player.playerAnimatorManager.LinkItemAnimationProfile(item));
                if (previous != null)
                {
                    Destroy(previous);
                }
                
            }
        }
        else
        {
            player.playerAnimatorManager.LinkItemAnimationProfile(null);
        }
 
    }

    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(cam.transform.position, cam.transform.forward * range);
    }
}
