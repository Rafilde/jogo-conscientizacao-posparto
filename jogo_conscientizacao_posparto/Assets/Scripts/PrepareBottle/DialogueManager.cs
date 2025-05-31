using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    private Queue<string> dialogueLines = new Queue<string>();
    private bool isDialogueActive = false;

    private Action onDialogueEnd; // <-- callback

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Novo método com callback opcional
    public void StartDialogue(string[] lines, Action onEnd = null)
    {
        dialogueLines.Clear();
        foreach (string line in lines)
        {
            dialogueLines.Enqueue(line);
        }

        onDialogueEnd = onEnd; // <-- armazena o callback
        dialoguePanel.SetActive(true);
        isDialogueActive = true;
        DisplayNextLine();
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
        }
    }

    void DisplayNextLine()
    {
        if (dialogueLines.Count == 0)
        {
            EndDialogue();
            return;
        }

        string line = dialogueLines.Dequeue();
        dialogueText.text = line;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;

        onDialogueEnd?.Invoke(); // <-- chama o callback, se existir
        onDialogueEnd = null;    // limpa referência
    }
}
