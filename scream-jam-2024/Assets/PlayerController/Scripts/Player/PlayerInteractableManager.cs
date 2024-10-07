using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

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

    [Header("Inventory")]
    public int itemSlots = 5;
    public Item[] items;
    public int itemCount = 0;
    public Item currentEquippedItem;



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

    public void HandleItemPickup()
    {
        if (itemCount >= 5)
            return;

        if (Input.GetKeyDown(KeyCode.E) && previousOutline != null)
        {
            Interactable interactable = previousOutline.GetComponent<Interactable>();
            items[itemCount] = interactable.GetItemInfo();
            interactable.DestroyItem();
            EquipItem(items[itemCount]);
            player.playerAnimatorManager.LinkItemAnimationProfile(items[itemCount]);
            ++itemCount;
        }
    }

    public void EquipItem(Item item)
    {
        if (item.itemPrefab != null)
        {
            GameObject newItem = Instantiate(item.itemPrefab, rightHand.transform, false);
            newItem.transform.localRotation = Quaternion.Euler(item.localRotation);
            newItem.transform.localPosition = item.localPosition;
            newItem.transform.localScale = item.localScale;
        }
        else
        {
            player.playerAnimatorManager.SwitchToUnarmedState();
            currentEquippedItem = null;
        }
    }

    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(cam.transform.position, cam.transform.forward * range);
    }
}
