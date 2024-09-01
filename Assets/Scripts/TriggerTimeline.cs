using UnityEngine;
using UnityEngine.Playables;

public class TriggerTimeline : MonoBehaviour
{
    public GameObject player;
    public GameObject timeline; // Reference to the Timeline

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeline.SetActive(true); // Activate the Timeline
        }
        Debug.Log("Triggered");
    }
}