using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class itemSlot : MonoBehaviour, IPointerClickHandler
{
    //======ITEM DATA======//
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField]
    private int maxNumberOfItems; 

    //=======ITEM SLOT=====//
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    //=======ITEM DESCRIPTION SLOT===//
    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }


    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        // check to see if slot is allready full
        if (isFull)
            return quantity;

        //update name 
        this.itemName = itemName;

        //update image
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;

        //update desription
        this.itemDescription = itemDescription;

        //update quantity
        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;


            //return leftovers
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }

        //update quintity text
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;
    }






    

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button== PointerEventData.InputButton.Left)
        {
            OnLeftCLick();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }

    }

    public void OnLeftCLick()
    {
       inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
        ItemDescriptionNameText.text = itemName;
        ItemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;
        if (itemDescriptionImage.sprite == null)
        {
            itemDescriptionImage.sprite = emptySprite;
        }


    }

    public void OnRightClick()
    { 
    
    }
}


    
    
    
    
    
    
   

