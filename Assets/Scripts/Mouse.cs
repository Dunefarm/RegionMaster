using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Mouse : MonoBehaviour {

    public MegaManager MegaMan;

    public Camera cam;
    public GameObject PlayerMarker;

    CustomBehaviour CurrentlyHoveredCustom;
    CustomBehaviour LastClickedCustom;
    CustomBehaviour LastHoveredCustom;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
    {
        LastHoveredCustom = CurrentlyHoveredCustom;

        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
        {
            CurrentlyHoveredCustom = LookThroughForCustom(hit.transform);
        }

        OnMouseStopHover();

        if (CurrentlyHoveredCustom != null)
        {
            OnMouseDown();
            OnMouseHover();
        }

        if(LastClickedCustom != null)
        {
            OnMouseUp();
            OnMouseHold();
        }   
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LastClickedCustom = CurrentlyHoveredCustom;
            LastClickedCustom.CustomOnMouseDown(Input.mousePosition, cam);
        }
    }

    void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (LastClickedCustom == null)
                return;

            if (CurrentlyHoveredCustom == LastClickedCustom)
                CurrentlyHoveredCustom.CustomOnMouseUp();
            else
                LastClickedCustom.CustomOnMouseUpOff();

            LastClickedCustom = null;
        }
    }

    void OnMouseHold()
    {
        if (Input.GetMouseButton(0) && LastClickedCustom != null)
        {
            LastClickedCustom.CustomOnMouseHold(Input.mousePosition, cam);
        }
    }

    void OnMouseHover()
    {
        CurrentlyHoveredCustom.OnMouseHover();
    }

    void OnMouseStopHover()
    {
        if (LastHoveredCustom == null)
            return;

        if (LastHoveredCustom != CurrentlyHoveredCustom)
        {
            LastHoveredCustom.OnMouseStopHover();
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
