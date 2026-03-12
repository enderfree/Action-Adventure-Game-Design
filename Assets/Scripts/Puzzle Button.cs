using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public int buttonID;                // number of this button
    public PuzzleManager puzzleManager; // reference to the puzzle manager

    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            puzzleManager.PressButton(buttonID);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}