using UnityEngine;

public class Button : MonoBehaviour, IHittable
{
    [SerializeField] Animator button;
    [SerializeField] GameObject iActivatable;
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

            if (iActivatable != null)
            {
                iActivatable.GetComponent<IActivatable>().Activate();
            }
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
