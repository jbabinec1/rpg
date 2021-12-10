using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using Object = UnityEngine.Object;
using System.Collections.Generic;

public class UserLoggedIn
{
    public string username;
    public string nickname;
    public string email;
    public string token;
    //public Object[] inventory;

    public List<ItemData> inventory { get; set; } = new List<ItemData>();
}
