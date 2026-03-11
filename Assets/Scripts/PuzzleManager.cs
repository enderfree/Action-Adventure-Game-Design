using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int[] correctSequence = { 3, 1, 5, 7 };
    private int currentStep = 0;

    public GameObject door;

    public void PressButton(int buttonID)
    {
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
        }
    }

    void PuzzleSolved()
    {
        Debug.Log("Puzzle Solved!");

        if (door != null)
        {
            door.SetActive(false); // opens door
        }
    }
}