using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public static LevelTimer Instance;
    private float startTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnStart() {
        StartTimer();
    }

    private void Start()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        startTime = Time.time;
    }

    public float GetElapsedTime()
    {
        return Time.time - startTime;
    }

    void OnEnable() {
        GameEvents.levelStarted += OnStart;
    }


    void OnDisable() {
        GameEvents.levelStarted -= OnStart;
    }
}