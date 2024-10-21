using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUIManager : MonoBehaviour
{
    [Header("Inventory")]
    public static PlayerInventoryUIManager Instance;
    public List<GameObject> icons = new();
    public List<Image> borders = new();

    [Header("Interact")]
    public TextMeshProUGUI interactText;

    [Header("Keys")]
    public int keyCount = 0;
    public TextMeshProUGUI keyText;
    public int keysRequired = 5;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        interactText.enabled = false;
    }

    private void Update()
    {
        UpdateKeyUi();
    }

    public void UpdateKeyUi()
    {
        keyText.text = keyCount + " / " + keysRequired;
    }
    public void LinkItemIcon(Item item, int index)
    {
        icons[index].gameObject.SetActive(true);

        if (item.icon != null)
        {
            icons[index].GetComponent<Image>().sprite = item.icon;
        }
    }

    public void UnlinkItemIcon(int index)
    {
        icons[index].gameObject.SetActive(false);
        icons[index].GetComponent<Image>().sprite = null;
    }

    public void UpdateSelectBorder(int index)
    {
        for (int i = 0; i < icons.Count; i++)
        {
            if (i != index)
            {
                borders[i].color = Color.black;
            }
            else
            {
                borders[i].color = Color.white;
            }
        }
    }

    public void SetInteractText(bool active, string text = "")
    {
        if (active)
        {
            if (WorldGameObjectStorage.Instance.player.playerInteractableManager.itemCount >= 5)
            {
                interactText.color = Color.red;
            }
            else
            {
                interactText.color = Color.white;
            }
            interactText.enabled = true;
            interactText.text = text + "\n E";
        }
        else
        {
            interactText.enabled = false;
        }
    }


}
