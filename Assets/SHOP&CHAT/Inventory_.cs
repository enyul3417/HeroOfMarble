using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_ : MonoBehaviour
{
    public GameObject inventoryPanel;//
    bool activeInventory = false;//

    public System.Action<ItemProperty> OnSlotClick;//

    public Transform rootSlot;
    public Store store;

    public List<Item> items = new List<Item>();
    public int SlotCnt;
    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    private List<Slot> slots;
    // Start is called before the first frame update

    
    void Start()
    {
        inventoryPanel.SetActive(activeInventory);//
        int slotCnt = rootSlot.childCount;

        slots = new List<Slot>();

        

        for(int i=0; i < slotCnt; i++)
        {
            var slot = rootSlot.GetChild(i).GetComponent<Slot>();

            slots.Add(slot);
        }

        store.OnSlotClick += BuyItem;
       


    }

   void BuyItem(ItemProperty item)
    {
       var emptySlot = slots.Find(t =>
        {
            return t.item == null || t.item.name == string.Empty;
            
        });

        if(emptySlot != null)
        {
            emptySlot.SetItem(item);
        }
    }

    public bool AddItem(Item _item)
    {
        if(items.Count< SlotCnt)
        {
            items.Add(_item);
            if(onChangeItem !=null)
            onChangeItem.Invoke();
            return true;
        }
        return false;
    }


    //아이템과 충돌시 AddItem 호출 (주인공 플레이어가 아이템과 충돌할 때)
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FieldItem"))
        {
            FieldItems fieldItems = collision.GetComponent<FieldItems>();
            if (AddItem(fieldItems.GetItem()))
                fieldItems.DestroyItem(); // 아이템 추가 성공시 필드아이템 파괴됨
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }

    public void OnClickSlot(Slot slot)
    {
        if (OnSlotClick != null)
        {
            OnSlotClick(slot.item);
        }
    }

}
