using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public ItemBuffer itemBuffer;
    public Transform slotRoot;

    public bool activeInventory = false;
    public GameObject inventoryPanel;

    public bool activeInventory2 = false;
    public GameObject inventoryPanel2;


    private List<Slot> slots;

    public System.Action<ItemProperty> OnSlotClick;
    // Start is called before the first frame update
    void Start()
    {
       
        slots = new List<Slot>();
        int slotCnt = slotRoot.childCount;

        inventoryPanel.SetActive(activeInventory);
        inventoryPanel2.SetActive(activeInventory2);


        for (int i=0; i < slotCnt; i++)
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
            slots.Add(slot);
        }

        
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);

            activeInventory2 = !activeInventory2;
            inventoryPanel2.SetActive(activeInventory2);
        }


    }

    
    
    //public void RayShop()//
    //{
    //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    mousePos.z = -10;
    //    RaycastHit2D hit2D = Physics2D.Raycast(mousePos, transform.forward, 30);
    //    if (hit2D.collider != null)
    //    {
    //        if (hit2D.collider.CompareTag("Store"))
    //        {

    //        }
    //    }
    //}
  
    public void OnClickSlot(Slot slot)
    {
       if (OnSlotClick != null)
        {
            OnSlotClick(slot.item);
        }
    }
}
