using System.Collections;
using UnityEngine;

namespace Assets._Dev.Behaviours
{
    public class Spin : MonoBehaviour
    {
        public float roundsPerMinute = 50f;

        // Update is called once per frame
        void Update()
        {
            var degreesPerSecond = roundsPerMinute * 360 / 60;

            transform.Rotate(0, degreesPerSecond * Time.deltaTime, 0);
        }
    }
}