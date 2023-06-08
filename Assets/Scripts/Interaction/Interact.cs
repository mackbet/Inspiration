using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private Interaction target;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Interaction interaction))
        {
            target = interaction;
            target.SetOutline(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Interaction interaction))
        {
            if (target = interaction)
            {
                target.SetOutline(false);
                target = null;
            }
        }
    }

    public void TryToInteract()
    {
        if (target == null)
            return;

        target.Activate();
    }
}
