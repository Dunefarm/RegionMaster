using UnityEngine;
using System.Collections;

public class CustomBehaviour : MonoBehaviour {

    float _customMouseClickCounter = 0;
    float _customMouseClickThreshold = 0.1f;
    bool _customOnMouseClicked = false;

    void Awake()
    {
        CustomAwake();
    }

    void Start()
    {
        CustomStart();
    }

    void Update()
    {
        if(_customOnMouseClicked)
        {
            _customMouseClickCounter += Time.deltaTime;
        }
        CustomUpdate();
    }

    protected virtual void CustomAwake()
    { }

    protected virtual void CustomStart()
    { }

    protected virtual void CustomUpdate()
    { }

    public void CustomOnMouseDown()
    {
        _customMouseClickCounter = 0;
        _customOnMouseClicked = true;
        OnMouseDown();
    }

    public virtual void OnMouseDown()
    {
    }

    public void CustomOnMouseUp()
    {
        OnMouseUp();
        _customOnMouseClicked = false;
        if (_customMouseClickCounter <= _customMouseClickThreshold)
        {
            OnMouseClicked();
        }
    }

    public virtual void OnMouseUp()
    {
    }

    public void CustomOnMouseUpOff()
    {
        _customOnMouseClicked = false;
        OnMouseUpOff();
    }

    public virtual void OnMouseUpOff()
    {
        print("Didn't let go from me!");
    }

    public void CustomOnMouseHold(Vector3 mousePos, Camera cam)
    {
        if (_customMouseClickCounter > _customMouseClickThreshold)
        {
            OnMouseHold(mousePos, cam);
        }
    }

    public virtual void OnMouseHold(Vector3 mousePos, Camera cam)
    {
        print("holding me!");
    }

    /// <summary>
    /// Called if object is clicked. Clicked = Mouse down and up on it in a sufficiently small timespan.
    /// </summary>
    public virtual void OnMouseClicked()
    {

    }

    // - - Delegates

    public delegate void d_NoArgVoid();

    public static event d_NoArgVoid lol;
}
