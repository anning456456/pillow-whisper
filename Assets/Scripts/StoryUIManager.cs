using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Video;

public class StoryUIManager : MonoBehaviour
{
    // 用于存储所有Panel的列表
    public GameObject[] PanelList;
    // 当前显示的Panel索引
    private int currentPanelIndex = 0;

    // 计时器，用于自动切换
    private float panelTimer = 0f;
    // 自动切换的时间（秒）
    public float autoSwitchTime = 3f;

    public Button pillowBtn;

    // 新增：Video相关
    public GameObject videoPanel; // 用于显示Video的UI面板
    public VideoPlayer videoPlayer; // VideoPlayer组件
    private bool isPlayingVideo = false; // 是否正在播放视频

    void Start()
    {
        // 初始化时只显示第一个Panel，隐藏其他Panel
        ShowPanel(0);
        panelTimer = 0f;
        pillowBtn.onClick.AddListener(JumpToScene);

        // 初始化VideoPanel为隐藏
        if (videoPanel != null)
            videoPanel.SetActive(false);

        // 注册视频播放完成事件
        if (videoPlayer != null)
            videoPlayer.loopPointReached += OnVideoFinished;
    }

    void Update()
    {
        // 如果正在播放视频，禁止切换
        if (isPlayingVideo) return;

        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            ShowNextPanel();
            panelTimer = 0f; // 重置计时器
        }
        else
        {
            // 如果没有点击，计时
            panelTimer += Time.deltaTime;
            if (panelTimer >= autoSwitchTime)
            {
                ShowNextPanel();
                panelTimer = 0f; // 切换后重置计时器
            }
        }
    }

    // 显示指定索引的Panel，隐藏其他Panel
    private void ShowPanel(int index)
    {
        if (PanelList == null || PanelList.Length == 0) return;

        for (int i = 0; i < PanelList.Length; i++)
        {
            if (PanelList[i] != null)
                PanelList[i].SetActive(i == index);
        }
        currentPanelIndex = index;
    }

    // 显示下一个Panel
    private void ShowNextPanel()
    {
        if (PanelList == null || PanelList.Length == 0) return;

        // 在第二个Panel（index==1）切到第三个Panel（index==2）之间插入视频
        if (currentPanelIndex == 1)
        {
            PlayVideoBetweenPanels();
            return;
        }

        if (currentPanelIndex < PanelList.Length - 1)
        {
            ShowPanel(currentPanelIndex + 1);
        }
        else
        {
            // 使用DoTween给pillowBtn做一个放缩动画
            if (pillowBtn != null)
            {
                pillowBtn.transform.DOKill();
                pillowBtn.transform.localScale = Vector3.one;
                pillowBtn.transform.DOScale(1.2f, 0.2f)
                    .SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        pillowBtn.transform.DOScale(1f, 0.2f).SetEase(Ease.InBack);
                    });
            }
            // 如果已经是最后一个Panel，则跳转到GameScene场景
            // UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
    }

    // 播放Panel2和Panel3之间的视频
    private void PlayVideoBetweenPanels()
    {
        if (videoPanel != null && videoPlayer != null)
        {
            isPlayingVideo = true;
            videoPanel.SetActive(true);
            videoPlayer.Stop();
            videoPlayer.Play();
        }
        else
        {
            // 如果没有设置视频，直接跳到下一个Panel
            ShowPanel(currentPanelIndex + 1);
        }
    }

    // 视频播放完成回调
    private void OnVideoFinished(VideoPlayer vp)
    {
        if (videoPanel != null)
            videoPanel.SetActive(false);

        isPlayingVideo = false;
        ShowPanel(currentPanelIndex + 1);
    }

    // 跳转到指定场景的方法
    public void JumpToScene()
    {
       UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
