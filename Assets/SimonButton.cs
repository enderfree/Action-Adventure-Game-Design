using System.Collections;
using UnityEngine;

public class SimonButton : MonoBehaviour, IHittable
{
    public int buttonID;
    public SimonPuzzleManager puzzleManager;

    [SerializeField] private Animator button;
    public Renderer buttonRenderer;
    public AudioSource buttonSound;

    public Color glowColor = Color.yellow;
    private Color originalColor;

    private bool isPressed = false;

    void Start()
    {
        originalColor = buttonRenderer.material.color;
    }

    // Called when player hits the button
    public void OnHit()
    {
        if (!isPressed)
        {
            isPressed = true;

            // Play sound once
            if (buttonSound != null)
                buttonSound.Play();

            // Play press animation
            if (button != null)
                button.Play("Press Button");

            // Send input to puzzle manager
            if (puzzleManager != null)
                puzzleManager.PlayerPress(buttonID);

            // Visual feedback
            StartCoroutine(Flash());

            // Delay unpress
            StartCoroutine(UnpressDelay());
        }
    }

    // Flash effect (NO SOUND here anymore)
    public IEnumerator Flash()
    {
        buttonRenderer.material.color = glowColor;

        yield return new WaitForSeconds(0.5f);

        buttonRenderer.material.color = originalColor;
    }

    // Delayed unpress (fixes instant reset bug)
    private IEnumerator UnpressDelay()
    {
        yield return new WaitForSeconds(0.5f);

        if (button != null)
            button.Play("Unpress Button");

        isPressed = false;
    }
}