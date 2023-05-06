using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractText : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    public Player player;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    // Update is called once per frame

    public void ChangeText(string changeText)
    {
        interactText.text = changeText;
    }
}