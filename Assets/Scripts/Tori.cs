using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tori : MonoBehaviour
{
    public float speed;
    public Color toriColor;
    public float maxX, minX;
    private int direction = 1; // 1 for right, -1 for left

    TorusManager torusManager;
    private void Start()
    {
        torusManager = FindObjectOfType<TorusManager>();
    }

    private void Update()
    {
        Move();
        Rotate();
    }
    // Move left and right - move from max x and min x
    void Move()
    {
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

    }
    void RandomColorChange()
    {

    }
    void Fall()
    {
        if(Input.GetMouseButton(0))
        {

        }
    }
}
