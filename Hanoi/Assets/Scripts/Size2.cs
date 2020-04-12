using UnityEngine;

public class Size2 : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<PlateSize>().setSize(2);
    }
}

