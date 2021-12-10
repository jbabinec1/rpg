using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetUserInfo : MonoBehaviour
{
    //public string token;
    // Start is called before the first frame update

    public InventoryUI inventory;


    public Inventory userData;
    public inventoryuicode inventoryslot;
    public GameObject gameItem;

    public ItemData itemm;
    public inventoryuicode inventoryInfo;

    public int spriteindex;

    public int spriteNum;

    void Start()
    {

        // StartCoroutine(Main.Instance.Web.GetUserData());

        // Debug.Log("boobs " + inventorysl.slots.Length);

     //   StartCoroutine(GetUserData()); ///testing this in awake rn
    }



    void Awake()
    {
        inventoryslot = GameObject.FindGameObjectWithTag("playerplaceholder").GetComponent<inventoryuicode>();

       // gameItem.GetComponent<Image>().sprite = inventoryInfo.sprites[userData.inventory[spriteindex].spriteIndex];

        StartCoroutine(GetUserData());
    }

    // Update is called once per frame
    void Update()
    {

    }


    //Get all user info including inventory + spawn inventory items into open inventory slots
    public IEnumerator GetUserData()
    {
        var user = new UserLoginInfo();


        string json = JsonUtility.ToJson(user);

        var req = new UnityWebRequest("http://localhost:23864/api/identity/GetUserData", "Get");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        string token = PlayerPrefs.GetString("token");
        req.SetRequestHeader("Content-Type", "application/json");
        req.SetRequestHeader("Authorization", "Bearer " + token);
      

        yield return req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.Log(req.error);
        }
        else
        {
            var userData = JsonConvert.DeserializeObject<Inventory>(req.downloadHandler.text);
            var userDataCount = userData.inventory.Count;
            //var inventoryitemdata = userData.inventory[0].spriteIndex;
            PlayerPrefs.SetString("inventory", userData.inventory.ToString());
            string inventory = PlayerPrefs.GetString("inventory");


            //If inventory item exists in db and slot is available, instantiate gameItem
           
                for (int i = 0; i < userData.inventory.Count; i++)
            {
                for (int slot = 0; slot < inventoryslot.slots.Length; slot++)
                {
                    //for (int sprites = 0; sprites < userData.inventory.Count; sprites++)
                    //{ }

                    
                    //var spritenumber = userData.inventory[i].spriteIndex;


                    if (userData.inventory[i].IsEquipped == true && inventoryslot.isFull[slot] == false)
                    {

                        // inventoryInfo.sprites[spritenumber]

                        spriteNum = userData.inventory[i].spriteIndex;

                        gameItem.GetComponent<Image>().sprite = gameItem.GetComponent<ItemData>().sprites[spriteNum];
                        //Debug.Log(userData.inventory[i].spriteIndex);
                        Instantiate(gameItem, inventoryslot.slots[slot].transform, false);
                        inventoryslot.isFull[slot] = true;
                        break;



                    }

                
             

            }


                //outputArea.text = userData.nickname.ToString();


            }




        }

    }







}
