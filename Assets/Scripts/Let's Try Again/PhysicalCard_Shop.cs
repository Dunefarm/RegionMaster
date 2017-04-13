using UnityEngine;
using System.Collections;

public class PhysicalCard_Shop : PhysicalCard {

    private float DISTANCE_FROM_DISCARD_PILE = 5;

    private void LetGo()
    {
        if (Vector3.Distance(Transform.position, MegaManager.CurrentPlayer.DiscardPileTranform.position) < 5)
        {
            MegaManager.CurrentPlayer.TryToBuyCard(Card);
        }
        MegaManager.Shop.ReturnCard(Card);
    }

    public override void OnMouseClicked()
    {
        print("Clicked a shop card");
    }

    public override void OnMouseUp()
    {
        if (_draggingCard)
            LetGo();
        _draggingCard = false;
    }

    public override void OnMouseHold(Vector3 mousePos, Camera cam)
    {
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
