using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _MyAssets.Scripts.Note
{
    /// <summary>
    /// ノートの制御
    /// </summary>
    [RequireComponent(typeof(AudioClip))]
    public class Note: MonoBehaviour
    {
        // ノート再生時のエフェクト
        [SerializeField] private GameObject _effect;
        
        // ノートを配置するタイミング
        private int _moment = 0;

        // 入力した
        private bool _isAimed = false;
        
        // オーディオクリップ
        [SerializeField] private AudioClip _se;
        
        // 経過時間
        private float _time;

        public Transform Transform => transform;

        public Vector3 Position => transform.position;

        public void Start()
        {
            Initialize();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            _isAimed = false;
        }

        public bool CanAim()
        {
            var noteManager = NoteManager.Instance;
            if (!_isAimed)
            {
                if (noteManager.CanHit(_moment))
                {
                    _isAimed = true;
                }
            }

            return _isAimed;
        }
        
        /// <summary>
        /// 削除する
        /// </summary>
        public void Explode() 
        {
            // エフェクトを生成
            var effect = Instantiate(_effect);
            effect.transform.position = transform.position;
            
            // 自身を削除
            Destroy(this.gameObject);
        }
    }
}