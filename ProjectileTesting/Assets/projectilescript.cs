using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectilescript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyObject(gameObject, 5f);
    }
}
