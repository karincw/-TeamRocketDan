using System;
using UnityEngine;

namespace Leo.Animation
{
    public class EllipticalMotion : MonoBehaviour
    {
        private Vector3 _center = new Vector3(0, 0, 0);
        public float widthRadius;
        public float heightRadius;
        public float speed;
        private float _angle;

        public void SetCenter(Vector3 center)
        {
            _center = center;
        }
        
        private void Update()
        {
            _angle += speed * Time.deltaTime;
            var x = _center.x + Mathf.Cos(_angle) * widthRadius;
            var y = _center.y + Mathf.Sin(_angle) * heightRadius;
            transform.position = _center + new Vector3(x, y, 0);
        }
    }
}