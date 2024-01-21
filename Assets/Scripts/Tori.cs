using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class Tori : MonoBehaviour
{
    public Color toriColor;
    AudioManager audioManager;
    
    private int direction = 1; // 1 for right, -1 for left
    int randomRot;

    bool isMove = true, isFalling = false, isInsidePole = false;
    bool isGetInput;
    bool Play= true;

    public float moveSpeed, rotationSpeed;
    public float maxX, minX, fallSpeed;

    Rigidbody rb;
    TorusManager torusManager;
    GameManager gameManager;
    private void Start()
    {
        torusManager = FindObjectOfType<TorusManager>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;

        MoveSpeed();

        //InvokeRepeating("RandomColorChange", Random.Range(0, 2), Random.Range(0, 2));
    }
    private void FixedUpdate()
    {
        isGetInput= !EventSystem.current.IsPointerOverGameObject();
        this.GetComponent<Renderer>().material.color = toriColor;

        if(!gameManager.isStart)
            return;
        
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

        switch (randomRot)
        {
            case 0:
                transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
                break;
            case 1:
                transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);
                break;
            case 2:
                transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
                break;
            case 3:
                transform.Rotate(Vector3.one * rotationSpeed * Time.deltaTime);
                break;
            case 4:
                transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
                break;
            case 5:
                transform.Rotate(Vector3.zero * rotationSpeed * Time.deltaTime);
                break;
        }
    }
    void MoveSpeed()
    {
        // Set the initial move speed
        moveSpeed = torusManager.currentMoveSpeed;

        if (moveSpeed > 5)
        {
            randomRot = 5; //normal
        }
        else if (moveSpeed > 8)
        {
            randomRot = Random.Range(0, 2);
        }
        else if(moveSpeed > 10)
        {
            randomRot = Random.Range(0, 4);
        }
        else if(moveSpeed > 15)
        {
            randomRot = Random.Range(0, 5);
        }
    }
    void RandomColorChange()
    {
        if(!isMove) return;
        StartCoroutine(torusManager.RandomColorChange(this));
    }
    void Fall()
    {
        if (gameManager.isGameOver)
            return;

        if (Input.GetMouseButton(0) && !isFalling && isGetInput)
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
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pole"))
        {
            isInsidePole = true;
            Debug.Log("Is inside the pole!");
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tori"))
        {
            if(Play)
            {
                audioManager.Play("Bounce");
                Play = false;
            }
            
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
            FillPole();
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
    void FillPole()
    {
        if (isFalling && transform.position.y > 9.4f)
        {
            gameManager.isGameOver = true;
        }
    }
}

