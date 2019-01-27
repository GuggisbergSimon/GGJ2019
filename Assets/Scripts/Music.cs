using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [FMODUnity.EventRef] [SerializeField] private string eventRefFoot;
    private FMOD.Studio.EventInstance footstep;
    private FMOD.Studio.ParameterInstance end; // 0 = init, 1 = walk, 1.5 = end, 2 = echo
    private FMOD.Studio.ParameterInstance volumeFoot;
    [SerializeField] private float volumeSoundFoot = 100; // from 0 to 100
    [SerializeField] private float fury = 100; // from 0 to 100

    private void Awake()
    {
        footstep = FMODUnity.RuntimeManager.CreateInstance(eventRefFoot);
        footstep.getParameter("Fury", out end);
        footstep.getParameter("Volume", out volumeFoot);
    }

    // Start is called before the first frame update
    void Start()
    {
        footstep.start();
        volumeFoot.setValue(volumeSoundFoot * (GameManager.Instance.VolumeMaster / 100) * (GameManager.Instance.VolumeMusic / 100));
    }

    // Update is called once per frame
    void Update()
    {
        end.setValue(GameManager.Instance.UIManager.FuryGauge.Fury/100f);
    }
}
