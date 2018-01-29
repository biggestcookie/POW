using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (this.gameObject.CompareTag("Lightning"))
        {
            Animation Vfx = this.gameObject.GetComponent<Animation>();
            Vfx.Play();
            Destroy(this.gameObject, Vfx.clip.length);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}