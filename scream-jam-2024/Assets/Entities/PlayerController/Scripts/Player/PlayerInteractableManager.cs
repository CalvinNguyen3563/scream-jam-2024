using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteractableManager : MonoBehaviour
{
    public PlayerManager player;
    public PlayerInventoryUIManager playerInventoryUIManager;
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
    public int currentItemIndex = -1;



    private void Awake()
    {
        items = new Item[itemSlots];
    }

    private void Start()
    {
        player = WorldGameObjectStorage.Instance.player;
        playerInventoryUIManager = PlayerInventoryUIManager.Instance;
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
                        playerInventoryUIManager.SetInteractText(false);
                        previousOutline.enabled = false;
                    }
                    outline.enabled = true;
                    playerInventoryUIManager.SetInteractText(true, outline.interactMsg);
                    previousOutline = outline;
                }
            }
        }
        else
        {
            if (previousOutline != null)
            {
                previousOutline.enabled = false;
                playerInventoryUIManager.SetInteractText(false);
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


                if (referencedItem == null)
                {
                    player.playerAnimatorManager.leftIKTarget = null;
                    Destroy(rightHandObject);
                    StartCoroutine(player.playerAnimatorManager.LinkItemAnimationProfile(null));
                    rightHandObject = null;
                    currentEquippedItem = null;
                    currentItemIndex = i - 1;
                    playerInventoryUIManager.UpdateSelectBorder(i - 1);
                    return;
                }
                else
                {
                    if (currentItemIndex != i - 1)
                    {
                        player.playerAnimatorManager.leftIKTarget = null;
                        Destroy(rightHandObject);
                        currentItemIndex = i - 1;
                        EquipItem(referencedItem);
                        currentEquippedItem = referencedItem;
                        playerInventoryUIManager.UpdateSelectBorder(i - 1);
                    }
                    else
                    {
                        player.playerAnimatorManager.leftIKTarget = null;
                        Destroy(rightHandObject);
                        StartCoroutine(player.playerAnimatorManager.LinkItemAnimationProfile(null));
                        rightHandObject = null;
                        currentEquippedItem = null;
                        currentItemIndex = -1;
                        playerInventoryUIManager.UpdateSelectBorder(-1);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                DropItem(currentEquippedItem, currentItemIndex);
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
            playerInventoryUIManager.SetInteractText(false);

            if (interactable.GetItemState() == Interactable.itemState.inHand)
            {
                int i = 0;
                bool foundValidSlot = false;
                while (i < items.Length && !foundValidSlot)
                {
                    if (items[i] == null)
                    {
                        foundValidSlot = true;
                    }
                    else
                    {
                        ++i;
                    }
                }

                items[i] = interactable.GetItemInfo();
                playerInventoryUIManager.LinkItemIcon(interactable.GetItemInfo(), i);
                interactable.DestroyItem();

                if (i == currentItemIndex)
                {
                    EquipItem(items[i]);
                    currentEquippedItem = items[i];
                }


                ++itemCount;
            }
            else if (interactable.GetItemState() == Interactable.itemState.interactable)
            {
                interactable.PerformSpecialAction();
            }

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
                
                playerInventoryUIManager.UpdateSelectBorder(currentItemIndex);
            }
        }
        else
        {
            StartCoroutine(player.playerAnimatorManager.LinkItemAnimationProfile(null));
        }
 
    }

    public void DropItem(Item item, int index)
    {
        if (item != null && item.dropPrefab != null)
        {
            GameObject dropItem = Instantiate(item.dropPrefab, WorldGameObjectStorage.Instance.player.transform.position, Quaternion.identity);
            Rigidbody itemRb = dropItem.GetComponent<Rigidbody>();
            Collider itemCollider = dropItem.GetComponent<Collider>();

            if (itemRb != null && itemCollider != null)
            {
                float time = 0.5f;
                StartCoroutine(IgnorePlayerCollider(itemCollider, time));

                Vector3 forwardDirection = WorldGameObjectStorage.Instance.mainCam.transform.forward;
                float horizontalForce = 5f;  
                float verticalForce = 5f;  
                Vector3 arcForce = forwardDirection * horizontalForce + Vector3.up * verticalForce;
                itemRb.AddForce(WorldGameObjectStorage.Instance.player.rb.velocity + arcForce, ForceMode.Impulse);

                Vector3 spin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 5f; 
                itemRb.angularVelocity = spin;
            }

            Destroy(rightHandObject);
            StartCoroutine(player.playerAnimatorManager.LinkItemAnimationProfile(null));
            items[index] = null;
            --itemCount;
            currentEquippedItem = null;
            rightHandObject = null;
            player.playerAnimatorManager.leftIKTarget = null;

            playerInventoryUIManager.UnlinkItemIcon(index);
    
        }
    }

    public void DestroyEquippedItem()
    {
        Destroy(rightHandObject);
        StartCoroutine(player.playerAnimatorManager.LinkItemAnimationProfile(null));
        items[currentItemIndex] = null;
        --itemCount;
        currentEquippedItem = null;
        rightHandObject = null;
        player.playerAnimatorManager.leftIKTarget = null;

        playerInventoryUIManager.UnlinkItemIcon(currentItemIndex);
    }

    private IEnumerator IgnorePlayerCollider(Collider collider, float time)
    {
        Physics.IgnoreCollision(collider, WorldGameObjectStorage.Instance.player.playerCollider, true);

        yield return new WaitForSeconds(time);

        if (collider != null)
        {
            Physics.IgnoreCollision(collider, WorldGameObjectStorage.Instance.player.playerCollider, false);
        }
    }

    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(cam.transform.position, cam.transform.forward * range);
    }
}
