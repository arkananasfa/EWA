using System.Collections.Generic;
using UnityEngine;

public class SoundsExtractor {

    public List<AudioClip> GetSounds(Unit unit, UsableSkill skill) {
        List<AudioClip> clips = new();
        int i = 1;
        while (true) {
            var clip = Resources.Load<AudioClip>($"Sounds/{unit.Code}/{skill.Code}" + (i == 1 ? "" : i.ToString()));
            if (clip == null) break;
            clips.Add(clip);
            i++;
        }
        return clips;
    }

    public List<AudioClip> GetSounds(Unit unit, string audioName) {
        List<AudioClip> clips = new();
        int i = 1;
        while (true) {
            var clip = Resources.Load<AudioClip>($"Sounds/{unit.Code}/{audioName}" + (i == 1 ? "" : i.ToString()));
            if (clip == null) break;
            clips.Add(clip);
            i++;
        }
        return clips;
    }

}