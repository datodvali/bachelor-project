using UnityEngine;

public class StartScreenScript : MonoBehaviour
{
    [SerializeField] private AudioSource _startScreenMusic;
    [SerializeField] private float _totalVolumeIncreaseTime = 2f;
    private float _elapsedVolumeIncreaseTime;
    [SerializeField] private float _timeBeforeStart = 1f;
    private float _elapsedTimeBeforeStart;
    private float _targetVolume;

    void OnEnable() {
        Time.timeScale = 1;
        if (_startScreenMusic != null) {
            _targetVolume = _startScreenMusic.volume;
            _startScreenMusic.volume = 0;
            _startScreenMusic.PlayScheduled(AudioSettings.dspTime + _timeBeforeStart);
        }
    }

    void OnDisable() {
        if (_startScreenMusic != null) {
            _startScreenMusic.Stop();
        }
    }

    void Update() {
        if (!MusicStarted()) return;
        if (_elapsedVolumeIncreaseTime > _totalVolumeIncreaseTime) return;
        _elapsedVolumeIncreaseTime += Time.deltaTime;
        if (_elapsedVolumeIncreaseTime < _totalVolumeIncreaseTime)
        {
            _startScreenMusic.volume = _targetVolume * (_elapsedVolumeIncreaseTime / _totalVolumeIncreaseTime);
        } else {
            _startScreenMusic.volume = _targetVolume;
        }
    }

    private bool MusicStarted() {
        if (_elapsedTimeBeforeStart > _timeBeforeStart) return true;
        _elapsedTimeBeforeStart += Time.deltaTime;
        return false;
    }
}
