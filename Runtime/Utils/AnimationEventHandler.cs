using System;
using UnityEngine;

namespace Scream.UniMO.Utils
{
    
    /// <summary>
    /// this is a class can add to gameObject then when animation finished you can use animation event to call AnimationFinished
    /// <para>it will broadcast the animation clip it set on animation event who subscribe the event OnAnimationFinshed</para>
    /// </summary>
    [RequireComponent(typeof(Animation))]
    public class AnimationEventHandler : MonoBehaviour
    {
        new Animation animation;
        /// <summary>
        /// Animation component
        /// </summary>
        /// <value>return animation component this gameobject hold</value>
        public Animation Animation { get { return animation; } }

        /// <summary>
        /// this callback will return a animation clip
        /// </summary>
        public event Action<AnimationClip> OnAnimationFinClip;

        /// <summary>
        /// this callback will return a string
        /// </summary>
        public event Action<String> OnAnimationFinString;

        /// <summary>
        /// this callback won't return anything
        /// </summary>
        public event Action OnAnimationFinVoid;
        
        void AnimationFinishedAnim(AnimationClip anim)
        {
            if (OnAnimationFinClip != null)
                OnAnimationFinClip(anim);
        }

        void AnimationFinishedString(string value)
        {
            if (OnAnimationFinString != null)
                OnAnimationFinString(value);
        }

        void AnimationFinishedVoid()
        {
            if (OnAnimationFinVoid != null)
                OnAnimationFinVoid();
        }

        void Awake() => animation = GetComponent<Animation>();
    }
}
