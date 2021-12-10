using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public List<ItemData> inventory { get; set; } = new List<ItemData>();
   // public Object[] inventory;
    public string nickname;
    public string concurrencyStamp;
    public string nameOfItem;
    public bool IsEquipped;
    public Sprite[] sprites;
    public int spriteIndex;

}
