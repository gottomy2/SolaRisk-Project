using UnityEngine;

public class Fracture : MonoBehaviour
{
    [Tooltip("\"Fractured\" is the object that this will break into")]
    public GameObject fractured;
   
    public void FractureObject()
    {
        var instance = Instantiate(fractured, transform.position, transform.rotation); //Spawn in the broken version
        instance.transform.localScale = gameObject.transform.localScale;

        foreach (Transform child in instance.transform)
        {
           
            float x = Random.Range(-100.0f,100.0f);
            float y = Random.Range(-100.0f, 100.0f);
            float z = Random.Range(-100.0f, 0);
            //Debug.Log(child.gameObject.name);
            Rigidbody rb = child.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(x, y, z);
            child.gameObject.AddComponent<Disappear>();
            Destroy(child.gameObject, 5f);
        }
        Destroy(gameObject); //Destroy the object to stop it getting in the way
    }
}
