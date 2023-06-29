using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class OneVideoPlayer : MonoBehaviour
{
    string extension = ".mp4";
    public TextMeshProUGUI tx;
    [SerializeField]
    private VideoPlayer videoPlayer;
    [SerializeField]
    private RenderTexture rt;
    [SerializeField]
    private VideoClip loopVideo;
    [SerializeField]
    private VideoClip[] videos;

    private int videoCnt = 10;

    private bool isQuiz = false;

    public float transitionDuration = 1.0f;

    public RawImage rawImage;

    public Image fadeimage;

    LoadFileManager loadFileManager;

    public string s_path;
    string[] v_url;

    public Toggle fadeToggle;
    bool isfade = true;
    // Start is called before the first frame update
    void Start()
    {
        loadFileManager = GetComponent<LoadFileManager>();

        GameObject camera = GameObject.Find("Main Camera");
        videoPlayer = camera.AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;

        //스트리밍 경로 비디오 파일개수
        //videoCnt = GetDirecotoryCount(s_path);
        //Debug.Log("dir cnt:" + videoCnt);

        v_url = new string[videoCnt];

        s_path = Application.streamingAssetsPath;

        v_url[0] = s_path + "/loop"+ extension;
        v_url[1] = s_path + "/v1"+ extension;
        v_url[2] = s_path + "/v2"+ extension;
        v_url[3] = s_path + "/v3" + extension;
        v_url[4] = s_path + "/v4" + extension;
        v_url[5] = s_path + "/v5" + extension;
        v_url[6] = s_path + "/v6"+ extension;
        v_url[7] = s_path + "/v7" + extension;
        v_url[8] = s_path + "/v8" + extension;
        v_url[9] = s_path + "/v9" + extension;

        videoPlayer.source = VideoSource.Url;

        videoPlayer.targetTexture = rt;
        loopVideoPlay(videoPlayer);

        //videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer source)
    {
        isQuiz = false;
        videoPlayer.Pause();
        if(videoPlayer.url != v_url[0])
        {
            PlayFade();
        }
        videoPlayer.url = v_url[0];
        Invoke("PlayClip", .5f);
    }

    // Update is called once per frame
    void Update()
    {
   
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayRandomClip();
        }
        if (Input.GetKeyDown(KeyCode.Keypad0)|| Input.GetKeyDown(KeyCode.Alpha0))
        {
            loopVideoPlay(videoPlayer);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayFixedClip(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayFixedClip(2);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayFixedClip(3);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayFixedClip(4);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayFixedClip(5);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6))
        {
            PlayFixedClip(6);
        }
        if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7))
        {
            PlayFixedClip(7);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8))
        {
            PlayFixedClip(8);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9))
        {
            PlayFixedClip(9);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isfade)
            {
                isfade = false;
            }
            else
            {
                isfade = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (videoPlayer.isPaused)
            {
                videoPlayer.Play();
            }
            else
            {
                videoPlayer.Pause();
            }
        }

    }
    public int GetDirecotoryCount(string filePath)
    {
        int count = 0;
        try
        {
            if (System.IO.Directory.Exists(filePath))
            {
                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(filePath);
                count = directoryInfo.GetFiles("*.*", System.IO.SearchOption.AllDirectories).Length;
            }
        }
        catch
        {; }
        return count;
    }

    void loopVideoPlay(VideoPlayer v)
    {
        v.Pause();
        v.url = v_url[0];
        v.isLooping = true;
        v.Play();
    }

    public void PlayLoop()
    {
        isQuiz = false;
        videoPlayer.Pause();
        PlayFade();
        videoPlayer.url = v_url[0];
        Invoke("PlayClip", .5f);
    }
    public void PlayRandomClip()
    {
        if (!isQuiz)
        {
            
            videoPlayer.Pause();
            PlayFade();
            int randCnt = Random.Range(1, videoCnt);
            //videoPlayer.clip = videos[randCnt];
            tx.text = "Play Video:"+v_url[randCnt];

            videoPlayer.url = v_url[randCnt];
            videoPlayer.isLooping = true;
            isQuiz = true;
            Invoke("PlayClip",1f);

        }
        else
        {
            Debug.Log("퀴즈중입니다.");
        }
    }
    public void PlayFixedClip(int index)
    {
        videoPlayer.Pause();
        PlayFade();
        //videoPlayer.clip = videos[index];
        tx.text = "Play Video:" + v_url[index];
        videoPlayer.url = v_url[index];

        Invoke("PlayClip", .5f);
    }

    public void PauseClip()
    {
        videoPlayer.Pause();
    }
    public void PlayClip()
    {
        videoPlayer.Play();
    }

    //

    public void PlayFade() { if (isfade) { StartCoroutine(FadeCoroutine()); } }
    IEnumerator FadeCoroutine()
    {
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.05f;
            yield return new WaitForSeconds(0.01f);
            fadeimage.color = new Color(1, 1, 1, fadeCount);
        }
        while (fadeCount > 0f)
        {
            fadeCount -= 0.05f;
            yield return new WaitForSeconds(0.01f);
            fadeimage.color = new Color(1, 1,1, fadeCount);
        }
    }
}
