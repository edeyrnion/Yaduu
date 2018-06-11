using UnityEngine;

public class MouseManager : Singleton<MouseManager>
{
    [SerializeField] private Texture2D _pointerCurser = null;
    [SerializeField] private Vector2 _pointerCurserHotspot = new Vector2(32, 3);
    [SerializeField] private Texture2D _walkCurser = null;
    [SerializeField] private Vector2 _walkCurserHotspot = new Vector2(0, 0);
    [SerializeField] private Texture2D _targetCurser = null;
    [SerializeField] private Vector2 _targetCurserHotspot = new Vector2(0, 0);

    public Layer[] LayerPriorities =
{
        Layer.Enemy,
        Layer.Walkable
    };

    private float _distanceToBackground = 50f;
    private Camera _viewCamera;

    private RaycastHit _hit;
    public RaycastHit Hit
    {
        get { return _hit; }
    }

    private Layer _layerHit;
    public Layer LayerHit
    {
        get { return _layerHit; }
    }

    public Events.EventOnClick OnClickEnvironment;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        Cursor.SetCursor(_pointerCurser, _pointerCurserHotspot, CursorMode.Auto);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.RUNNING)
        {
            // Look for raycast hit on layers
            foreach (Layer layer in LayerPriorities)
            {
                var hit = RaycastForLayer(layer);
                if (hit.HasValue)
                {
                    _hit = hit.Value;
                    if (_layerHit != layer)
                    {
                        _layerHit = layer;
                        OnLayerChange(layer);
                    }
                    _layerHit = layer;

                    if (Input.GetMouseButton(0))
                    {
                        OnClickEnvironment.Invoke(_hit.point, _layerHit);
                    }

                    return;
                }

            }
            // Return background hit otherwise
            _hit.distance = _distanceToBackground;
            if (_layerHit != Layer.RaycastEndStop)
            {
                _layerHit = Layer.RaycastEndStop;
                OnLayerChange(Layer.RaycastEndStop);
            }
        }
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (currentState == GameManager.GameState.PAUSED || currentState == GameManager.GameState.PREGAME)
        {
            Cursor.SetCursor(_pointerCurser, _pointerCurserHotspot, CursorMode.Auto);
        }
        else
        {
            _viewCamera = Camera.main;
            OnLayerChange(_layerHit);
        }
    }

    void OnLayerChange(Layer newLayer)
    {
        Debug.Log(newLayer);
        switch (newLayer)
        {
            case Layer.Walkable:
                Cursor.SetCursor(_walkCurser, _walkCurserHotspot, CursorMode.Auto);
                break;
            case Layer.Enemy:
                Cursor.SetCursor(_targetCurser, _targetCurserHotspot, CursorMode.Auto);
                break;
            case Layer.RaycastEndStop:
                Cursor.SetCursor(_pointerCurser, _pointerCurserHotspot, CursorMode.Auto);
                break;
            default:
                return;
        }
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer;
        Ray ray = _viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, _distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
