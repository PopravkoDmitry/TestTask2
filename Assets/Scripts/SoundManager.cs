using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private StringAudioClipDictionary _sounds;

    public void PlaySoundAtPoint(string key, Transform position)
    {
        AudioSource.PlayClipAtPoint(_sounds[key], position.position);
    }
}
