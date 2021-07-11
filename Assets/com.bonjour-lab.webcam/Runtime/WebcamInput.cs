using UnityEngine;

namespace Bonjour.Webcam
{
    public sealed class WebcamInput : MonoBehaviour
    {
        [SerializeField][Tooltip("Camera name")] string deviceName = "";
        [SerializeField][Tooltip("Camera resolution")] Vector2Int resolution = new Vector2Int(1920, 1080);
        [SerializeField][Tooltip("Camera frame rate")] int frameRate = 30;

        WebCamTexture webcam;
        [SerializeField][Tooltip("Target texture to render on")] public RenderTexture targetBuffer;
        [SerializeField][Tooltip("Clamp game to camera FPS (usefull is uyou want to perform image computation)")] bool clampFPSToCameraFPS;
        [SerializeField][Tooltip("Horizontal mirror")] bool horMirror;
        [SerializeField][Tooltip("Vertical mirror")] bool vertMirror;

        private struct ScaleOffset{
            public Vector2 scale;
            public Vector2 offset;
        }

        void Start()
        {
            if(clampFPSToCameraFPS){
                // ! Only usefull if you want to clamp the FPS to the speed of youir camera 
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = frameRate;
            }

            webcam       = new WebCamTexture(deviceName, resolution.x, resolution.y, frameRate);
            webcam.Play();
        }

        void OnDestroy()
        {
            if (webcam != null) Destroy(webcam);
        }

        private ScaleOffset CheckMirror(float aspect, bool vflip)
        {
            ScaleOffset scaleoffset = new ScaleOffset();
            float scalex    = horMirror ? -aspect : aspect;
            float offsetx   = horMirror ?  (1 + aspect) / 2 : (1 - aspect) / 2;

            float flipsy    = vflip ? -1 : 1;
            float flipoy    = vflip ?  1 : 0;

            float scaley    = vertMirror ? -flipsy : flipsy;
            float offsety   = vertMirror ? 1-flipoy : flipoy;

            scaleoffset.scale   = new Vector2(scalex, scaley);
            scaleoffset.offset  = new Vector2(offsetx, offsety);

            return scaleoffset;
        }

        void Update()
        {
            if (!webcam.didUpdateThisFrame) return;

            var aspect1 = (float)webcam.width / webcam.height;
            var aspect2 = (float)resolution.x / resolution.y;
            var gap = aspect2 / aspect1;

            var vflip = webcam.videoVerticallyMirrored;
            var scale = new Vector2(gap, vflip ? -1 : 1);
            var offset = new Vector2((1 - gap) / 2, vflip ? 1 : 0);

            ScaleOffset scaleoffset = CheckMirror((float) gap, (bool) false);

            Graphics.Blit(webcam, targetBuffer, scaleoffset.scale, scaleoffset.offset);
        }

    }
}
