using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorusManager : MonoBehaviour
{
    public GameObject tori;
    public ParticleSystem particle;

    [NonReorderable]
    public List<ColorItems> colorItems;
    Vector3 pos;

    public float colourChangeTime;
    public float maxMoveSpeed = 10f; // Set your maximum move speed here
    public float currentMoveSpeed; // Variable to track the current move speed
    public float incrementSpeed = 0.5f;
    public float particleLifetime = 2f;

    int count = 0;

    GameManager gameManager;
    ScoreManager scoreManager;
    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        gameManager = FindObjectOfType<GameManager>();

        particle.Stop();

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

        pos = new Vector3(UnityEngine.Random.Range(-10, 14), 13.05f, 0);
        GameObject toriClone = Instantiate(tori, pos, Quaternion.identity);
        toriClone.name = "Tori " + count;
        //Random Color
        int randomColor = UnityEngine.Random.Range(0, colorItems.Count);
        toriClone.GetComponent<Tori>().toriColor = colorItems[randomColor].color;

        // Increase the move speed
        currentMoveSpeed = Mathf.Min(currentMoveSpeed + incrementSpeed, maxMoveSpeed);

        // Set the move speed for the instantiated Tori
        toriClone.GetComponent<Tori>().moveSpeed = currentMoveSpeed;

        count++;
    }
    public void AddMatchPoint()
    {
        scoreManager.score++;
    }
    public IEnumerator RandomColorChange(Tori tori)
    {
        yield return new WaitForSeconds(colourChangeTime);

        int randomColor = UnityEngine.Random.Range(0, colorItems.Count);
        tori.toriColor = colorItems[randomColor].color;
    }
    void Match()
    {
        foreach (var colourItem in colorItems)
        {
            if (colourItem.matchingObjects.Count > 1)
            {
                foreach (var matchingObject in colourItem.matchingObjects)
                {
                    AddMatchPoint();

                    // Instantiate the particle system at the position of the matching object
                    ParticleSystem particleInstance = Instantiate(particle, matchingObject.transform.position, Quaternion.identity);

                    // Set the particle system color
                    var mainModule = particleInstance.main;
                    mainModule.startColor = colourItem.color;

                    // Play the particle system
                    particleInstance.Play();

                    // Destroy the matching object
                    Destroy(matchingObject);

                    // Destroy the particle system after a certain duration
                    Destroy(particleInstance.gameObject, particleLifetime);
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
