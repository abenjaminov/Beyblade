using System.Collections;
using UnityEngine;

namespace Assets._Dev.Behaviours
{
    public class Spin : MonoBehaviour
    {
        public float rotationSpeed = 50f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, 1 * rotationSpeed * Time.deltaTime, 0);
        }
    }
}