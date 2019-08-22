using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Defender
{
    [RequireComponent(typeof(Animator))]
    public class ShieldAnimation : MonoBehaviour
    {
        private const string _ANIMATION_FADE_IN = "Shield_Fade_In";
        private const string _ANIMATION_FADE_OUT = "Shield_Fade_Out";
        private const string _TRIGGER_FADE_IN = "Trigger.Fade.In";
        private const string _TRIGGER_FADE_OUT = "Trigger.Fade.Out";
        private Dictionary<string, int> triggerDictionary = new Dictionary<string, int>();
        private DefenderArena arena;
        private TeamType team;
        private Animator animator;
        private bool isSwitching;
        private IDisposable switchShieldDisposable;
        // ----------------------------------------------------------------------------------
        public bool IsOpen;
        public float speed;
        // ----------------------------------------------------------------------------------
        public void Init()
        {
            animator = GetComponent<Animator>();
            isSwitching = false;
            IsOpen = GetComponent<Collider>().enabled;
            
        }
        public void SwitchShield()
        {
            if(!isSwitching)
            {
                isSwitching = true;
                IsOpen = !IsOpen;
                SetTrigger(IsOpen ? _TRIGGER_FADE_IN : _TRIGGER_FADE_OUT);
            }
        }
        private void SetTrigger(string _trigger)
        {
            if(!triggerDictionary.ContainsKey(_trigger))
            {
                triggerDictionary.Add(_trigger, Animator.StringToHash(_trigger));
            }
            animator.SetTrigger(triggerDictionary[_trigger]);
        }
        private void OnSwitchingStart()
        {
            
            GetComponent<Collider>().enabled = IsOpen;
            
        }
        private void OnSwitchingEnd()
        {
            isSwitching = false;
            GetComponent<Collider>().enabled = IsOpen;
            
        }
    }
}
