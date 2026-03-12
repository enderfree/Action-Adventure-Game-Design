using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonPuzzleManager : MonoBehaviour
{
    public List<int> sequence = new List<int>();
    private int playerStep = 0;

    public int sequenceLength = 4;

    public SimonButton[] buttons;

    public GameObject wall;
    public AudioSource openSound;
    public AudioSource failSound;

    private bool isShowingSequence = false;

    void Start()
    {
        GenerateSequence();
        StartCoroutine(ShowSequence());
    }

    void GenerateSequence()
    {
        sequence.Clear();

        for (int i = 0; i < sequenceLength; i++)
        {
            sequence.Add(Random.Range(0, buttons.Length));
        }
    }

    IEnumerator ShowSequence()
    {
        isShowingSequence = true;

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < sequence.Count; i++)
        {
            yield return StartCoroutine(buttons[sequence[i]].Flash());
            yield return new WaitForSeconds(0.5f);
        }

        playerStep = 0;
        isShowingSequence = false;
    }

    public void PlayerPress(int buttonID)
    {
        if (isShowingSequence) return;

        if (buttonID == sequence[playerStep])
        {
            playerStep++;

            if (playerStep >= sequence.Count)
            {
                PuzzleSolved();
            }
        }
        else
        {
            StartCoroutine(FailSequence());
        }
    }

    IEnumerator FailSequence()
    {
        if (failSound != null)
        {
            failSound.Play();
        }

        playerStep = 0;

        yield return new WaitForSeconds(1f);

        StartCoroutine(ShowSequence());
    }

    void PuzzleSolved()
    {
        Debug.Log("Puzzle Solved!");

        if (openSound != null)
        {
            openSound.Play();
        }

        if (wall != null)
        {
            wall.SetActive(false);
        }
    }
}