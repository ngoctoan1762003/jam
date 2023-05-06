using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerText : MonoBehaviour
{
    public GameObject interactTextUI;
    public InteractText interactText;
    public string Text;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            interactTextUI.SetActive(true);
            interactText.ChangeText(Text);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactTextUI.SetActive(false);
    }
}