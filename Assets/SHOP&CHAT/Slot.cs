using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [HideInInspector]
    public ItemProperty item;

    public UnityEngine.UI.Image image;
    public UnityEngine.UI.Button sellButton;

    private void Awake()
    {
        SetSellButtonInteractable(false);
    }

    void SetSellButtonInteractable(bool b)
    {
        if (sellButton != null)
        {
            sellButton.interactable = b;
        }
    }
    public void SetItem(ItemProperty item)
    {
        this.item = item;

        if (item == null)
        {
            image.enabled = false;
            SetSellButtonInteractable(false);
            gameObject.name = "Empty";
        }
        else
        {
            image.enabled = true;
            gameObject.name = item.name;
            image.sprite = item.sprite;
            SetSellButtonInteractable(true);
        }
    }

    public void OnClickSellButton()
    {
        SetItem(null);
    }
}
