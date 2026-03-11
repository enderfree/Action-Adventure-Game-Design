using UnityEngine;

public class HammerHitHitbox : MonoBehaviour
{
    public bool hitting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hitting && other.TryGetComponent<IHittable>(out IHittable iHittable))
        {
            iHittable.OnHit();
        }
    }
}
