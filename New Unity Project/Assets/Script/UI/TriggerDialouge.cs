using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriggerDialouge : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public GameObject Dialouge;
    public DialougeManager dialougeManager;
    public string[] Name;
    [TextArea(3, 10)]
    public string[] sentences;
    public bool isTouching;
    public bool isSwitchScene = false;

    private void Update()
    {
        if (isTouching == true)
        {
            Trigger();
        }
    }

    public void Trigger()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //nameText.text = Name;
            Dialouge.SetActive(true);
            dialougeManager.StartDialouge(this);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            isTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouching = false;
    }
}