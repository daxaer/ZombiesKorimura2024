using Audio;
using Audio.Controllers;
using Audio.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioChanelContainerController _audioChanelContainerController;

    public void SetAudioChannel(AudioChanelContainerController audioChanelContainerController)
    {
        _audioChanelContainerController = audioChanelContainerController;
    }

    public void PlayAudio3D(AudioConfig audio, Transform transform)
    {
        _audioChanelContainerController.DispatchAudio3D(audio, transform);
    }

    public void PlayAudio2D(AudioConfig audio)
    {
        _audioChanelContainerController.DispatchAudio(audio);
    }
}
