using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemData item;
    public int amount;

    [Header("UI References")]
    public Image itemIcon;
    public Text amountText;
    public GameObject emptySlotImage;

    void Start()
    {
        UpdateSlotUI();
    }

    public void AddAmount(int value)
    {
        amount += value;
        UpdateSlotUI();
    }

    public void RemoveAmount(int value)
    {
        amount -= value;

        if(amount <= 0)
        {
            ClearSlot();
        }
        else
        {
            UpdateSlotUI();
        }
    }

    public void ClearSlot()
    {
        item = null;
        amount = 0;
        UpdateSlotUI();
    }

    public void SetItem(ItemData newItem, int newAmount)
    {
        item = newItem;
        amount = newAmount;
        UpdateSlotUI();
    }
    void UpdateSlotUI()
    {
        if(item != null)
        {
            itemIcon.sprite = item.itemIcon;
            itemIcon.enabled = true;

            amountText.text = amount > 1 ? amount.ToString() : "";
            if(emptySlotImage != null)
            {
                emptySlotImage.SetActive(false);
            }
        }
        else
        {
            itemIcon.enabled = false;
            amountText.text = "";
            if(emptySlotImage != null)
            {
                emptySlotImage.SetActive(true);
            }
        }
    }
}
