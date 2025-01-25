using UnityEngine;

public class Shield : MonoBehaviour
{
    public Transform leftHand;

    private void Update()
    {
        transform.position = leftHand.position;
        transform.rotation = leftHand.rotation;
    }
}
