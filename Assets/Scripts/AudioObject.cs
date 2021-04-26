using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioObject : MonoBehaviour
{

    public AudioClip[] clips;

    public float deathTime;

    private float time;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= deathTime)
        {
            Destroy(this.gameObject);
        }
        time += Time.deltaTime;
    }
}
