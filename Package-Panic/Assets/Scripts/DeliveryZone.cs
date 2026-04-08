using UnityEngine;
using Unity.Netcode;

public class DeliveryZone : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer) return;

        if (other.CompareTag("Package"))
        {
            GameManager.Instance.AddScore(10);

            PackagePool.Instance.ReturnPackageToPool(other.gameObject);
        }
    }
}