using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAHologramSparkAnimation : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Starting point of the spark animation.")]
        private Transform _start;
        [SerializeField]
        [Tooltip("End point of the spark animation.")]
        private Transform _end;
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

        public void Init(Transform start, Transform end)
        {
            _start = start;
            _end = end;
            transform.position = _start.position;
        }

        private void Start()
        {
            _startTime = Time.time;
        }

        private void LateUpdate()
        {
            float t = (Time.time - _startTime) / _duration;
            transform.localPosition = Vector3.Lerp(_start.localPosition, _end.localPosition, t);
        }
    }
}