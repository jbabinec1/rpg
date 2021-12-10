using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectPanel : MonoBehaviour, IPointerExitHandler
{
    public bool alreadyVisited;
    public bool visiting;
    public PlayerMovement movementscript;
    public GameObject player;
    [SerializeField] public PlayerMovement getClickState;

    void Awake()
    {
    player = GameObject.FindWithTag("Playa");
    alreadyVisited = false;

    movementscript = player.GetComponent<PlayerMovement>();
    }

    public void OnPointerEnter()
    {
        visiting = true;
        Debug.Log("hovering rn");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        alreadyVisited = true;
        visiting = false;
        MyContextMenu.Instance.DeleteContextMenu();

        //set canClick back to true so context menu can be instantiated again after not hovered over Panel game obj
        if (movementscript.canCLick == false) {

            movementscript.canCLick = true;
        }


        
    }

}
