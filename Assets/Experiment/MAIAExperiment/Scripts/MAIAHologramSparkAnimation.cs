using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAHologramSparkAnimation : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Starting point of the spark animation.")]
        private Vector3 _start;
        [SerializeField]
        [Tooltip("End point of the spark animation.")]
        private Vector3 _end;
        [SerializeField]
        [Tooltip("Time (in seconds) until which the sparking animation reaches the end point.")]
        private float _duration = 1.5f;

        public float duration
        {
            get
            {
                return _duration;
            }
        }

        private float _startTime;

        public void Init(Vector3 start, Vector3 end)
        {
            _start = start;
            _end = end;
            transform.position = _start;
        }

        private void Start()
        {
            _startTime = Time.time;
        }

        private void LateUpdate()
        {
            float t = (Time.time - _startTime) / _duration;
            transform.position = Vector3.Lerp(_start, _end, t);
        }
    }
}