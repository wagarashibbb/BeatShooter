using UnityEngine;

namespace _MyAssets.Scripts.Note
{
    public class EndLine: MonoBehaviour
    {
        // 位置Zのキャッシュ
        //private Vector3 _cache;

        [SerializeField] private int _moment = 0;

        void Start()
        {
            var _cache = transform.position;
        
            var noteManager = NoteManager.Instance;

            var ny = NoteSetter.Instance.GetYPosWithMoment(_moment);
            
            transform.position = new Vector3(_cache.x, ny, _cache.z);
        }
    }
}