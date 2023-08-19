//  SceneNavigator.cs
//  http://kan-kikuchi.hatenablog.com/entry/SceneNavigator
//
//  Created by kan.kikuchi on 2017.05.14.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// �V�[���̑J�ڂ����s�A�Ǘ�����N���X
/// </summary>
public class SceneNavigator : SingletonMonoBehaviour<SceneNavigator>
{

    //�t�F�[�h�����ۂ�
    public bool IsFading
    {
        get { return _fader.IsFading || _fader.Alpha != 0; }
    }

    //��O�ƌ��݁A���̃V�[����
    private string _beforeSceneName = "";
    public string BeforeSceneName
    {
        get { return _beforeSceneName; }
    }

    private string _currentSceneName = "";
    public string CurrentSceneName
    {
        get { return _currentSceneName; }
    }

    private string _nextSceneName = "";
    public string NextSceneName
    {
        get { return _nextSceneName; }
    }

    //�t�F�[�h��̃C�x���g
    public event Action FadeOutFinished = delegate { };
    public event Action FadeInFinished = delegate { };

    //�t�F�[�h�p�N���X
    [SerializeField]
    private CanvasFader _fader = null;

    //�t�F�[�h����
    public const float FADE_TIME = 0.5f;
    private float _fadeTime = FADE_TIME;

    //=================================================================================
    //������
    //=================================================================================

    /// <summary>
    /// ������(Awake�������̑O�̏��A�N�Z�X���A�ǂ��炩�̈�x�����s���Ȃ�)
    /// </summary>
    protected override void Init()
    {
        base.Init();

        //���@���G�f�B�^�����s���Ă��鎞�ɂ�Add�����ꍇ��Reset�����s����Ȃ��̂ŁAInit������s
        if (_fader == null)
        {
            Reset();
        }

        //�ŏ��̃V�[�����ݒ�
        _currentSceneName = SceneManager.GetSceneAt(0).name;

        //�i�������A�t�F�[�h�p�̃L�����o�X���\����
        DontDestroyOnLoad(gameObject);
        _fader.gameObject.SetActive(false);
    }

    //�R���|�[�l���g�ǉ����Ɏ����Ŏ��s�����(���@���G�f�B�^�����s���Ă��鎞�ɂ͓��삵�Ȃ�)
    private void Reset()
    {
        //�I�u�W�F�N�g�̖��O��ݒ�
        gameObject.name = "SceneNavigator";

        //�t�F�[�h�p�̃L�����o�X�쐬
        GameObject fadeCanvas = new GameObject("FadeCanvas");
        fadeCanvas.transform.SetParent(transform);
        fadeCanvas.SetActive(false);

        Canvas canvas = fadeCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;

        fadeCanvas.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        fadeCanvas.AddComponent<GraphicRaycaster>();
        _fader = fadeCanvas.AddComponent<CanvasFader>();
        _fader.Alpha = 0;

        //�t�F�[�h�p�̉摜�쐬
        GameObject imageObject = new GameObject("Image");
        imageObject.transform.SetParent(fadeCanvas.transform, false);
        imageObject.AddComponent<Image>().color = Color.black;
        imageObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2000, 2000);
    }

    //=================================================================================
    //�V�[���̕ύX
    //=================================================================================

    /// <summary>
    /// �V�[���̕ύX
    /// </summary>
    public void Change(string sceneName, float fadeTime = FADE_TIME)
    {
        if (IsFading)
        {
            Debug.LogError("�t�F�[�h���ł��I");
            return;
        }

        //���̃V�[�����ƃt�F�[�h���Ԃ�ݒ�
        _nextSceneName = sceneName;
        _fadeTime = fadeTime;

        //�t�F�[�h�A�E�g
        _fader.gameObject.SetActive(true);
        _fader.Play(isFadeOut: false, duration: _fadeTime, onFinished: OnFadeOutFinish);
    }

    //�t�F�[�h�A�E�g�I��
    private void OnFadeOutFinish()
    {
        FadeOutFinished();

        //�V�[���ǂݍ��݁A�ύX
        SceneManager.LoadScene(_nextSceneName);

        //�V�[�����X�V
        _beforeSceneName = _currentSceneName;
        _currentSceneName = _nextSceneName;

        //�t�F�[�h�C���J�n
        _fader.gameObject.SetActive(true);
        _fader.Alpha = 1;
        _fader.Play(isFadeOut: true, duration: _fadeTime, onFinished: OnFadeInFinish);
    }

    //�t�F�[�h�C���I��
    private void OnFadeInFinish()
    {
        _fader.gameObject.SetActive(false);
        FadeInFinished();
    }

}
