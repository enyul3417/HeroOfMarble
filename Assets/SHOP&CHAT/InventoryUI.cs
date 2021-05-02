using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    public GameObject inventoryPanel;
    bool activeInventory = false;

    public System.Action<ItemProperty> OnSlotClick;

    public ItemBuffer itemBuffer;
    public Transform slotRoot;

    public Slot[] slots;
    private List<Slot> slotss;

    private void Start()
    {

        inventoryPanel.SetActive(activeInventory);
        //closeShop.onClick.AddListener(DeActiveShop);//

        
        int slotCnt = slotRoot.childCount;
        slotss = new List<Slot>();

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();

            if (i < itemBuffer.items.Count)
            {
                slot.SetItem(itemBuffer.items[i]);
            }
            else//아이템 없을 때 오는 곳
            {
                slot.GetComponent<UnityEngine.UI.Button>().interactable = false;
            }
            slotss.Add(slot);
        }


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
        //if (Input.GetMouseButtonUp(0))//
        //    RayShop();

    }

    public void OnClickSlot(Slot slot)
    {
        if (OnSlotClick != null)
        {
            OnSlotClick(slot.item);
        }
    }

    //public GameObject shop; //
    //public Button closeShop; //

    //public void RayShop()//
    //{
    //  Vector3 mousePos  = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    mousePos.z = -10;
    //    RaycastHit2D hit2D=Physics2D.Raycast(mousePos, transform.forward, 30);
    //    if(hit2D.collider != null)
    //    {
    //        if (hit2D.collider.CompareTag("Store"))
    //        {

    //        }
    //    }
    //}

    //public void ActiveShop(bool isOpen)//
    //{ 
    //    shop.SetActive(isOpen);
    //}

    //public void DeActiveShop()//
    //{
    //    ActiveShop(false);
    //}
}
