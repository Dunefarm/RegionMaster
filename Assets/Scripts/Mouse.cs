using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Mouse : MonoBehaviour {

    public MegaManager MegaMan;

    public Camera cam;
    public GameObject PlayerMarker;

    CustomBehaviour CurrentlyClickedCustom;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                CurrentlyClickedCustom = LookThroughForCustom(hit.transform);
                if (CurrentlyClickedCustom != null) 
                    CurrentlyClickedCustom.CustomOnMouseDown();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                CustomBehaviour cust = LookThroughForCustom(hit.transform);
                if (cust != null && cust == CurrentlyClickedCustom)
                {
                    cust.CustomOnMouseUp();
                }
                else if(CurrentlyClickedCustom != null)
                {
                    CurrentlyClickedCustom.CustomOnMouseUpOff();
                }
                CurrentlyClickedCustom = null;
            }
        }
        if(Input.GetMouseButton(0) && CurrentlyClickedCustom != null)
        {
            CurrentlyClickedCustom.CustomOnMouseHold(Input.mousePosition, cam);
        }
    }

    //void HitToken(RaycastHit hit)
    //{
    //    Token token = hit.transform.root.GetComponent<Token>();
    //    if (token == null)
    //        return;
    //    if (token.OwnerTypeOwner == Token.OwnerTypes.None)
    //    {
    //        token.OwnerTypeOwner = token.GetCurrentPlayer();
    //        //colMan.AddTokenToPool(token);
    //        //GridMan.CheckForCompletedRegions();
    //        //GameObject marker = (GameObject) Instantiate(PlayerMarker, token.transform.position, Quaternion.identity);
    //        //marker.transform.parent = token.transform;
    //    }
    //}

    void HitCard()
    { }

    CustomBehaviour LookThroughForCustom(Transform trans)
    {
        CustomBehaviour cust = trans.GetComponent<CustomBehaviour>();
        if (cust != null)
            return cust;
        Transform t = trans.parent;
        while (t != null)
        {
            cust = t.GetComponent<CustomBehaviour>();
            if (cust != null)
                return cust;
            t = t.parent;
        }
        return null;
    }
}
