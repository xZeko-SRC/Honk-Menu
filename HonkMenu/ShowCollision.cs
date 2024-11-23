using UnityEngine;
using MelonLoader;

namespace HonkMenu
{
    public class ShowCollision : MonoBehaviour
    {
        private string _collision;
        private string _collisionTag;
        public bool _coll = true;

        private GameObject _lastCollidedObject;
        private LineRenderer lineRenderer;

        void Start()
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 8;

            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;

            lineRenderer.loop = true;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

            Color tealColor = new Color(0.0f, 0.8f, 0.8f);
            lineRenderer.startColor = tealColor;
            lineRenderer.endColor = tealColor;

            lineRenderer.enabled = false;
        }

        void OnCollisionEnter(Collision collider)
        {
            _collision = collider.gameObject.name;
            _collisionTag = collider.gameObject.tag;
            _lastCollidedObject = collider.gameObject;

            MelonLogger.Msg($"Collision Detected: {_collision}, Tag: {_collisionTag}");

            if (_coll && lineRenderer.enabled && _lastCollidedObject != null)
            {
                DrawBoundingBox(_lastCollidedObject);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                _coll = !_coll;
                MelonLogger.Msg($"Toggled _coll: {_coll}");

                if (!_coll)
                {
                    lineRenderer.enabled = false;
                }
            }

            if (_coll && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Y))
            {
                lineRenderer.enabled = !lineRenderer.enabled;
                MelonLogger.Msg($"Toggled LineRenderer: {lineRenderer.enabled}");
            }
        }

        void DrawBoundingBox(GameObject obj)
        {
            Collider collider = obj.GetComponent<Collider>();
            if (collider == null) return;

            Bounds bounds = collider.bounds;

            Vector3[] corners = new Vector3[8];
            corners[0] = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
            corners[1] = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
            corners[2] = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, bounds.extents.z);
            corners[3] = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, bounds.extents.z);
            corners[4] = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, -bounds.extents.z);
            corners[5] = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, -bounds.extents.z);
            corners[6] = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, bounds.extents.z);
            corners[7] = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, bounds.extents.z);

            lineRenderer.positionCount = 16;
            lineRenderer.SetPositions(new Vector3[]
            {
                corners[0], corners[1], corners[2], corners[3], corners[0],
                corners[4], corners[5], corners[6], corners[7], corners[4],
                corners[0], corners[4],
                corners[1], corners[5],
                corners[2], corners[6],
                corners[3], corners[7]
            });
        }

        void OnGUI()
        {
            if (_coll && !string.IsNullOrEmpty(_collision))
            {
                GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 2000, 20), _collision);
                GUI.Label(new Rect(Screen.width / 2, (Screen.height / 2) - 100, 2000, 20), _collisionTag);
            }
        }
    }
}
