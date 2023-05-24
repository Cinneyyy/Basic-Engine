using System.Collections.Generic;

namespace Engine.Audio;

public static class AudioPlayer
{
    public static readonly Dictionary<string, AudioFile> loadedAudios = new();


    private static void OnAudioFileStop(AudioFile file, bool wasManualStop)
    {
        if (wasManualStop || !file.loop)
            return;

        file.progress = 0;
        file.Play();
    }


    public static AudioFile LoadFile(string filePath, string id)
    {
        var audio = new AudioFile(filePath);
        loadedAudios.Add(id, audio);
        audio.onStop += OnAudioFileStop;
        return audio;
    }

    public static void UnloadFile(string id)
    {
        var file = loadedAudios[id];
        loadedAudios.Remove(id);
        file.Dispose();
    }

    public static AudioFile GetFile(string id)
        => loadedAudios[id];

    public static void Play(string id)
        => loadedAudios[id].Play();

    public static void Stop(string id)
        => loadedAudios[id].Stop();

    public static void Pause(string id)
        => loadedAudios[id].Pause();

    public static void Replay(string id)
        => loadedAudios[id].Replay();
}