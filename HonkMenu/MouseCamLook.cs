using UnityEngine;
using MelonLoader;

namespace HonkMenu
{
    public class MouseCamLook : MonoBehaviour
    {
        [SerializeField]
        public float sensitivity = 5.0f;
        [SerializeField]
        public float smoothing = 2.0f;

        private GameObject _goose;
        private Vector2 mouseLook;
        private Vector2 smoothV;

        private void Start()
        {
            _goose = GameObject.Find("Goose");
            if (_goose != null)
            {
                _goose = this.transform.parent.gameObject;
            }
            else
            {
                MelonLogger.Msg("Goose not found.");
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.W))
            {
                MelonLogger.Msg("hello camera");

                Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
                md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

                smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
                smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
                mouseLook += smoothV;

                transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
                _goose.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, _goose.transform.up);
            }
        }
    }
}