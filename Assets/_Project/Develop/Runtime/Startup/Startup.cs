using Leopotam.Ecs;
using UnityEngine;
using _Project.Develop.Runtime.Startup.Data;
using _Project.Develop.Runtime.Startup.Installers;
using _Project.Develop.Runtime.Data.Configs;

namespace _Project.Develop.Runtime.Startup 
{
    sealed class Startup : MonoBehaviour {
        [SerializeField] private BusinessNamesDB _businessNamesDatabase;
        [SerializeField] private BusinessesDB _businessesDatabase;
        [SerializeField] private SceneData _sceneData;

        private EcsWorld _world;
        private EcsSystems _defaultSystems;
        private EcsSystems _appSystems;
        private ServiceInstaller _serviceInstaller;
        private MapsInstaller _mapsInstaller;

        private void Awake () {
            _world = new EcsWorld();
            _defaultSystems = new EcsSystems (_world);
            _appSystems = new EcsSystems (_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_defaultSystems);
#endif
            _serviceInstaller = new ServiceInstaller();
            _serviceInstaller.Init();

            _mapsInstaller = new MapsInstaller();
            _mapsInstaller.Init();

            var businessesInitData = DataInstaller.PrepareBusinessInitData(_businessesDatabase, _businessNamesDatabase);

            var systemsInstaller = new SystemsInstaller();
            systemsInstaller.Init(
                _defaultSystems,
                _appSystems,
                businessesInitData,
                _sceneData,
                _serviceInstaller,
                _mapsInstaller
            );

            _appSystems.Init();
            _defaultSystems.Init();
        }

        private void Update () {
            _defaultSystems?.Run ();
        }

        private void OnDestroy () {
            if (_defaultSystems != null) {
                _defaultSystems.Destroy ();
                _defaultSystems = null;
                _appSystems.Destroy();
                _appSystems = null;
                _world.Destroy ();
                _world = null;
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                _appSystems.Run();
            }
        }

        private void OnApplicationQuit()
        {
            _appSystems.Run();
        }
    }
}