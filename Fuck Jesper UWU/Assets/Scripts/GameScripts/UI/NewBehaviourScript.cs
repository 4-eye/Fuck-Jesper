using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public UI_manager uiManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Call the AddKeyUI method of the UI_manager when key is picked up
            uiManager.AddKeyUI();

            // Disable or destroy the key GameObject after pickup
            gameObject.SetActive(false);
            // or Destroy(gameObject);
        }
    }
}
