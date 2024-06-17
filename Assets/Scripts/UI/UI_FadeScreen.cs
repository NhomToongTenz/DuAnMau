using System;
using UnityEngine;

namespace UI
{
    public class UI_FadeScreen : MonoBehaviour
    {
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void FadeIn() => anim.SetTrigger("fadeIn");
        public void FadeOut() => anim.SetTrigger("fadeOut");
    }
}