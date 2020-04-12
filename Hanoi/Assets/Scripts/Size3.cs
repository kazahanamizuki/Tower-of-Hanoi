using UnityEngine;

public class Size3 : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<PlateSize>().setSize(3);
    }
}

