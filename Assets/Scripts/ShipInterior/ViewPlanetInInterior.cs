using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPlanetInInterior : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (GlobalData.playerPosition != "Pstart")
        {
            //gameObject.GetComponent<MeshFilter>().mesh = GlobalData.currentMeshFilter.mesh;
            //gameObject.GetComponent<MeshRenderer>().materials = GlobalData.currentMeshRenderer.materials;
        }
    }

    // Update is called once per frame
        void FixedUpdate()
    {
        gameObject.transform.Rotate(new Vector3(1f,1f,1f) * Time.deltaTime);
    }
}
