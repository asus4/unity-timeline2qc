using System;
using System.Net;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;

using UnityOSC;

namespace AppKit
{
    [Serializable]
    public class VideoPreview : PlayableAsset
    {
        [Range(0, IPEndPoint.MaxPort)]
        public int port = 3333;
        public ExposedReference<VideoPlayer> player;

        OSCClient client;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            if (client == null)
            {
                client = new OSCClient(IPAddress.Parse("127.0.0.1"), port);
            }

            var template = new AudioPreviewPlayable();
            template.player = player.Resolve(graph.GetResolver());
            template.client = client;
            return ScriptPlayable<AudioPreviewPlayable>.Create(graph, template);
        }
    }

    [Serializable]
    public class AudioPreviewPlayable : PlayableBehaviour
    {
        public VideoPlayer player;
        public OSCClient client;

        public override void OnGraphStart(Playable playable)
        {
        }
        public override void OnGraphStop(Playable playable)
        {
        }

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (Application.isPlaying)
            {
                player.Play();
            }
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
        }

        public override void PrepareFrame(Playable playable, FrameData info)
        {
            double t = playable.GetTime();
            client.Send(new OSCMessage("/time", (float)t));
        }
    }
}