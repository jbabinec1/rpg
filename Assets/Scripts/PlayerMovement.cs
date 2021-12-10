using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5.0f;

    public Rigidbody2D rb;

    public Camera cam1;

    Vector2 movement;

    Vector2 mousePos;

    Vector2 lastClickedPos;

    Vector2 lastClicked; //Testing click to walk from UI function
    bool moving;

    ItemController honeyObj;

    public bool canCLick;
    public bool clickToWalk;

    public Sprite currentWeapon;
    public GameObject bullet;
    public float walkSpeed = 1;
    public int damage = 20;
    public float step;
    public float getTime;

    public LayerMask groundLayer;


    //public ContextMenu contextMenu;
    public Image contentPanel;
    public Button sampleButton;
    private List<ContextMenuItem> contextMenuItems;




    #region
    [Command]
    private void CmdMove(Vector2 lastClickedPos, float step)
    {
        transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
    }


    [Command]
    private void CmdLook(float angle)
    {

       rb.rotation = angle;
    }


  


    
    [Command]
    private void CmdMoveLeft()
    {
        transform.position = Vector2.MoveTowards(transform.position, -transform.right, step);
        // rb.velocity = new Vector2(moveSpeed * -1, rb.velocity.y);
        // Debug.Log("Move left");


        // gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed * -1, moveSpeed);
        //gameObject.GetComponent<Rigidbody2D>().MovePosition(transform.position + Vector3.forward * Time.deltaTime * 4);

        
    }





    #endregion


    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        contextMenuItems = new List<ContextMenuItem>();
        Action<Image> walk = new Action<Image>(WalkAction);

        contextMenuItems.Add(new ContextMenuItem("Walk", sampleButton, walk));
        canCLick = true;
        clickToWalk = false;


       // gameObject.GetComponent<BoxCollider2D>().size = new Vector2(
    //gameObject.GetComponent<RectTransform>().sizeDelta.x,
    //gameObject.GetComponent<RectTransform>().sizeDelta.y
    //);

    }




    private void Start()
    {
        
    }


    [ClientCallback]
    void Update()
    {

        if(!hasAuthority) { return; }
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam1.ScreenToWorldPoint(Input.mousePosition);

        //zDebug.Log(Input.GetAxisRaw("Horizontal"));


    }




    [ClientCallback]
    private void FixedUpdate()
    {

        //if (!hasAuthority) { return; }
        //Moves player with Arrow & WASD keys       
        // rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        //Get vector position between mouse position and player sprite position


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CmdMoveLeft();
        }

            


        

         Vector2 lookDir = mousePos - rb.position;

         float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 5f;

        // rb.rotation = angle;


        //Mouse click movement
        //Removing for now       if (EventSystem.current.IsPointerOverGameObject()) return; //Prevents character from moving if mouse is hovered and clicked over context menu

        var checkIfMoveable = CheckForObjectUnderMouse();
        //&& checkIfMoveable.tag == "bridge_planks"
        if (IsGrounded() && isMouseClickInBoundary() != null && Input.GetMouseButtonDown(0))
        {
            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
            CmdMove(lastClickedPos, step);
            CmdLook(angle);

        }


        if (moving && (Vector2)transform.position != lastClickedPos)
        {
            float step = walkSpeed * Time.deltaTime;
            CmdMove(lastClickedPos, step);
            
        }

        else
        {
            moving = false;
        }

      

        ///contentPanel.GetComponent<DetectPanel>().alreadyVisited == true

        var mouseSelection = CheckForObjectUnderMouse();

        if (Input.GetMouseButtonDown(1) && isMouseClickInBoundary() && isPickupItem().gameObject.name != "pickup" && canCLick == true)  //mouseSelection == null
        { 
            canCLick = false;
            MyContextMenu.Instance.CreateContextMenu(contextMenuItems, new Vector2(Input.mousePosition.x, Input.mousePosition.y));

            
            
        }

        //If WalkAction has been activted by walk button and not already moving, move character
        if (clickToWalk == true && moving == false)  //IsGrounded()
        {
            
            moving = true;
            CmdLook(angle);

        }

        if (moving && (Vector2)transform.position != lastClickedPos)
        {
            float step = walkSpeed * Time.deltaTime;
            CmdLook(angle);
            CmdMove(lastClickedPos, step);

        }
        else
        {
            moving = false;
            clickToWalk = false;
            
        }


    }





    void WalkAction(Image contextPanel)
    {
        //Debug.Log("Walking");
        clickToWalk = true;
        lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Destroy(contextPanel.gameObject);
        // canCLick = true;
    }


    //Check if mouse is over object so context menu won't show
    private GameObject CheckForObjectUnderMouse()
    {

        Vector2 touchPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit2D = Physics2D.Raycast(touchPostion, Vector2.zero);

        return hit2D.collider != null ? hit2D.collider.gameObject : null;

    }


    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);

        Color rayColor;
        if (hit.collider != null)  //hit.collider != null 
        {
            rayColor = Color.green;
            
            return true;
        } 
        return false;
    }



    private GameObject isMouseClickInBoundary()
    {
        Vector2 touchPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(touchPostion, Vector2.zero, groundLayer);

        
        return hit.collider != null ? hit.collider.gameObject : null;

    }



    private GameObject isPickupItem()
    {
        Vector2 touchPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(touchPostion, Vector2.zero, groundLayer);

        return hit.collider != null ? hit.collider.gameObject : null;


    }





}
