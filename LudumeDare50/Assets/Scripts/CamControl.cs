using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoundsCheck))]
public class CamControl : MonoBehaviour
{
    [SerializeField] private Vector2Int camSizeRange;
    [SerializeField] private float moveSpeed, zoomTime;
    [SerializeField] private int zoomStep = 1;

    private Coroutine camCor;
    private Camera cam;
    private int initLevel, targetLevel = 0;
    private float currentLevel;
    private float currZoomTime;
    private bool isZooming;

    private BoundsCheck bc;

    private void InitCameraSize()
    {
        if(cam.orthographicSize < camSizeRange.y)
        {
            if(cam.orthographicSize > camSizeRange.x)
            {
                currentLevel = cam.orthographicSize;
            }
            else
            {
                currentLevel = camSizeRange.x;
                cam.orthographicSize = currentLevel;
            }
        }
        else
        {
            currentLevel = camSizeRange.y;
            cam.orthographicSize = currentLevel;
        }
    }

    private void Awake()
    {
        bc = GetComponent<BoundsCheck>();
    }

    void Start()
    {
        cam = Camera.main;

        InitCameraSize();
    }

    void Update()
    {
        if(Input.anyKey)
        {
            MoveCam();
        }

        if(Input.mouseScrollDelta.y != 0 && !isZooming)
        {
            if(Input.mouseScrollDelta.y > 0 && currentLevel == camSizeRange.x)
                return;
            if(Input.mouseScrollDelta.y < 0 && currentLevel == camSizeRange.y)
                return;

            bool b = Input.mouseScrollDelta.y > 0;
            InitScrolling(b);
        }
    }

    private void MoveCam()
    {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * cam.orthographicSize * moveSpeed * Time.deltaTime;
        cam.transform.position += velocity;
    }

    private void InitScrolling(bool zoomIn)
    {
        if(initLevel == 0)
        {
            initLevel = (int)currentLevel;
            targetLevel = initLevel;
        }

        int add = (zoomIn ? -1 : 1) * zoomStep;
        targetLevel = Mathf.Clamp(targetLevel + add, camSizeRange.x, camSizeRange.y);

        StartCoroutine(ZoomDelay());
    }

    private IEnumerator ZoomDelay()
    {
        isZooming = true;

        currentLevel = initLevel;
        currZoomTime = zoomTime;
        float t = 0;

        while(t < 1)
        {
            currZoomTime -= Time.deltaTime;
            t = (zoomTime - currZoomTime) / zoomTime;
            cam.orthographicSize = Mathf.SmoothStep(currentLevel, targetLevel, t);
            bc.SetRadius();
            yield return null;
        }

        currentLevel = targetLevel;
        initLevel = 0;
        currZoomTime = 0;

        isZooming = false;
    }
}
