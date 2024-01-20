using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tori : MonoBehaviour
{
    public float speed;
    public Color toriColor;
    public float maxX, minX, fallSpeed;
    private int direction = 1; // 1 for right, -1 for left
    bool isMove = true, isFalling = false;

    Rigidbody rb;
    TorusManager torusManager;
    private void Start()
    {
        torusManager = FindObjectOfType<TorusManager>();
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        //rb.WakeUp();
    }
    private void FixedUpdate()
    {
        Move();
        Fall();
        Rotate();
    }
    // Move left and right - move from max x and min x
    void Move()
    {
        if (!isMove)
            return;

        // Calculate the new position based on the speed and direction
        float newPosition = transform.position.x + direction * speed * Time.deltaTime;

        // Change direction when reaching boundaries
        if (newPosition > maxX || newPosition < minX)
        {
            direction *= -1; // Change direction
        }

        transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
    }
    // Rotate
    void Rotate()
    {
        if (!isMove)
            return;

    }
    void RandomColorChange()
    {

    }
    void Fall()
    {
        if (Input.GetMouseButton(0) && !isFalling)
        {
            isFalling = true;
            isMove = false;
            rb.useGravity = true;
        }

        // If falling, move the object downwards
        if (isFalling)
        {
            Debug.Log("Fall!");

            // Move the Torus downward
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }
    // Reached Bottom then Instantiate a new tori

}
