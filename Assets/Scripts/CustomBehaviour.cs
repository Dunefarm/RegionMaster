﻿using UnityEngine;
using System.Collections;

public class CustomBehaviour : MonoBehaviour {

    float _customMouseClickCounter = 0;
    float _customMouseClickThreshold = 0.1f;
    bool _customOnMouseClicked = false;
    float _customScreenspaceDragDistanceThreshold = 0.05f;
    Vector3 _customMousePositionOnDown;

    protected bool _mouseHold;
    protected Transform _transform;

    void Awake()
    {
        _transform = transform;
        CustomAwake();
    }

    void Start()
    {
        CustomStart();
    }

    void Update()
    {
        if (_customOnMouseClicked)
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
        _mouseHold = true;
        OnMouseDown();
    }

    public void CustomOnMouseDown(Vector2 mousePos, Camera cam)
    {
        _customMousePositionOnDown = cam.ScreenToViewportPoint(mousePos);
        _customMouseClickCounter = 0;
        _customOnMouseClicked = true;
        _mouseHold = true;
        OnMouseDown();
    }

    public virtual void OnMouseDown()
    {
    }

    public void CustomOnMouseUp()
    {
        _mouseHold = false;
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
        float dist = (_customMousePositionOnDown - cam.ScreenToViewportPoint(mousePos)).sqrMagnitude;
        if (_customMouseClickCounter > _customMouseClickThreshold
            || dist >= Mathf.Pow(_customScreenspaceDragDistanceThreshold, 2))
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

    public virtual void OnMouseHover()
    { }

    public virtual void OnMouseStopHover()
    { }
}
