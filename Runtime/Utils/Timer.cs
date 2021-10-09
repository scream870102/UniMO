using UnityEngine;
#if UNITY_EDITOR
using Scream.UniMO.Editor.Utils;
#endif

namespace Scream.UniMO.Utils
{
    /// <summary>
    /// a countdown timer easy to use not affected by TimeScale
    /// <para>call method Reset to reset timer and call property IsFinshed to check if countdown finished</para>
    /// </summary>
    [System.Serializable]
    public class UnscaledTimer
    {

#if UNITY_EDITOR
        [ReadOnly, SerializeField] float remain = 0f;
        [ReadOnly, SerializeField] bool bFinished = false;
#endif
        float timeSection = 0f;
        float timer = 0f;
        float remainTime = 0f;

        /// <summary> timer is in pause state </summary>
        public bool IsPausing { get; private set; } = false;

        /// <summary>return the cd range from 0 to 1 0 means timer finished </summary>
        public float Remain01 => Remain / timeSection;

        /// <summary>remaining time until the countdown end</summary>
        public float Remain
        {
            get
            {
                float offset = timer - Time.unscaledTime;
                if (IsPausing)
                    return remainTime;
                if (offset >= 0f)
                    return offset;
                return 0f;
            }
        }

        /// <summary>if this countdown finished or not</summary>
        public bool IsFinished
        {
            get
            {
#if UNITY_EDITOR
                bFinished = timer <= Time.unscaledTime;
                remain = Remain;
#endif
                return timer <= Time.unscaledTime && !IsPausing;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="timeSection">time section for this timer</param>
        /// <param name="CanUseFirst">isFinshed equals to true at first or not</param>
        public UnscaledTimer(float timeSection = 0f, bool CanUseFirst = true)
        {
            this.timeSection = timeSection;
            IsPausing = false;
            if (!CanUseFirst)
                Reset();
            else
                timer = 0f;
        }

        /// <summary>Pause this timer</summary>
        public void Pause()
        {
            IsPausing = true;
            remainTime = timer - Time.unscaledTime;
        }

        /// <summary>Resume this timer</summary>
        public void Resume()
        {
            IsPausing = false;
            timer = Time.unscaledTime + remainTime;
        }

        /// <summary>Reset countdown timer with default setting</summary>
        public void Reset()
        {
            timer = Time.unscaledTime + timeSection;
            IsPausing = false;
        }

        /// <summary>
        /// reset countdown timer with new timeSection
        /// </summary>
        /// <param name="timeSection">new time section for this timer</param>
        public void Reset(float timeSection)
        {
            this.timeSection = timeSection;
            timer = Time.unscaledTime + timeSection;
            IsPausing = false;
        }
    }

    /// <summary>
    /// a countdown timer easy to use affected by TimeScale
    /// <para>call method Reset to reset timer and call property IsFinshed to check if countdown finished</para>
    /// </summary>
    [System.Serializable]
    public class ScaledTimer
    {

#if UNITY_EDITOR
        [ReadOnly, SerializeField] float remain = 0f;
        [ReadOnly, SerializeField] bool bFinished = false;
#endif
        float timeSection = 0f;
        float timer = 0f;
        float remainTime = 0f;

        /// <summary> timer is in pause state </summary>
        public bool IsPausing { get; private set; } = false;

        /// <summary>return the cd range from 0 to 1 0 means timer finished </summary>
        public float Remain01 => Remain / timeSection;

        /// <summary>remaining time until the countdown end</summary>
        public float Remain
        {
            get
            {
                float offset = timer - Time.time;
                if (IsPausing)
                    return remainTime;
                if (offset >= 0f)
                    return offset;
                return 0f;
            }
        }

        /// <summary>if this countdown finished or not</summary>
        public bool IsFinished
        {
            get
            {
#if UNITY_EDITOR
                bFinished = timer <= Time.time;
                remain = Remain;
#endif
                return timer <= Time.time && !IsPausing;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="timeSection">time section for this timer</param>
        /// <param name="CanUseFirst">isFinshed equals to true at first or not</param>
        public ScaledTimer(float timeSection = 0f, bool CanUseFirst = true)
        {
            this.timeSection = timeSection;
            IsPausing = false;
            if (!CanUseFirst)
                Reset();
            else
                timer = 0f;
        }

        /// <summary>Pause this timer</summary>
        public void Pause()
        {
            IsPausing = true;
            remainTime = timer - Time.time;
        }

        /// <summary>Resume this timer</summary>
        public void Resume()
        {
            IsPausing = false;
            timer = Time.time + remainTime;
        }

        /// <summary>Reset countdown timer with default setting</summary>
        public void Reset()
        {
            timer = Time.time + timeSection;
            IsPausing = false;
        }

        /// <summary>
        /// reset countdown timer with new timeSection
        /// </summary>
        /// <param name="timeSection">new time section for this timer</param>
        public void Reset(float timeSection)
        {
            this.timeSection = timeSection;
            timer = Time.time + timeSection;
            IsPausing = false;
        }
    }
}