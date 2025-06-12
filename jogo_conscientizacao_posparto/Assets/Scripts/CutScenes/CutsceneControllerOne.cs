using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneControllerOne : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string mainSceneName = "InitialScene";

    void Start()
    {
        // Se não foi arrastado no Inspector:
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        // Inscreve no evento de fim do vídeo:
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Cutscene terminou! Indo pra cena principal...");
        SceneManager.LoadScene(mainSceneName);
    }
}
