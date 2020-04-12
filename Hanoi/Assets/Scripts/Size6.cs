using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size6 : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<PlateSize>().setSize(6);
    }
}
