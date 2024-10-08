using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    public float scrollSpeed = 10f;
    public bool isVertical = true;
    public float minY = -5f;
    public float maxY = 5f;

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        Vector3 scrollDirection = isVertical ? Vector3.down : Vector3.left;
        Vector3 movement = scrollDirection * scrollInput * scrollSpeed;

        transform.position += movement;

        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }
}