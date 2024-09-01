using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class audio : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    public AudioClip audioClip2;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        //si acabo el audio 1 empieza el audio 2
        if (audioClip.length == audioSource.time){
            StartCoroutine(Wait());

            audioSource.clip = audioClip2;
            audioSource.Play();
        }
        //el el audio 2 acaba se cambia de escena usando scene manager en vez de application.loadlevel
        if (audioClip2.length == audioSource.time)
        {

            SceneManager.LoadScene("Level");
        }
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(10f);
    }


}
