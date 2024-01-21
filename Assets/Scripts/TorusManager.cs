using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorusManager : MonoBehaviour
{
    public GameObject tori;
    [NonReorderable]
    public List<ColorItems> colorItems;
    public Transform pos;

    public float colourChangeTime;
    public float initialMoveSpeed = 5f; // Set your initial move speed here
    public float maxMoveSpeed = 10f; // Set your maximum move speed here
    private float currentMoveSpeed; // Variable to track the current move speed

    int count = 0;

    GameManager gameManager;
    ScoreManager scoreManager;
    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        gameManager = FindObjectOfType<GameManager>();

        currentMoveSpeed = initialMoveSpeed;
        InstantiateTori();
    }
    private void Update()
    {
        Match();
    }
    // Instantiate Tori
    public void InstantiateTori()
    {
        if (gameManager.isGameOver)
            return;

        GameObject toriClone = Instantiate(tori, pos.position, Quaternion.identity);
        toriClone.name = "Tori " + count;
        //Random Color
        int randomColor = UnityEngine.Random.Range(0, colorItems.Count);
        toriClone.GetComponent<Tori>().toriColor = colorItems[randomColor].color;

        // Increase the move speed
        currentMoveSpeed = Mathf.Min(currentMoveSpeed + 1f, maxMoveSpeed);

        // Set the move speed for the instantiated Tori
        toriClone.GetComponent<Tori>().moveSpeed = currentMoveSpeed;

        count++;
    }
    public void AddMatchPoint()
    {
        scoreManager.score++;
    }
    IEnumerator RandomColorChange(Color color, Tori tori)
    {
        yield return new WaitForSeconds(colourChangeTime);

        int randomColor = UnityEngine.Random.Range(0, colorItems.Count);
        tori.toriColor = colorItems[randomColor].color;
    }
    void Match()
    {
        foreach (var colourItem in colorItems)
        {
            if(colourItem.matchingObjects.Count > 1)
            {
                for (int i = 0; i < colourItem.matchingObjects.Count; i++)
                {
                    AddMatchPoint();
                    Destroy(colourItem.matchingObjects[i]);
                }
                colourItem.matchingObjects.Clear();
            }
        }
    }
}
[Serializable]
public class ColorItems
{
    public Color color;
    [NonReorderable]
    public List<GameObject> matchingObjects; //Matching objects will be added here
}
