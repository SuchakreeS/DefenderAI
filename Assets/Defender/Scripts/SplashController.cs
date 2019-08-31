using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Defender
{
    public class SplashController : MonoBehaviour
    {
        private Animator _Animator;
        private void Start()
        {
            _Animator = GetComponent<Animator>();
            Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe
            (
                _ =>
                {
                    _Animator.SetTrigger("Trigger.Fade.In");
                }
            ).AddTo(this);
        }
        public void OnFadeInStartAnimation()
        {

        }
        public void OnFadeInEndAnimation()
        {
            Observable.Timer(TimeSpan.FromSeconds(2.5)).Subscribe
            (
                _ =>
                {
                    _Animator.SetTrigger("Trigger.Fade.Out");
                }
            ).AddTo(this);
        }
        public void OnFadeOutStartAnimation()
        {

        }
        public void OnFadeOutEndAnimation()
        {
            ApplicationManager.Instance.LoadSceneAsync(ApplicationManager.SCENE_MAIN_MENU_NAME);
        }
    }
}
