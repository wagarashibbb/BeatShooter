using _MyAssets.Scripts.Note;
using Arbor;
using UnityEngine;

namespace _MyAssets.Scripts.Arbor
{
    // 拍数で遷移する
    [AddComponentMenu("MyArbor")]
    [AddBehaviourMenu("Transition/BeatTransition")]
    public class BeatTransition : StateBehaviour
    {
        [SerializeField] private int _measure;
        [SerializeField] private int _moment;

        /// <summary>
        /// 遷移先ステート
        /// </summary>
        [SerializeField] private StateLink _NextState = new StateLink();
        
        void Transition()
        {
            Transition(_NextState);
        }

        public override void OnStateBegin()
        {
            _measure += BeatManager.Instance.CurrentMeasure;
            _moment += BeatManager.Instance.CurrentMoment;
            
            // モーメントの繰り上げ
            _measure += Mathf.FloorToInt((float)_moment / (float)BeatManager.Instance.Beat);
            _moment = _moment % BeatManager.Instance.Beat;
        }

        public override void OnStateUpdate()
        {
            if (_NextState != null)
            {
                var measure = BeatManager.Instance.CurrentMeasure;
                var moment = BeatManager.Instance.CurrentMoment;

                if (measure >= _measure && moment > _moment)
                {
                    Transition();
                }
            }
        }
    }
}