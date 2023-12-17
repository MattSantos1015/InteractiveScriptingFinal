using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunshot : MonoBehaviour
{
    AudioSource gunShot;
    // Start is called before the first frame update
    void Start()
    {
        gunShot = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Mouse0))
        {
            gunShot.Play();
        }
    }
}
