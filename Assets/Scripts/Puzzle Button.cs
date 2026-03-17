using UnityEngine;

public class PuzzleButton : MonoBehaviour, IHittable
{
    public int buttonID;                // number of this button
    public PuzzleManager puzzleManager; // reference to the puzzle manager

    [SerializeField] Animator button;
    // [SerializeField] GameObject iActivatable;
    [SerializeField] AudioSource buttonSound;

    bool isPressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnHit()
    {
        if (!isPressed)
        {
            buttonSound.Play();
            button.Play("Press Button");
            isPressed = true;

            puzzleManager.PressButton(buttonID);
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