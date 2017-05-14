using UnityEngine;
using System.Collections;

public class PhysicalCard_Hand : PhysicalCard {

    public override void OnMouseUp()
    {
        if (_draggingCard)
        {
            if (Camera.main.WorldToViewportPoint(Transform.position).y < 0.25f) //Return to hand
            {
                ReturnToHand();
            }
            else
            {
                Card.PlayCard();
            }
        }
        _draggingCard = false;
    }

    public override void OnMouseHold(Vector3 mousePos, Camera cam)
    {
        if (!TurnPhases.IsCurrentPhase(TurnPhase.Place))
            return;

        _draggingCard = true;
        RaycastHit hit;
        Debug.DrawRay(cam.transform.position, cam.ScreenToWorldPoint(mousePos + Vector3.forward * 1000000));
        if (Physics.Raycast(cam.ScreenPointToRay(mousePos), out hit, Mathf.Infinity, Card.MegaMan.TableLayerMask))
        {
            Transform.position = hit.point + Vector3.back * 1;
        }
        //Vector3 mousePlusDepth = mousePos + Vector3.forward * Vector3.Distance(cam.transform.position, originalHandPosition);
    }
}
