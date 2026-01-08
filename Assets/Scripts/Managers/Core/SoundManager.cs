using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager 
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();






    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound)); //Sound Enum값의 이름을 가진 배열을 순서대로 soundNames에 넣어줌 이때 MaxCount(sound갯수 체크용)도 포함됨
            //[Bgm, Effect, MaxCount]

            for (int i = 0; i < soundNames.Length -1; i++) // 위에서 포함된 MaxCount를 빼고 실행하기 위해 -1을 해줌
            {
                GameObject go = new GameObject { name = soundNames[i] }; //soundNames의 이름을 가진 새로운 게임 오브젝트를 만들어줌
                _audioSources[i] = go.AddComponent<AudioSource>(); //위에서 생성한 게임 오브젝트에 AudioSource컴포넌트를 붙여서 그 오브젝트 자체를 _audioSources[i]에 넣어줌
                go.transform.parent = root.transform; //Bgm, Effect게임 오브젝트를 @Sound 산하로 내림
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true; //Bgm만 루프로 무한 재생하게 해줌
        }
    }
    public void Clear()
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }
    
    public void Play( string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch );
    }


    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {

        if (audioClip == null)
            return;

        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }

    }

    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (type == Define.Sound.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {
            
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing! {path}");

        return audioClip;
    }
}
