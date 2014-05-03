using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NAudio.Wave;

namespace TryAgain.Sounds
{

    /// <summary>
    /// Stream for looping playback
    /// </summary>
    public class LoopStream : WaveStream
    {
        WaveStream sourceStream;

        /// <summary>
        /// Creates a new Loop stream
        /// </summary>
        /// <param name="sourceStream">The stream to read from. Note: the Read method of this stream should return 0 when it reaches the end
        /// or else we will not loop to the start again.</param>
        public LoopStream(WaveStream sourceStream)
        {
            this.sourceStream = sourceStream;
            this.EnableLooping = true;
        }

        /// <summary>
        /// Use this to turn looping on or off
        /// </summary>
        public bool EnableLooping { get; set; }

        /// <summary>
        /// Return source stream's wave format
        /// </summary>
        public override WaveFormat WaveFormat
        {
            get { return sourceStream.WaveFormat; }
        }

        /// <summary>
        /// LoopStream simply returns
        /// </summary>
        public override long Length
        {
            get { return sourceStream.Length; }
        }

        /// <summary>
        /// LoopStream simply passes on positioning to source stream
        /// </summary>
        public override long Position
        {
            get { return sourceStream.Position; }
            set { sourceStream.Position = value; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;

            while (totalBytesRead < count)
            {
                int bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                if (bytesRead == 0)
                {
                    if (sourceStream.Position == 0 || !EnableLooping)
                    {
                        // something wrong with the source stream
                        break;
                    }
                    // loop
                    sourceStream.Position = 0;
                }
                totalBytesRead += bytesRead;
            }
            return totalBytesRead;
        }
    }

    class Themes
    {
        public volatile static int currentTheme = 0;
        public volatile static bool soundPlaying = true;

        private static String[] themes = 
        {
            "./Content/Themes/loop.wav",
            "./Content/Themes/theme1.wav"
        };

        private static Thread themeThread;

        static void SetTheme(int id)
        {
            currentTheme = id;
        }

        private static void threadedThme()
        {
            int themeid = currentTheme;
            WaveOut waveOut = null;

            while (soundPlaying)
            {
                themeid = currentTheme;
                if (waveOut == null)
                {
                    WaveFileReader reader = new WaveFileReader(themes[themeid]);
                    LoopStream loop = new LoopStream(reader);
                    waveOut = new WaveOut();
                    waveOut.Init(loop);
                    waveOut.Play();
                }
                else
                {
                    waveOut.Stop();
                    waveOut.Dispose();
                    waveOut = null;
                }
                
                while(soundPlaying && (themeid == currentTheme))
                {
                };

                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }


        }

        public static void PlayTheme()
        {
            themeThread = new Thread(threadedThme);
            themeThread.Start();
        }

        public static void Stop()
        {
            soundPlaying = false;
        }
    }
}
