using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Web Web;
    public MyNetworkManager mynetworkmanager;

    public static Main Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Web = GetComponent<Web>(); //Component that holds logic making http requests
        mynetworkmanager = GetComponent<MyNetworkManager>(); //Component of custom NetworkManager methods
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
