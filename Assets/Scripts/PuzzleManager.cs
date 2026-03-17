using UnityEngine;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    public int[] correctSequence = { 3, 1, 5, 7 };
    private int currentStep = 0;

    public GameObject door;
    public AudioSource openSound;
    public AudioSource wrongButton;
    public List<GameObject> buttons;

    public void PressButton(int buttonID)
    {
        Debug.Log(currentStep);

        if (buttonID == correctSequence[currentStep])
        {
            currentStep++;

            if (currentStep >= correctSequence.Length)
            {
                PuzzleSolved();
            }
        }
        else
        {
            Debug.Log("Wrong button. Reset puzzle.");
            currentStep = 0;
            wrongButton.Play();

            foreach (GameObject button in buttons)
            { 
                if (button.TryGetComponent<PuzzleButton>(out PuzzleButton puzzleButton))
                {
                    puzzleButton.Unpress();
                }
            }
        }
    }

    void PuzzleSolved()
    {
        Debug.Log("Puzzle Solved!");

        if (openSound != null)
        {
            openSound.Play();
        }

        if (door != null)
        {
            door.SetActive(false);
        }
    }
}