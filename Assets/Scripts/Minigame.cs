using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    [SerializeField] protected AudioClip _music;
    [SerializeField] protected float _maxVolume = 0.5f;
    [HideInInspector] public float TargetMusicVolume = 0f;
    protected AudioSource MusicSource;

    protected virtual float MinigameTime => GameManager.Instance ? GameManager.Instance.MinigameTime : 10f;

    public bool IsRunning { get; protected set; } = false;
    public bool IsFinished { get; protected set; } = false;
    protected Timer Timer;

    protected virtual string MinigameName { get; } = "Minigame";

    protected virtual void Start()
    {
        Timer = FindObjectOfType<Timer>();
        if (!Timer)
        {
            Debug.LogError($"No timer in minigame");
            return;
        }

        if (GameManager.Instance && GameManager.Instance.CurrentMinigame == null)
            GameManager.Instance.CurrentMinigame = this;
    }

    public void StartMinigame()
    {
        Debug.Log("Starting minigame");
        Timer.StartTimer(MinigameTime);
        IsRunning = true;

        MusicSource = GetComponent<AudioSource>();
        if (!MusicSource)
            MusicSource = gameObject.AddComponent<AudioSource>();

        MusicSource.clip = _music;
        // if (GameManager.Instance)
        //     MusicSource.pitch = GameManager.Instance.MusicModifier; // TODO 07/10: change pitch based on game speed
        MusicSource.loop = true;
        MusicSource.Play();
        TargetMusicVolume = _maxVolume;
    }

    private void FixedUpdate()
    {
        if (MusicSource)
            MusicSource.volume = Mathf.Lerp(MusicSource.volume, TargetMusicVolume, Time.fixedDeltaTime * 4f);
    }

    public virtual void EndMinigame(bool won = true)
    {
        if (!IsRunning)
            return;

        IsRunning = false;
        Debug.Log($"Minigame ended, player {(won ? "won" : "lost")}");

        Timer.StopTimer();

        IsFinished = true;

        if (GameManager.Instance)
            GameManager.Instance.FinishMinigame(won);

        AudioClip clip = Resources.Load<AudioClip>(won ? "win" : "fail");
        if (clip)
        {
            AudioSource a = gameObject.AddComponent<AudioSource>();
            a.clip = clip;
            a.Play();
        }
    }
}