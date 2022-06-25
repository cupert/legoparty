using UnityEngine;

public class ReturnBulletsOutsideTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.BULLET))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            bullet.Despawn();
        }
    }
}
