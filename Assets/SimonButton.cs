using System.Collections;
using UnityEngine;

public class SimonButton : MonoBehaviour
{
    public int buttonID;
    public SimonPuzzleManager puzzleManager;

    public Renderer buttonRenderer;
    public AudioSource buttonSound;

    public Color glowColor = Color.yellow;
    private Color originalColor;

    private bool playerNear = false;

    void Start()
    {
        originalColor = buttonRenderer.material.color;
    }

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            PressButton();
        }
    }

    void PressButton()
    {
        StartCoroutine(Flash());

        if (puzzleManager != null)
        {
            puzzleManager.PlayerPress(buttonID);
        }
    }

    public IEnumerator Flash()
    {
        if (buttonSound != null)
        {
            buttonSound.Play();
        }

        buttonRenderer.material.color = glowColor;

        yield return new WaitForSeconds(0.5f);

        buttonRenderer.material.color = originalColor;
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