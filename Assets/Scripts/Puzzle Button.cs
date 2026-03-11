using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public int buttonID;
    public PuzzleManager puzzleManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puzzleManager.PressButton(buttonID);
        }
    }
}