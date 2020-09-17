using AsteroidsCore;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace AsteroidsView
{
    public sealed class Context : MonoBehaviour
    {
        [SerializeField] private ShipDataSetter shipDataSetter;
        [SerializeField] private UfoDataSetter ufoDataSetter;
        [SerializeField] private AsteroidDataSetter asteroidDataSetter;
        [SerializeField] private BulletDataSetter bulletDataSetter;
        [SerializeField] private LaserDataSetter laserDataSetter;

        public World World { get; private set; }
        public ViewMode ViewMode { get; set; }
        public UnityViewFactory ViewFactory { get; private set; }
        public UnityInputService InputService { get; private set; }
        public ShipDataSetter ShipDataSetter => shipDataSetter;
        public UfoDataSetter UfoDataSetter => ufoDataSetter;
        public AsteroidDataSetter AsteroidDataSetter => asteroidDataSetter;
        public BulletDataSetter BulletDataSetter => bulletDataSetter;
        public LaserDataSetter LaserDataSetter => laserDataSetter;

        private void Awake()
        {
            ViewMode = ViewMode.Sprite;

            var dataStorage = new DataStorage
            {
                ShipData = shipDataSetter.data,
                UfoData = ufoDataSetter.data,
                AsteroidData = asteroidDataSetter.data,
                BulletData = bulletDataSetter.data,
                LaserData = laserDataSetter.data
            };
            
            InputService = new UnityInputService();
            
            ViewFactory = new UnityViewFactory(this);

            GetBounds(out var width, out var height, out var offsetX, out var offsetY);

            World = new World(width, height, offsetX, offsetY)
            {
                DataStorage = dataStorage,
                ViewFactory = ViewFactory,
                InputService = InputService
            };
            
            World.Initialize();

            var userInterfaceManager = new GameObject().AddComponent<UserInterfaceManager>();
            userInterfaceManager.Context = this;

            var viewModeManager = new GameObject().AddComponent<ViewModeManager>();
            viewModeManager.Context = this;
        }

        private void Update()
        {
            InputService.Tick();
            World.Tick();
        }

        private void FixedUpdate()
        {
            World.FixedTick();
        }

        private void LateUpdate()
        {
            World.LateTick();
        }

        private void GetBounds(out float width, out float height, out float offsetX, out float offsetY)
        {
            var mainCamera = Camera.main;

            var z = mainCamera.gameObject.transform.position.z;
            var topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, -z));
            var bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, -z));

            width = Vector2.Distance(new Vector2(bottomLeft.x, 0), new Vector2(topRight.x, 0));
            height = Vector2.Distance(new Vector2(0, topRight.y), new Vector2(0, bottomLeft.y));
            offsetX = bottomLeft.x;
            offsetY = topRight.y;
        }
    }
}