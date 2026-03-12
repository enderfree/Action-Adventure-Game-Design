using UnityEngine;

public class PlayAnimationOnPress : MonoBehaviour
{
    public Animator animator;          // animation controller
    public AudioSource buttonSound;    // button press sound

    private bool playerNear = false;
    private bool pressed = false;      // prevents spam

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E) && !pressed)
        {
            pressed = true;

            if (buttonSound != null)
            {
                buttonSound.Play();
            }

            if (animator != null)
            {
                animator.SetTrigger("Play");
            }
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