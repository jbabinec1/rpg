using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class YoItemController : MonoBehaviour
{
    public Button sampleButton;

    private List<ContextMenuItem> contextMenuItems;

    public MyContextMenu contextMenu;

    public Canvas canvas;

    public bool canClick;

    public bool alreadyVisited;

    public bool isLoggedIn;

    public bool isEquipped = false;

    public Image contentPanel;

    //  public InventoryUI inventory;
    public inventoryuicode inventory;

    public Inventory userData;

    public GameObject gameItem;

    public GameObject player;

    public GameObject slots;

    public GameObject gameSlot;

    public GameObject spawnHereItem;

    public Sprite[] sprites;



     void Awake()
    {
        canClick = true;
        alreadyVisited = false;

        contextMenuItems = new List<ContextMenuItem>();

        Action<Image> equip = new Action<Image>(EquipAction);

        contextMenuItems.Add(new ContextMenuItem("Equip", sampleButton, equip));

        inventory = GameObject.FindGameObjectWithTag("playerplaceholder").GetComponent<inventoryuicode>();

        //  inventory = player.GetComponent<InventoryUI>();



        string inventoryArray = PlayerPrefs.GetString("inventory");

        userData = JsonConvert.DeserializeObject<Inventory>(inventoryArray);

    }





    void OnMouseOver()   //OnMouseOver()
    {


        if (Input.GetMouseButtonDown(1) && canClick)
        {


            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
            MyContextMenu.Instance.CreateContextMenu(contextMenuItems, new Vector2(pos.x, pos.y));

            canClick = false;



        }


    }


    void OnMouseExit()
    {
        canClick = true;
    }



    void EquipAction(Image contextPanel)
    {
        string nameItem = "YoYo";
        int spriteIndex = 1;
        //isEquipped = true;

        var length = inventory.slots.Length;
        Debug.Log(length);

        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;
                isEquipped = true;

                gameItem.GetComponent<Image>().sprite = inventory.sprites[spriteIndex];
                Instantiate(gameItem, inventory.slots[i].transform, false);
                Destroy(gameObject);

                ItemData item = gameItem.GetComponent<ItemData>();


                //might need to add this (ItemData item) in the GetData() function.. 
                break;

            }
        }

        if (isEquipped)
        {
            StartCoroutine(Main.Instance.Web.AddItemToInventory(nameItem, true, spriteIndex));
        }


        Destroy(contextPanel.gameObject);


        canClick = true;


    }




}
