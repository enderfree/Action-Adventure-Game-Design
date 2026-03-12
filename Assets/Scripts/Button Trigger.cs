using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject wall; // the wall that will disappear
    private bool playerNear = false;
    public AudioSource buttonSound;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            PressButton();
            wall.SetActive(false);
        }
    }
    void PressButton()
    {
        if (buttonSound != null)


        {
            buttonSound.Play();
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