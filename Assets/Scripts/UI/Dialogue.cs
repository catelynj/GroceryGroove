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
    private int index;
    public Image clerkSprite;
    public Image bossSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();

        //clerkSprite.color = new Color(0.16078f, 0.4880f, 0.6745f, 1f); original color
        //bossSprite.color = new Color(0.6754f,0.16185f,0.16185f, 1f); orignial color
        clerkSprite.color = new Color(0.2279f, 0.2532f, 0.2679f, 0.5f);
        clerkSprite.transform.SetAsFirstSibling();
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeDialogueLine());
    }

    IEnumerator TypeDialogueLine()
    {
        foreach(char c in dialogueLines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        //consider moving sprite changes to the coroutine to make the logic easier 
        //bool isTalking = true means make it dark and behind, false means reset to original and move to front etc. etc. 
        
        // also consider adding the character noises to this coroutine
    }

    public void NextDialogueLine()
    {
        if(index < dialogueLines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeDialogueLine());


            //temporary solution for demo purposes -- will rework sprite modification later when more dialogue and art is added
            bossSprite.color = new Color(0.3056f,0.2739f,0.2739f, 0.5f); // darker and behind clerk
            bossSprite.transform.SetAsFirstSibling();
            clerkSprite.color = new Color(0.16078f, 0.4880f, 0.6745f, 1f); // reset to original
        }
        else
        {
            SceneManager.LoadScene("LevelSelect");
        }
    }


}
