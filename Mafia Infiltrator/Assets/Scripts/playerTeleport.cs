using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerTeleport : MonoBehaviour
{
    [SerializeField] private Button interactButton;
    [SerializeField] private GameObject redGate; // Reference to the red gate GameObject
    [SerializeField] private GameObject blueGate; 
    private GameObject currentTeleporter;

    void Start()
    {
        interactButton.onClick.AddListener(OnInteractButtonClicked);
    }

    private void OnInteractButtonClicked()
    {
        if (currentTeleporter != null && currentTeleporter.GetComponent<Teleporter>().IsTeleporterEnabled())
        {
            transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }
}
