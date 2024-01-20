using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusManager : MonoBehaviour
{
    public GameObject tori;
    public List<Color> colors;
    public Transform pos;

    private void Start()
    {
        InstantiateTori();
    }
    // Instantiate Tori
    public void InstantiateTori()
    {
        GameObject toriClone = Instantiate(tori, pos.position, Quaternion.identity);

        //Random Color
        int randomColor = Random.Range(0, colors.Count);
        toriClone.GetComponent<Renderer>().material.color = colors[randomColor];
    }
    void Match()
    {
        // Destroy Matching Donuts -> LOL!

    }
}
