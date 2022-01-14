using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPosition : MonoBehaviour
{
    private void Start()
    {
        gameObject.transform.position = GlobalData.playerTransformPosition;
        gameObject.transform.rotation = GlobalData.playerTransformRotation;
    }
    private void OnDestroy()
    {
        GlobalData.playerTransformPosition = gameObject.transform.position;
        GlobalData.playerTransformRotation = gameObject.transform.rotation;
    }
}
