using System;
using System.Collections.Generic;
using _MyAssets.Scripts.Common;
using _MyAssets.Scripts.Note;
using UniRx;
using UnityEngine;

namespace _MyAssets.Scripts.Player
{
    public class Shooter: MonoBehaviour
    {
        [SerializeField]
        private Laser _laser;
        [SerializeField]
        private Sight _sight;

        private List<NoteBase> _targets = new List<NoteBase>();

        /// <summary>
        /// レイキャストで手前のノートを取得
        /// </summary>
        private NoteBase GetLookingNote()
        {
            var dist = 300;
            var hit = Physics2D.Raycast(transform.position, Vector2.up, dist, LayerMask.GetMask("Note"));
            if (hit.collider)
            {
                return hit.collider.gameObject.GetComponent<NoteBase>();
            }

            return null;
        }
        
        /// <summary>
        /// レイキャストで複数のノートを取得
        /// </summary>
        private List<NoteBase> GetLookingNotes()
        {
            var dist = 300;
            var hits = Physics2D.RaycastAll(transform.position, Vector2.up, dist, LayerMask.GetMask("Note"));

            var notes = new List<NoteBase>();
            
            foreach(var hit in hits)
            {
                if (hit.collider)
                {
                    var note = hit.collider.gameObject.GetComponent<NoteBase>();
                    notes.Add(note);
                }
            }

            return notes;
        }
        
        /// <summary>
        /// ショットする
        /// </summary>
        public void Shot()
        {
            var note = GetLookingNote();
            // レイキャストで手前のノートを取得
            if (note != null)
            {
                ShotAt(note);
            }
        }

        /// <summary>
        /// ショットする
        /// </summary>
        private void ShotAt(NoteBase note)
        {
            // 射撃で爆発
            note.Hit();
        }

        /// <summary>
        /// レーザーを撃つ
        /// </summary>
        public void Laser()
        {
            foreach (var t in _targets.ToArray())
            {
                LaserAt(t);
                _targets.Remove(t);
            }
        }

        /// <summary>
        /// レーザーを撃つ
        /// </summary>
        /// <param name="t">ターゲット</param>
        /// <returns></returns>
        private void LaserAt(NoteBase t)
        {
            // レーザーを生成する
            var l = Instantiate(_laser).GetComponent<Laser>();
            l.Initialize(transform.position);
            l.SetTarget(t);
            var period = l.Period;

            // 発射後にノートを破壊
            Observable.Timer(TimeSpan.FromSeconds(period))
                .Subscribe(_ =>
                {
                    t.Hit();
                });
        }

        /// <summary>
        /// エイムする
        /// </summary>
        public void Aim()
        {
            var notes = GetLookingNotes();
            foreach (var note in notes)
            {
                AimAt(note);
            }
        }

        /// <summary>
        /// エイムする
        /// </summary>
        /// <param name="t">ターゲット</param>
        private void AimAt(NoteBase t)
        {
            if (_targets.Contains(t)) return;
            
            var s = Instantiate(_sight).GetComponent<Sight>();
            s.SetTarget(t);
            _targets.Add(t);
        }
        
        /// <summary>
        /// ショットできる
        /// </summary>
        /// <returns>ショットする/しない</returns>
        public bool CanShot()
        {
            var a = BeatManager.Instance.CurrentMoment;
            return (a % 2 == 0) && BeatManager.Instance.CanHit(a);
        }
        
        /// <summary>
        /// 照準をあわせられる
        /// </summary>
        /// <returns>あわせられる/あわせられない</returns>
        public bool CanAim()
        {
            var a = BeatManager.Instance.CurrentMoment;
            return (a % 2 == 1) && BeatManager.Instance.CanHit(a);
        }
        
        /// <summary>
        /// レーザーを発射できる
        /// </summary>
        /// <returns>できる/できない</returns>
        public bool CanLaser()
        {
            if (_targets == null) return false;
            
            var a = BeatManager.Instance.CurrentMoment % 2;
            return a == 3 && BeatManager.Instance.CanHit(a);
        }
    }
}