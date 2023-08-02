using UnityEngine;

public class AudioManager {

    public float MusicVolume;
    public float SoundVolume;

    public void PlaySound (AudioSource source) {
        source.volume = SoundVolume;
        source.loop = false;
        source.Play();
    }

}

public static class AudioSorceExtensions {

    public static void PlaySoundThroughManager(this AudioSource source) {
        Game.AudioManager.PlaySound(source);
    }

}