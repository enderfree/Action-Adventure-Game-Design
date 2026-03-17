using System.Collections;
using UnityEngine;

public class SimonButton : MonoBehaviour, IHittable
{
    public int buttonID;
    public SimonPuzzleManager puzzleManager;
    [SerializeField] Animator button;
    public Renderer buttonRenderer;
    public AudioSource buttonSound;

    public Color glowColor = Color.yellow;
    private Color originalColor;

    private bool isPressed = false;

    void Start()
    {
        originalColor = buttonRenderer.material.color;
    }

    void Update()
    {
        
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

    public void OnHit()
    {
        if (!isPressed)
        {
            buttonSound.Play();
            button.Play("Press Button");
            isPressed = true;

            PressButton();
            Unpress();
        }
    }

    public void Unpress()
    {
        if (isPressed)
        {
            buttonSound.Play();
            button.Play("Unpress Button");
            isPressed = false;
        }
    }
}