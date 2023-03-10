using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class TVManager : MonoBehaviour
{
    public static TVManager Instance;

    [SerializeField] private GameObject Panel_UI;
    [SerializeField] private GameObject Panel_Video;
    [SerializeField] private VideoPlayer PlayerVideo;

    [SerializeField] private List<VideoClip> _videos = new();
    
    private TextMeshProUGUI _panelUIText;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Panel_Video.SetActive(false);
        Panel_UI.SetActive(true);

        _panelUIText = Panel_UI.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ActivatePanelVideo()
    {
        Panel_UI.SetActive(false);

        Panel_Video.SetActive(true);
    }

    public void ActivatePanelUI()
    {
        Panel_Video.SetActive(false);
        Panel_UI.SetActive(true);
    }

    public void ChangeTextPanelUI(string newText)
    {
        _panelUIText.text = newText;
    }

    public void PlayVideo(int indexVideo)
    {
        PlayerVideo.clip = _videos[indexVideo];
        PlayerVideo.Play();
    }
}