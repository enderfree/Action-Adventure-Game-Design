using UnityEngine;

public class Button : MonoBehaviour, IHittable
{
    [SerializeField] Animator button;
    [SerializeField] GameObject iActivatable;

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
        button.Play("Press Button");

        if (iActivatable != null) 
        {
            iActivatable.GetComponent<IActivatable>().Activate();
        }
    }

    public void Unpress()
    {
        button.Play("Unpress Button");
    }
}
