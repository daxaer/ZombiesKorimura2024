using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public interface IAudioConfig
    {
        AudioClip AudioClip { get; }
        AudioMixerGroup AudioMixerGroup { get; }
        bool Mute { get; }
        bool Loop { get; }
        bool PlayOnAwake { get; }
        int Priority { get; }
        float Volume { get; }
        float Pitch { get; }
        float SpatialBlend { get; }
    }
}
