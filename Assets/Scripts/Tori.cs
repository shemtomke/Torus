using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Tori : MonoBehaviour
{
    public Color toriColor;
    
    private int direction = 1; // 1 for right, -1 for left

    bool isMove = true, isFalling = false, isInsidePole = false;

    public float moveSpeed, rotationSpeed;
    public float maxX, minX, fallSpeed;
    public float raycastDistance = 5f;
    public float xOffset = 0.5f; // Offset in the x-axis

    public List<GameObject> sameColorTorus = new List<GameObject>();

    Rigidbody rb;
    TorusManager torusManager;
    GameManager gameManager;
    private void Start()
    {
        torusManager = FindObjectOfType<TorusManager>();
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
    }
    private void FixedUpdate()
    {
        this.GetComponent<Renderer>().material.color = toriColor;

        Move();
        RotateRight();
        Fall();
        Rotate(); 
    }
    // Move left and right - move from max x and min x
    void Move()
    {
        if (!isMove)
            return;

        // Calculate the new position based on the speed and direction
        float newPosition = transform.position.x + direction * moveSpeed * Time.deltaTime;

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
    void RotateRight()
    {
        if (!isMove)
            return;
        
    }
    void RandomColorChange()
    {
        
    }
    void Fall()
    {
        if (gameManager.isGameOver)
            return;

        if (Input.GetMouseButton(0) && !isFalling)
        {
            isFalling = true;
            isMove = false;
            rb.useGravity = true;
            Invoke("NewTori", 0.5f);
        }

        // If falling, move the object downwards
        if (isFalling)
        {
            // Move the Torus downward
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }
    void NewTori()
    {
        torusManager.InstantiateTori();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pole"))
        {
            isInsidePole = true;
        }
        if (collision.gameObject.CompareTag("Tori"))
        {
            var obj = collision.gameObject;
            var objTori = collision.gameObject.GetComponent<Tori>();

            if (toriColor == objTori.toriColor)
            {
                foreach (var colorItem in torusManager.colorItems)
                {
                    if (toriColor == colorItem.color)
                    {
                        if (!colorItem.matchingObjects.Contains(obj))
                            colorItem.matchingObjects.Add(obj);

                        if (!colorItem.matchingObjects.Contains(this.gameObject))
                            colorItem.matchingObjects.Add(this.gameObject);
                    }
                }
            }

            OutsidePole();
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            OutsidePole();
        }
    }
    void OutsidePole()
    {
        if (!isFalling)
            return;

        if(!isInsidePole)
        {
            gameManager.isGameOver = true;
        }
    }
}
