using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioConfig", menuName = "AudioManager/AudioConfig", order = 1)]
public class AudioConfig : ScriptableObject, IAudioConfig
{
    public AudioClip AudioClip => _audioClip;
    public AudioMixerGroup AudioMixerGroup => _audioMixerGroup;
    public bool Mute => _mute;
    public bool Loop => _loop;
    public bool PlayOnAwake => _playOnAwake;
    public int Priority => _priority;
    public float Volume => _volume;
    public float Pitch => _pitch;
    public float SpatialBlend => _spatialBlend;

    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private bool _mute;
    [SerializeField] private bool _loop;
    [SerializeField] private bool _playOnAwake;
    [SerializeField, Range(0, 256)] private int _priority;
    [SerializeField, Range(0,1)] private float _volume;
    [SerializeField, Range(-3,3)] private float _pitch;
    [SerializeField, Range(0, 1)] private float _spatialBlend;
} 
