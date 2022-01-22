using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicMgr : BaseManager<MusicMgr>
{
    //唯一的背景音乐组件
    private AudioSource bkMusic = null;

    //音效列表
    private GameObject soundObj = null;
    private List<AudioSource> soundList = new List<AudioSource>();


    //音效和背景音量大小
    private float bkValue = 0.3f;
    private float soundValue = 1;
    

    public MusicMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(Update);
    }
    //检测音效是否播放完
    private void Update()
    {
        for(int i = soundList.Count-1 ; i>=0 ; i--)
        {
            
            if(!soundList[i].isPlaying)
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
            
        }
    }


    /// <summary>
    /// 播放背景音乐
    /// </summary>
    // Start is called before the first frame update
    public void PlayBkMusic(string name)
    {
        if(bkMusic == null)
        {
            //新建空对象
            GameObject obj = new GameObject();
            MonoMgr.GetInstance().AddDontDestroyOnLoad(obj);
            obj.name = "BkMusic";
            bkMusic = obj.AddComponent<AudioSource>();
        }
        //异步加载背景音乐
        ResMgr.GetInstance().LoadSync<AudioClip>("Music/BK/" + name, (clip)=> {
            bkMusic.clip = clip;
            bkMusic.volume = bkValue;
            bkMusic.loop = true;
            bkMusic.Play();
        }) ;
    }
    /// <summary>
    /// 改变背景音乐大小
    /// </summary>
    public void ChangeBKValue(float v)
    {
        bkValue = v;
        if (bkMusic == null)
            return;
        bkMusic.volume = bkValue;
    }
    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public  void PauseBKMusic()
    {
        if (bkMusic == null)
            return;
        bkMusic.Pause();
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopBKMusic()
    {
        if (bkMusic == null)
            return;
        bkMusic.Stop();
    }
    /// <summary>
    /// 播放音效
    /// 因为需要得到对应的音效切片 所以需要用委托
    /// </summary>
    public void PlaySound(string name, bool isLoop, UnityAction<AudioSource> callback = null)
    {
        if(soundObj == null)
        {
            soundObj = new GameObject();
            MonoMgr.GetInstance().AddDontDestroyOnLoad(soundObj);
            soundObj.name = "Sound";
        }

        //当音效资源异步加载结束后，再添加一个音效
        ResMgr.GetInstance().LoadSync<AudioClip>("Music/Sound/" + name, (clip) => {
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = isLoop;
            source.volume = soundValue;
            source.Play();
            soundList.Add(source);
            if (callback != null)
                callback(source);
        });


    }
    /// <summary>
    /// 改变声效音量大小
    /// </summary>
    /// <param name="value"></param>
    public void ChangeSoundValue(float value)
    {
        soundValue = value;
        for(int i=0;i<soundList.Count;i++)
        {
            soundList[i].volume = value;
        }
    }

    /// <summary>
    /// 停止播放音效
    /// </summary>
    /// <param name="source"></param>
    public void StopSound(AudioSource source)
    {
        //如果有这个音乐
        if(soundList.Contains(source))
        {
            //移除
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }

}
