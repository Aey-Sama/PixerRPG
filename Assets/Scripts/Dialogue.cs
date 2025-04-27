using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel; // The dialogue UI panel
    public TextMeshProUGUI dialogueText; // The dialogue text
    public GameObject pressFPrompt; // The "Press F" prompt UI
    public Transform npcTransform; // The NPC position to follow
    public Vector3 offset = new Vector3(0, 2f, 0); // Offset above the NPC
    public string[] lines;
    public float textSpeed = 0.05f;

    private int index;
    private bool isPlayerInRange;
    private bool isDialogueActive;
    public AreaExit areaExit;


    private void Start()
    {
        dialoguePanel.SetActive(false);
        if (pressFPrompt != null)
            pressFPrompt.SetActive(false); // Make sure "Press F" prompt is hidden initially
    }

    private void Update()
    {
        if (isPlayerInRange && !isDialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartDialogue();
            }

            // Update "Press F" position above NPC in screen space
            if (pressFPrompt != null && npcTransform != null)
            {
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(npcTransform.position + offset);
                pressFPrompt.transform.position = screenPosition;
            }
        }

        if (isDialogueActive && Input.GetKeyDown(KeyCode.F))
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }


    private void StartDialogue()
    {
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        if (pressFPrompt != null)
            pressFPrompt.SetActive(false); // Hide "Press F" once dialogue starts
        index = 0;
        dialogueText.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }
    



    private void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);

        if (areaExit != null)
        {
            areaExit.canEnterDungeon = true; // Properly unlock
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (pressFPrompt != null)
                pressFPrompt.SetActive(true); // Show "Press F" when player gets near
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (pressFPrompt != null)
                pressFPrompt.SetActive(false); // Hide "Press F" when player leaves
        }
    }
}
