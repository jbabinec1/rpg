using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
//using Unity.Plastic.Newtonsoft.Json;
using Newtonsoft.Json;
using Mirror;



public class Web : MonoBehaviour
{
   // UserLoggedIn userLoggedInn;
    TMP_Text outputArea;
    Text email;
    public bool isLoggedIn = false;
    NetworkManager manager;

    GameObject gameSlot;
    Canvas canvas;
    public inventoryuicode inventorysl;

    public Sprite[] sprites;
    

    //[SerializeField] public TMP_Text outputArea = null;

    void Start() {

        outputArea = GameObject.Find("OutputArea").GetComponent<TMP_Text>();
        manager = GetComponent<NetworkManager>();

        //GameObject slot = Instantiate(gameSlot, canvas.transform.position, Quaternion.identity);
        //  slot.transform.SetParent(canvas.transform, false);

       


    }





    NetworkConnection conn;
    public IEnumerator Login(string email, string password)
    {

        //var user = new UserLoginInfo();
        var user = new UserLoginInfo();
        user.email = email;
        user.password = password;
       

        string json = JsonUtility.ToJson(user);

        var req = new UnityWebRequest("http://localhost:23864/api/identity/login", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
 
        yield return req.SendWebRequest();
        

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.Log(req.error);
        } else
        {
            isLoggedIn = true;
#pragma warning disable CS0436 // Type conflicts with imported type
            var userData = JsonConvert.DeserializeObject<UserLoggedIn>(req.downloadHandler.text);
#pragma warning restore CS0436 // Type conflicts with imported type
            outputArea.text = userData.nickname.ToString();
            PlayerPrefs.SetString("token", userData.token.ToString());
            
           
        }

        if(isLoggedIn)
        {

             NetworkManager.singleton.StartClient();  

            //string inventoryString =  PlayerPrefs.GetString("inventory");
            //string toJson = JsonUtility.ToJson(inventoryString);
            //var inventoryData = JsonConvert.DeserializeObject<ItemData>(toJson);
            //outputArea.text = inventoryData.NameOfItem.ToString();

        }
       manager.networkAddress = "35.233.140.87";

    }





 /*   public IEnumerator GetUserData()
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

        //var userData = JsonConvert.DeserializeObject<UserLoggedIn>(req.downloadHandler.text);
        //PlayerPrefs.SetString("inventory", userData.inventory.ToString());

        //string inventoryString =  PlayerPrefs.GetString("inventory");

        yield return req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.Log(req.error);
        }
        else
        {
            var userData = JsonConvert.DeserializeObject<Inventory>(req.downloadHandler.text);
            var userDataCount = userData.inventory.Count;
            PlayerPrefs.SetString("inventory", userData.inventory.ToString());
            string inventory = PlayerPrefs.GetString("inventory");
           //  Debug.Log(userDataCount); //Using userDataCount because trying to see if counting array correctly
  

           // for (int slot = 0; slot < inventoryslot.slots.Length; slot++)
        /*    {
                for (int i = 0; i < userData.inventory.Count; i++)
                {
                    if (inventoryslot.isFull[slot] == false) { 
               
                        inventoryslot.isFull[i] = true;
                        Instantiate(gameItem, inventoryslot.slots[slot].transform, false);
                        Debug.Log(userData.inventory[i].NameOfItem);
                        break;
                }
            } */

      //  } 
          
            //outputArea.text = userData.nickname.ToString();



            
     //   }      

 //   }  */






    


    public IEnumerator AddItemToInventory(string nameOfItem, bool isEquipped, int spriteIndex)
    {
        var item = new ItemData();
        //int numba = 1;
        item.spriteIndex = spriteIndex;
        item.NameOfItem = nameOfItem;
        item.IsEquipped = isEquipped;
        
        //item.sprite = sprites[numba];


        string json = JsonUtility.ToJson(item);

        var req = new UnityWebRequest("http://localhost:23864/api/identity/PostInventoryItem", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        string token = PlayerPrefs.GetString("token");
        req.SetRequestHeader("Authorization", "Bearer " + token);

        yield return req.SendWebRequest();


        if (req.isNetworkError || req.isHttpError)
        {
            Debug.Log(req.error);
        }
        else
        {
            // isLoggedIn = true;
              var itemData = JsonConvert.DeserializeObject<ItemData>(req.downloadHandler.text);
            Debug.Log(itemData.IsEquipped);
            //  outputArea.text = userData.nickname.ToString();

              //Debug.Log(itemData.NameOfItem);


            Debug.Log("done");
            
        }
       

    }

    



}










/* public class UserData
{
    public string email;
    public string password;      
    
} */



/* [System.Serializable]
public class Inventory
{
    public Object[] inventory;
   // public inventory[];
    public string nickname;
    public string concurrencyStamp;
    public string nameOfItem;
    
} */



/* public class ItemData
{
    public string NameOfItem;
    
} */



/* public class UserLoggedIn
{
    public string username;
    public string nickname;
    public string email;
    public string token;
    public Object[] inventory;
   // public List<ItemData> inventory { get; set; }

} */



/* Don't think this is actually being used anywhere?
public class UserInformation
{
    public Inventory[] Inventory; 
}  */