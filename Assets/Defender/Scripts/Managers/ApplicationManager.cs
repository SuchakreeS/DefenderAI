using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System;

namespace Defender
{
    public class ApplicationManager : SingletonDontDestroyOnLoad<ApplicationManager>
    {
        public const string SCENE_PRELOAD_NAME = "00.preload";
        public const string SCENE_SPLASH_NAME = "01.splash";
        public const string SCENE_MAIN_MENU_NAME = "02.main.menu";
        private void Start()
        {
            // Preload

            // Start Application
            LoadSceneAsync(SCENE_SPLASH_NAME);
        }

        // ------------------------------------------------------------------------------------
        // Public Function
        public void LoadSceneAsync(string _sceneName)
        {
            SceneManager.LoadSceneAsync(_sceneName);
        }
        // ------------------------------------------------------------------------------------
    }
}
