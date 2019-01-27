using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [FMODUnity.EventRef] [SerializeField] private string eventRefMusic;
    private FMOD.Studio.EventInstance music;
    private FMOD.Studio.ParameterInstance furySound; // 0 = init, 1 = walk, 1.5 = end, 2 = echo
    private FMOD.Studio.ParameterInstance volumeMusic;
    [SerializeField] private float volumeSoundMusic = 100; // from 0 to 100
    [SerializeField] private float fury = 100; // from 0 to 100


    private void Awake()
    {
        music = FMODUnity.RuntimeManager.CreateInstance(eventRefMusic);
        music.getParameter("Fury", out furySound);
        music.getParameter("Volume", out volumeMusic);
    }

    // Start is called before the first frame update
    void Start()
    {
        music.start();
        volumeMusic.setValue(volumeSoundMusic * (GameManager.Instance.VolumeMaster / 100) * (GameManager.Instance.VolumeMusic / 100));
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(music, transform, GetComponent<Rigidbody2D>());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.UIManager)
        {
            furySound.setValue(GameManager.Instance.UIManager.FuryGauge.Fury / 100f);
        }
        else
        {
            furySound.setValue(fury / 100);
            volumeMusic.setValue(volumeSoundMusic * (GameManager.Instance.VolumeMaster / 100) * (GameManager.Instance.VolumeMusic / 100));
        }
    }

    private void OnDestroy()
    {
        music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
