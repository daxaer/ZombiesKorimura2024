using Audio.Views;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Audio.Controllers
{
    public class AudioChanelContainerController : MonoBehaviour, IAudioChannelContainerController
    {
        IAudioChannelContainerView _audioChannelContainerView;
        private List<AudioSource> _audioChannels;

        public AudioChanelContainerController(IAudioChannelContainerView audioChannelContainerView)
        {
            _audioChannelContainerView = audioChannelContainerView;
            _audioChannels = new List<AudioSource>();
            InitializeChannels();
        }

        private void InitializeChannels()
        {
            for (int i = 0; i < _audioChannelContainerView.AudioChannelsSize; i++)
            {
                var audioChannel = GameObject.Instantiate(_audioChannelContainerView.AudioChannelSource, _audioChannelContainerView.Transform);
                audioChannel.SetActive(true);
                var audioSource = audioChannel.GetComponent<AudioSource>();
                _audioChannels.Add(audioSource);
            }
        }

        public AudioSource GetFirstEmptyChannel => _audioChannels.Find(channel => channel.clip == null || !channel.isPlaying);

        public void DispatchAudio(IAudioConfig audioSettings)
        {
            var channel = GetFirstEmptyChannel;
            channel.clip = audioSettings.AudioClip;
            channel.outputAudioMixerGroup = audioSettings.AudioMixerGroup;
            channel.mute = audioSettings.Mute;
            channel.loop = audioSettings.Loop;
            channel.playOnAwake = audioSettings.PlayOnAwake;
            channel.priority = audioSettings.Priority;
            channel.volume = audioSettings.Volume;
            channel.pitch = audioSettings.Pitch;
            channel.spatialBlend = audioSettings.SpatialBlend;
            channel.Play();
        }

        public void DispatchAudio3D(IAudioConfig audioSettings, Transform transform)
        {
            var channel = GetFirstEmptyChannel;
            channel.clip = audioSettings.AudioClip;
            channel.outputAudioMixerGroup = audioSettings.AudioMixerGroup;
            channel.mute = audioSettings.Mute;
            channel.loop = audioSettings.Loop;
            channel.playOnAwake = audioSettings.PlayOnAwake;
            channel.priority = audioSettings.Priority;
            channel.volume = audioSettings.Volume;
            channel.pitch = audioSettings.Pitch;
            channel.spatialBlend = audioSettings.SpatialBlend;
            channel.transform.position = transform.position;
            channel.Play();
        }
    }
}