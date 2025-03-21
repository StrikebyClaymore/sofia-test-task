using SofiaTestTask.Entities;
using SofiaTestTask.UI;
using SofiaTestTask.Utility;
using UnityEngine;
using Zenject;

namespace SofiaTestTask
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ViewsContainer _viewsContainer;
        [SerializeField] private GameLoop _gameLoop;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private CameraFollow _cameraFollow;
        
        public override void InstallBindings()
        {
            Container.BindInstance<PlayerConfig>(_playerConfig).AsSingle().NonLazy();
            Container.BindInstance<PlayerView>(_playerView).AsSingle().NonLazy();
            
            Container.BindInstance<ViewsContainer>(_viewsContainer).AsSingle().NonLazy();
            Container.BindInstance<CameraFollow>(_cameraFollow).AsSingle().NonLazy();
            Container.Bind<InputManager>().AsSingle().NonLazy();
            Container.Bind<ControlsController>().AsSingle().NonLazy();
            Container.Bind<ControllersManager>().AsSingle().NonLazy();
            
            Container.Bind<Player>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
            Container.BindInstance<GameLoop>(_gameLoop).AsSingle().NonLazy();
        }
    }
}