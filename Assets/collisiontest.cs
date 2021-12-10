using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisiontest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "real_player")
        {

            Destroy(other.gameObject);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
