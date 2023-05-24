using NAudio.Wave;
using System;

namespace Engine.Audio;

public class AudioFile : IDisposable
{
    public event OnAudioFileStop onStop;
    public WaveOutEvent outputDevice { get; private set; }
    public AudioFileReader fileReader { get; private set; }
    public bool loop;
    /// <summary>
    /// The progress of this audio file from 0 to 1
    /// </summary>
    public float progress
    {
        get => fileReader.Position / (float)fileReader.Length;
        set => fileReader.Position = (long)(value * fileReader.Length);
    }
    public float volume
    {
        get => fileReader.Volume;
        set => fileReader.Volume = value;
    }

    private bool stoppedManually;


    public AudioFile(string filePath)
    {
        fileReader = new(filePath);
        outputDevice = new();
        outputDevice.Init(fileReader);
        outputDevice.PlaybackStopped += PlaybackStopped;
    }

    ~AudioFile()
        => Dispose();


    private void PlaybackStopped(object sender, StoppedEventArgs args)
    {
        onStop(this, stoppedManually);
        stoppedManually = false;
    }


    public void Play()
        => outputDevice.Play();

    public void Stop()
    {
        stoppedManually = true;
        outputDevice.Stop();
    }

    public void StopWithoutNotify()
    {
        stoppedManually = false;
        outputDevice.Stop();
    }

    public void Replay()
    {
        Reset();
        Play();
    }

    public void Reset()
    {
        outputDevice.Stop();
        progress = 0;
    }

    public void Pause()
        => outputDevice.Pause();


    public void Dispose()
    {
        outputDevice.Dispose();
        fileReader.Dispose();
        outputDevice = null;
        fileReader = null;
        GC.SuppressFinalize(this);
    }
}