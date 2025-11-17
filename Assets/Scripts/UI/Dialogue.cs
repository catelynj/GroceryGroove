using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] dialogueLines;
    public float textSpeed = 0.03f;
    private int _index;
    public Image clerkSprite;
    public Image bossSprite;
    public Button btnNextLine;
    private bool _isTyping = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();

        clerkSprite.color = new Color(1f, 1f, 1f, 0.5f);
        clerkSprite.transform.SetAsFirstSibling();

        btnNextLine.enabled = false;
    }

    void StartDialogue()
    {
        _index = 0;
        StartCoroutine(TypeDialogueLine());
    }

    IEnumerator TypeDialogueLine()
    {
        foreach (char c in dialogueLines[_index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        _isTyping = false;
        btnNextLine.enabled = true;
    }

    public void NextDialogueLine()
    {
        if(_index < dialogueLines.Length - 1)
        {
            _index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeDialogueLine());

            bossSprite.color = new Color(1f,1f,1f, 0.5f); // boss half opacity 
            bossSprite.transform.SetAsFirstSibling();     // boss moves to back
            clerkSprite.color = new Color(1, 1, 1, 1f); // reset clerk to original

            _isTyping = true;
        }
        else
        {
            SceneManager.LoadScene("LevelSelect");
        }
    }


}
