using UnityEngine;

public class SmackClientInFace : MonoBehaviour
{
    private ClientManager _clientManager;

    private void Start()
    {
        _clientManager = FindObjectOfType<ClientManager>();
        if (_clientManager == null)
        {
            Debug.LogError("There should be a client manager in the scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            _clientManager.DialogRead();
        }
    }
}