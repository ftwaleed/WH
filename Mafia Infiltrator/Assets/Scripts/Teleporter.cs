using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private bool isTeleporterEnabled = true;

    public Transform GetDestination()
    {
        return destination;
    }

    public void EnableTeleporter()
    {
        isTeleporterEnabled = true;
    }

    public void DisableTeleporter()
    {
        isTeleporterEnabled = false;
    }

    public bool IsTeleporterEnabled()
    {
        return isTeleporterEnabled;
    }
}
