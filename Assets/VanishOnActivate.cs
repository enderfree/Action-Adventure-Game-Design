using UnityEngine;

public class VanishOnActivate : MonoBehaviour, IActivatable
{
    [SerializeField] AudioSource vanishingSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        if (vanishingSound != null)
        {
            vanishingSound.Play();
        }

        gameObject.SetActive(false);
    }
}
