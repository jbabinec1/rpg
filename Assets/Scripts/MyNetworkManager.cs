using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;

public class MyNetworkManager : NetworkManager
{
    public bool isLoggedIn = false;
    TMP_Text outputArea;

   

    /*
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("Connected to client");
    } */

    public override void OnServerAddPlayer(NetworkConnection conn) {
    base.OnServerAddPlayer(conn);
    MyNetWorkPlayer player = conn.identity.GetComponent<MyNetWorkPlayer>();
        //this.GetStartPosition() = this.transform.position;
        player.SetDisplayName($"Player {numPlayers}");
       // playerPrefab.AddComponent<BoxCollider2D>();
        //playerPrefab.AddComponent<Rigidbody2D>();


    }








    public IEnumerator Login(string email, string password)
    {
        // var user = new UserData();
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
        }
        else
        {
            isLoggedIn = true;
            var userData = JsonConvert.DeserializeObject<UserLoggedIn>(req.downloadHandler.text);
            outputArea.text = userData.nickname.ToString();
            PlayerPrefs.SetString("token", userData.token.ToString());
        }

        if (isLoggedIn)
        {
            // NetworkManager.singleton.OnServerAddPlayer(NetworkConnection conn)
            //NetworkManager.singleton.StartHost();
            //Main.Instance.mynetworkmanager.OnServerAddPlayer(conn);

        }

    }





}
