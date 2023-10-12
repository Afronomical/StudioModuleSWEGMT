using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsMenuController : MonoBehaviour
{
    [SerializeField] private TMP_Text m_Text;
    [SerializeField] private List<string> instructionTexts = new();

    private int instructionIndex = 0;

    public void Back()
    {
        SceneManager.LoadScene(1);
        //Debug.Log("Back to main menu!");
    }

    private void Start()
    {
        DisplayText();
    }

    public void NextText()
    {
        if (instructionIndex < instructionTexts.Count - 1)
        {
            instructionIndex++;
            DisplayText();
        }
    }

    public void PrevText()
    {
        if (instructionIndex > 0)
        {
            instructionIndex--;
            DisplayText();
        }
    }

    private void DisplayText()
    {
        m_Text.text = instructionTexts[instructionIndex];
    }
}
