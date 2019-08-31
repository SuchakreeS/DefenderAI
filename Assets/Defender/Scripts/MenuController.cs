using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Defender
{
    public class MenuController : MonoBehaviour
    {
        // ----------------------------------------------------------------------
        [SerializeField] Button m_PlayButton;
        [SerializeField] Button m_SettingButton;
        [SerializeField] Button m_AboutUsButton;

        // ----------------------------------------------------------------------
        // Unity Function
        private void Start()
        {
            m_PlayButton.OnClickAsObservable().Subscribe
            (
                _ => 
                {
                    OnPlayButton();
                }
            ).AddTo(this);
        }
        // ----------------------------------------------------------------------
        // Public Function


        // ----------------------------------------------------------------------
        // Private Function
        private void OnPlayButton()
        {
            ApplicationManager.Instance.LoadSceneAsync(ApplicationManager.SCENE_PLAYER_NAME);
        }
        // ----------------------------------------------------------------------
    }
}
