using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




[System.Serializable]
public class ContextMenuItem
{
    public string text;
    public Button button;
    public Action<Image> action;
    //public Action<Image> poop;

    public ContextMenuItem(string text, Button button, Action<Image> action)
    {
        this.text = text;
        this.button = button;
        this.action = action;

    }

}



public class MyContextMenu : MonoBehaviour
{
    public Image contentPanel;
    public Canvas canvas;


    Vector2 mousePos;
    public Camera cam;
    public Image panelClone;

    public bool isExited;

    public static MyContextMenu instance;



    public static MyContextMenu Instance    //public static ContextMenu Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(MyContextMenu)) as MyContextMenu;

                if (instance == null)
                {
                    var newContextMenu = (new GameObject("MyContextMenu")).AddComponent<MyContextMenu>();
                    instance = newContextMenu;


                }
            }
            return instance;
        }

    }




    public void CreateContextMenu(List<ContextMenuItem> items, Vector2 position)
    {

        panelClone = Instantiate(contentPanel, new Vector3(position.x, position.y, 0), Quaternion.identity) as Image;
        //as Image;
        panelClone.transform.SetParent(canvas.transform);
        panelClone.transform.SetAsLastSibling();
        panelClone.rectTransform.anchoredPosition = position;

        foreach (var item in items)
        {
            ContextMenuItem tempReference = item;
            Button button = Instantiate(item.button) as Button;
            Text buttonText = button.GetComponentInChildren(typeof(Text)) as Text;
            buttonText.text = item.text;
            button.onClick.AddListener(delegate { tempReference.action(panelClone); });
            button.transform.SetParent(panelClone.transform);


        }

    }





    public void DeleteContextMenu()
    {
        Destroy(panelClone.gameObject);

    }

    public bool isMenuNull()
    {
        if(panelClone.gameObject == null)
        {
            return true;
        }
        return false;
    }


  /*  public bool doesMenuExist()
    {
        Vector2 touchPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit2D = Physics2D.Raycast(touchPostion, Vector2.zero);

        return hit2D.collider != null ? hit2D.collider.gameObject : null;
    } */


}
