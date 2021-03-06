using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // ?????? ????ȭ
public class Sound
{
    public string name; //???? ?̸?
    public AudioClip clip; // ??
}
public class AudioManager : Singleton<AudioManager>
{
    public AudioSource[] audioSources;
    public Sound[] soundClass; 


    private void Awake()
    {
        base.Awake();
    }

    public void SetVolume(float volume)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = volume;
        }
    }


    public void PlaySE(string _name, int audioSourceNum)
    {
        for(int i = 0;i< soundClass.Length;i++)
        {
            if(_name == soundClass[i].name)
            {
                audioSources[audioSourceNum].clip = soundClass[i].clip;
                audioSources[audioSourceNum].Play();

                    
            }
        }
    }
    public void StopSE(int audioSourceNum)
    {
         audioSources[audioSourceNum].Stop();
    }

   
    

}
