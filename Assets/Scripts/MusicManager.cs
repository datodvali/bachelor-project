using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource _introMusic, _loopedMusic;

    void Start() {
        _introMusic.Play();
        _loopedMusic.PlayScheduled(AudioSettings.dspTime + _introMusic.clip.length);
    }
}
