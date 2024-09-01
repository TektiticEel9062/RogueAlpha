using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip Piedra;
    public AudioClip Alfombra;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // This function will be called automatically by Unity when a collision occurs
    void OnCollisionEnter(Collision collision)
    {
        // Check for the tag of the colliding object and play the corresponding sound
        if (collision.gameObject.tag == "Piedra")
        {
            audioSource.clip = Piedra;
            audioSource.Play();
        }
        else if (collision.gameObject.tag == "Alfombra")
        {
            audioSource.clip = Alfombra;
            audioSource.Play();
        }
    }
}