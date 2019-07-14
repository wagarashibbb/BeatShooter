using _MyAssets.Scripts.Base;

namespace _MyAssets.Scripts.Note
{
    public class NoteSetter: SingletonMonoBehaviour<NoteSetter>
    {
        /// <summary>
        /// 位置を決める
        /// </summary>
        /// <param name="moment"></param>
        /// <returns></returns>
        public float GetYPosWithMoment(float moment)
        {
            var noteScreen = NoteScreen.Instance;
            var noteManager = NoteManager.Instance;
            
            // 縦幅を取得
            var h = noteScreen.Height;
            var k = noteScreen.K;
            
            // 位置を計算
            var m = noteManager.StartMoment + moment;
            var ny = GetYPosWithTime(m * noteManager.DurationPerBeat);

            return ny;
        }
        
        public float GetYPosWithTime(float time)
        {
            var noteScreen = NoteScreen.Instance;
            var noteManager = NoteManager.Instance;
            
            // 縦幅を取得
            var h = noteScreen.Height;
            var k = noteScreen.K;
            
            var t = time / noteManager.Duration;
            
            // 位置を計算
            var ny = h * t * k;

            return ny;
        }

        public float GetYPos()
        {
            var noteManager = NoteManager.Instance;
            
            return GetYPosWithTime(noteManager.CurrentTime);
        }
    }
}