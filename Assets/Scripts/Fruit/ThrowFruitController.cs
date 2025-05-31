using UnityEngine;

public class ThrowFruitController : MonoBehaviour
{
    public static ThrowFruitController instance;

    public GameObject CurrentFruit { get; set; }
    [SerializeField] private Transform _throwFruitTransform;
    [SerializeField] private Transform _parentAfterThrow;
    [SerializeField] private FruitSelector _selector;

    private PlayerController _playerController;

    private Rigidbody2D _rb;
    private CircleCollider2D _circleCollider;

    public Bounds Bounds { get; private set; }

    private const float EXTRA_WIDTH = 0.03f;

    public bool CanThrow { get; set; } = true;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();

        SpawnAFruit(_selector.PickRandomFruitForThrow());
    }

    private void Update()
    {
        if (UserInput.IsThrowPressed && CanThrow)
        {
            _playerController.SetThrowingSprite();
            SpriteIndex index = CurrentFruit.GetComponent<SpriteIndex>();
            Quaternion rot = CurrentFruit.transform.rotation;

            GameObject go = Instantiate(FruitSelector.instance.Fruits[index.Index], CurrentFruit.transform.position, rot);
            go.transform.SetParent(_parentAfterThrow);
            Destroy(CurrentFruit);

            CanThrow = false;
        }
    }

    public void SpawnAFruit(GameObject fruit)
    {
        GameObject go = Instantiate(fruit, _throwFruitTransform);
        go.transform.localScale = Vector3.one;
        CurrentFruit = go;
        _circleCollider = CurrentFruit.GetComponent<CircleCollider2D>();
        Bounds = _circleCollider.bounds;

        _playerController.ChangeBoundary(EXTRA_WIDTH);
    }
}
