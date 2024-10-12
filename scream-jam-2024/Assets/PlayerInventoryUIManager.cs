using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUIManager : MonoBehaviour
{
    public static PlayerInventoryUIManager Instance;
    public List<GameObject> icons = new();
    public List<Image> borders = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
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


}
