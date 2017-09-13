using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Video;
using UnityOSC;



namespace AppKit
{
    [ExecuteInEditMode, RequireComponent(typeof(VideoPlayer))]
    public class TimelineVideo : MonoBehaviour, ITimeControl
    {
        [SerializeField]
        int port = 3333;

        VideoPlayer player;
        OSCClient client;

        void OnEnable()
        {
            player = GetComponent<VideoPlayer>();
            client = new OSCClient(IPAddress.Parse("127.0.0.1"), port);
        }

        void OnDisable()
        {
            client.Close();
        }

        #region IPlayable

        public void OnControlTimeStart()
        {
        }

        public void OnControlTimeStop()
        {
        }

        public void SetTime(double time)
        {
            Debug.LogFormat("settime: {0}", time);
            client.Send(new OSCMessage("/time", (float)time));
        }

        #endregion // IPlayable
    }

}