using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class MyNetWorkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text displayNameText = null;
   // [SerializeField] private Renderer displayColourRenderer = null; 


    [SyncVar(hook="SetPlayerName")] //SyncVar broadcasts variable and changes to all clients // SetPlayerName is a callback function to put DisplayName on text UI
    [SerializeField]
    private string displayName = "No name"; 

   // [SyncVar(hook="SetDisplayColor")]
    //[SerializeField]
    //private Color displayColor = Color.black;

    #region

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;

    }

   // [Server]
    //Callback for displayColor that changes actual object color in UI 
  //  public void SetDisplayColor(Color oldColour, Color newColour)
    //{
      //  displayColourRenderer.material.SetColor("_Color", newColour);

   // }



    //The 'hook' that is referenced/called from front-end .. actual server method is included
    [Command]
  private void CmdSetName(string newName)
    {
        RpcSetName(newName); //Calling to all clients from server

        SetDisplayName(newName); 
    }



    #endregion





    //Client side functions
    #region  


    //Actual Front end method that calls server method
    [ContextMenu("Set name yo")]
    public void GetName()
    {
        CmdSetName("New number who dis");   //Need to eventually attach variable in params that contains name entered from form
    }

    [ClientRpc]
    public void RpcSetName(string name) {
        Debug.Log(name);
    }



    //Callback for displayName that changes text above players head in UI..
    //Binds text to actual text field
    public void SetPlayerName(string oldText, string displayName)
    {
        displayNameText.text = displayName;
    }






   // public void setColor(Color newDisplayColour)
    //{
     //   displayColor = newDisplayColour;
    //}

    #endregion




}
