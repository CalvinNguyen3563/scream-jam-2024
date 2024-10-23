using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI useText;
    public Coroutine fadeText;

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
            interactText.color = Color.white;  
            interactText.enabled = true;
            interactText.text = text + "\n E";
        }
        else
        {
            interactText.enabled = false;
        }
    }

    public void FadeUseTextCommand()
    {
        if (fadeText != null)
        {
            StopCoroutine(fadeText);
        }
        fadeText = StartCoroutine(FadeUseText());

    }

    public IEnumerator FadeUseText()
    {
        Color originalColor = useText.color;
        originalColor.a = 1f;
        useText.color = originalColor;

        float timeElapsed = 0f;
        float timeLimit = 1.5f;

        while (timeElapsed < timeLimit)
        {
            // Calculate the alpha based on the elapsed time
            float adjustTime = timeElapsed / timeLimit;

            // Lerp the alpha from 1 (fully opaque) to 0 (fully transparent)
            Color newColor = useText.color;
            newColor.a = Mathf.Lerp(1f, 0f, adjustTime);
            useText.color = newColor;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure alpha is set to 0 at the end
        Color finalColor = useText.color;
        finalColor.a = 0f;
    }


}
