using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBobbing : MonoBehaviour
{
    public float bobbingSpeed = 0.18f;    // Speed of the bobbing motion
    public float bobbingAmount = 0.05f;   // Amount of vertical movement
    public float midpoint = 2.0f;         // The midpoint position of the camera

    private float timer = 0.0f;
    private float defaultPosY;

    void Start()
    {
        // Get the initial y position of the camera
        defaultPosY = transform.localPosition.y;
    }

    void Update()
    {
        // Get the player's movement inputs
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // If the player is moving
        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f; // Reset timer when there's no movement
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer += bobbingSpeed;

            if (timer > Mathf.PI * 2)
            {
                timer -= Mathf.PI * 2;
            }
        }

        if (waveslice != 0)
        {
            float translateChange = waveslice * bobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;

            // Apply the bobbing effect to the y position
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + translateChange, transform.localPosition.z);
        }
        else
        {
            // Reset the camera to its default position when there's no movement
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY, transform.localPosition.z);
        }
    }
}