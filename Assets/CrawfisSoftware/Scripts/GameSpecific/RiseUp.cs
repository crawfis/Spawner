using System;
using System.Collections;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class RiseUp : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private float destructionAltitude = 10;
        [SerializeField] private float growTime = 2;
        [SerializeField] private float initialGrowSize = 0.1f;
        [SerializeField] private float finalGrowSize = 1f;

        void Start()
        {
            StartCoroutine( Behaviour() );
        }

        private IEnumerator Behaviour()
        {
            StartCoroutine(Grow());
            yield return new WaitForSeconds(growTime);
            StartCoroutine(MoveUp());
        }

        private IEnumerator Grow()
        {
            float scale = initialGrowSize;
            transform.localScale = new Vector3(scale, scale, scale);
            float growSpeed = (finalGrowSize - initialGrowSize) / growTime;
            float endGrowTime = Time.time + growTime;
            while(Time.time < endGrowTime)
            {
                scale += growSpeed * Time.deltaTime;
                transform.localScale = new Vector3(scale, scale, scale);
                yield return null;
            }
        }

        private IEnumerator MoveUp()
        {
            Vector3 position = transform.position;
            while (position.y < destructionAltitude)
            {
                position = transform.position;
                position.y += speed * Time.deltaTime;
                transform.position = position;
                yield return null;
            }
            Destroy(this.gameObject);
        }
    }
}
