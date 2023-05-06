using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialougeManager : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI dialougeSentence;
    private Queue<string> sentences;
    private Queue<string> names;
    private bool isSwitchScene;
    public Animator transition;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
        gameObject.SetActive(false);
    }

    public void StartDialouge(TriggerDialouge dialouge)
    {
        sentences.Clear();
        names.Clear();
        foreach (string sentence in dialouge.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (string name in dialouge.Name)
        {
            names.Enqueue(name);
        }
        isSwitchScene = dialouge.isSwitchScene;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialouge();
            return;
        }
        string sentence = sentences.Dequeue();
        dialougeSentence.text = sentence;
        string name = names.Dequeue();
        Name.text = name;
    }

    public void EndDialouge()
    {
        
        if (isSwitchScene)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
        else gameObject.SetActive(false);
        //PlayerManager.instance.gameObject.GetComponent<Player>().EnableMove();
    }

    IEnumerator LoadLevel(int level)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(level);
    }
}