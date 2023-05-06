using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutoText : MonoBehaviour
{
    public TextMeshProUGUI tutoText;
    // Start is called before the first frame update
    void Update()
    {
        tutoText.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2, 0));
    }
}
