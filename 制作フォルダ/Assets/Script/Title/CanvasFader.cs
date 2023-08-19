//  CanvasFader.cs
//  http://kan-kikuchi.hatenablog.com/entry/SceneNavigator
//
//  Created by kan.kikuchi on 2017.05.14.

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CanvasGroup))]
/// <summary>
/// �L�����o�X���t�F�[�h����N���X
/// </summary>
public class CanvasFader : MonoBehaviour
{

    //�t�F�[�h�p�̃L�����o�X�Ƃ��̃A���t�@�l
    private CanvasGroup _canvasGroupEntity;
    private CanvasGroup _canvasGroup
    {
        get
        {
            if (_canvasGroupEntity == null)
            {
                _canvasGroupEntity = GetComponent<CanvasGroup>();
                if (_canvasGroupEntity == null)
                {
                    _canvasGroupEntity = gameObject.AddComponent<CanvasGroup>();
                }
            }
            return _canvasGroupEntity;
        }
    }
    public float Alpha
    {
        get
        {
            return _canvasGroup.alpha;
        }
        set
        {
            _canvasGroup.alpha = value;
        }
    }

    //�t�F�[�h�̏��
    private enum FadeState
    {
        None, FadeIn, FadeOut
    }
    private FadeState _fadeState = FadeState.None;

    //�t�F�[�h���Ă��邩
    public bool IsFading
    {
        get { return _fadeState != FadeState.None; }
    }

    //�t�F�[�h����
    [SerializeField]
    private float _duration;
    public float Duration { get { return _duration; } }

    //�^�C���X�P�[���𖳎����邩
    [SerializeField]
    private bool _ignoreTimeScale = true;

    //�t�F�[�h�I����̃R�[���o�b�N
    private event Action _onFinished = null;

    //=================================================================================
    //�X�V
    //=================================================================================

    private void Update()
    {
        if (!IsFading)
        {
            return;
        }

        float fadeSpeed = 1f / _duration;
        if (_ignoreTimeScale)
        {
            fadeSpeed *= Time.unscaledDeltaTime;
        }
        else
        {
            fadeSpeed *= Time.deltaTime;
        }

        Alpha += fadeSpeed * (_fadeState == FadeState.FadeIn ? 1f : -1f);

        //�t�F�[�h�I������
        if (Alpha > 0 && Alpha < 1)
        {
            return;
        }

        _fadeState = FadeState.None;
        this.enabled = false;

        if (_onFinished != null)
        {
            _onFinished();
        }
    }

    //=================================================================================
    //�J�n
    //=================================================================================

    /// <summary>
    /// �Ώۂ̃I�u�W�F�N�g�̃t�F�[�h���J�n����
    /// </summary>
    public static void Begin(GameObject target, bool isFadeOut, float duration)
    {
        CanvasFader canvasFader = target.GetComponent<CanvasFader>();
        if (canvasFader == null)
        {
            canvasFader = target.AddComponent<CanvasFader>();
        }
        canvasFader.enabled = true;


        canvasFader.Play(isFadeOut, duration);
    }

    /// <summary>
    /// �t�F�[�h���J�n����
    /// </summary>
    public void Play(bool isFadeOut, float duration, bool ignoreTimeScale = true, Action onFinished = null)
    {
        this.enabled = true;

        _ignoreTimeScale = ignoreTimeScale;
        _onFinished = onFinished;

        Alpha = isFadeOut ? 1 : 0;
        _fadeState = isFadeOut ? FadeState.FadeOut : FadeState.FadeIn;

        _duration = duration;
    }

    //=================================================================================
    //��~
    //=================================================================================

    /// <summary>
    /// �t�F�[�h��~
    /// </summary>
    public void Stop()
    {
        _fadeState = FadeState.None;
        this.enabled = false;
    }

}
