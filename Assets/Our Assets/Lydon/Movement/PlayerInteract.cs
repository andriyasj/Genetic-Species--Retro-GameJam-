using System;
using UnityEngine;

public interface ITakeover
{
    public void Takeover();
}

public class PlayerInteract : MonoBehaviour
{
    public float InteractRange = 6f;
    public Camera playerCamera;
    //private bool _canInteract = false;

    public void AttemptTakeover()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out ITakeover takeoverObj))
            {
                takeoverObj.Takeover();
            }
        }
    }
}