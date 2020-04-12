using UnityEngine;

public class Size4 : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<PlateSize>().setSize(4);
    }
}
