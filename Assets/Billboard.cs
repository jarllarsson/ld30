using UnityEngine;
using System.Collections;

using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update()
    {
        //dir.y = 0.0;
        transform.rotation = Camera.main.transform.rotation;
    }
}