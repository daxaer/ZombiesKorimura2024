

using System.Collections.Generic;
using UnityEngine;

namespace Audio.Views
{
    public interface IAudioChannelContainerView
    {
        int AudioChannelsSize { get; }
        GameObject AudioChannelSource { get; }
        Transform Transform { get; }
    }
}
