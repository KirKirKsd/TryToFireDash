using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Music {

    public GameObject viewUI;
    public AudioClip musicClip;

}

public class MusicPlayer : MonoBehaviour {

    public List<GameObject> viewsUI;
    public List<AudioClip> musicClips;
    [HideInInspector] public List<Music> musics = new();

    public AudioSource source;
    public AudioMixer mixer;

    private bool isPaused;
    private bool isLoop;
    private int musicHead;
    
    public List<Sprite> pauseSprites;
    public List<Sprite> loopSprites;

    public Image pauseImageUI;
    public Image loopImageUI;

    public GameObject controlsUI;

    private void Start() {
        if (!PlayerPrefs.HasKey("Volume.Master")) PlayerPrefs.SetFloat("Volume.Master", 1f);
        if (!PlayerPrefs.HasKey("Volume.Music")) PlayerPrefs.SetFloat("Volume.Music", 0.5f);
        if (!PlayerPrefs.HasKey("Volume.SFX")) PlayerPrefs.SetFloat("Volume.SFX", 1f); 
        
        mixer.updateMode = AudioMixerUpdateMode.UnscaledTime;

        mixer.SetFloat("MasterParam", Mathf.Log10(PlayerPrefs.GetFloat("Volume.Master")) * 20);
        mixer.SetFloat("MusicParam", Mathf.Log10(PlayerPrefs.GetFloat("Volume.Music")) * 20);
        mixer.SetFloat("SFXParam", Mathf.Log10(PlayerPrefs.GetFloat("Volume.SFX") * 20));
        
        print("Asd");
        
        for (var i = 0; i < viewsUI.Count; i++) {
            Music temp = new() {
                viewUI = viewsUI[i],
                musicClip = musicClips[i]
            };
            musics.Add(temp);
        }

        musicHead = Random.Range(0, viewsUI.Count);
        
        musics[musicHead].viewUI.SetActive(true);
        source.clip = musics[musicHead].musicClip;
        source.Play();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.L) || (!source.isPlaying && !isPaused)) {
            if (!isLoop) Next();
            else source.Play();
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            Pause();
        }
        
        if (Input.GetKeyDown(KeyCode.J)) {
            Previous();
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            SetLoop();
        }
    }

    public void Next() {
        var previousHead = musicHead;
        musicHead += 1;
        if (musicHead >= musics.Count) musicHead = 0;
        musics[previousHead].viewUI.SetActive(false);
        print(previousHead);
        musics[musicHead].viewUI.SetActive(true);
        source.clip = musics[musicHead].musicClip; 
        source.Play();
    }

    public void Previous() {
        var previousHead = musicHead;
        musicHead -= 1;
        if (musicHead < 0) musicHead = musics.Count - 1;
        musics[previousHead].viewUI.SetActive(false);
        musics[musicHead].viewUI.SetActive(true);
        source.clip = musics[musicHead].musicClip;
        source.Play();
    }

    public void Pause() {
        switch (isPaused) {
            case true:
                source.Play();
                pauseImageUI.sprite = pauseSprites[0];
                break;
            case false:
                source.Pause();
                pauseImageUI.sprite = pauseSprites[1];
                break;
        }

        isPaused = !isPaused;
    }

    public void SetLoop() {
        isLoop = !isLoop;
        switch (isLoop) {
            case true:
                loopImageUI.sprite = loopSprites[1];
                break;
            case false:
                loopImageUI.sprite = loopSprites[0];
                break;
        }
    }
    
}
