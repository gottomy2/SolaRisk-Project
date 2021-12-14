using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class Wire : MonoBehaviour {
    private Vector3 _startPoint;
    private Vector3 _startPosition;

    public GameObject lightOn;

    // public SpriteRenderer wireEnd;
    private LineRenderer _lineRenderer;

    void Start() {
        _startPoint = transform.parent.position;
        _startPosition = transform.position;
        _lineRenderer = gameObject.GetComponent<LineRenderer>();
    }


    private void OnMouseDrag() {
        // mouse position to world point
        if (Camera.main is { }) {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;

            // check for Connection
            Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, .2f);
            foreach (Collider2D collider in colliders) {
                if (collider.gameObject != gameObject) {
                    UpdateWire(collider.transform.position);
                    if (transform.parent.name.Equals(collider.transform.parent.name)) {
                        Main.Instance.SwitchChange(1);
                        collider.GetComponent<Wire>()?.Done();
                        Done();
                    }

                    return;
                }
            }

            // update wire
            UpdateWire(newPosition);
        }
    }

    void Done() {
        lightOn.SetActive(true);
        Destroy(this);
    }

    private void OnMouseUp() {
        // reset wire position
        UpdateWire(_startPosition);
    }

    void UpdateWire(Vector3 newPosition) {
        // update and position
        transform.position = newPosition;
        // update direction
        Vector3 direction = newPosition - _startPoint;
        transform.right = direction * transform.lossyScale.x;

        _lineRenderer.SetPosition(0, _startPosition);
        _lineRenderer.SetPosition(1, newPosition);
    }
}