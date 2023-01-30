using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound  {
    public AudioClip clip;
    public string name;
    [Range(0f, 1f)]
    [SerializeField] float volume;
    [Range(0f, 1f)]
    [SerializeField] float volumeVariance = .1f;


    [Range(.1f, 3f)]
    [SerializeField]     float pitch;
    [Range(0f, 1f)]
    [SerializeField] float pitchVariance = .1f;

    public bool loop;


    public float Volume
    {
        get
        {
            return volume + Random.Range(-10f * volumeVariance, 10f * volumeVariance)/10f;
        }
    }
    public float Pitch
    {
        get
        {
            return pitch + Random.Range(-10f * pitchVariance, 10f * pitchVariance) / 10f; 
        }
    }
}
