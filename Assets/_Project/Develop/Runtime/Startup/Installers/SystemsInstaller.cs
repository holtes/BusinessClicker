using Leopotam.Ecs;
using _Project.Develop.Runtime.Startup.Data;
using _Project.Develop.Runtime.Presentation.BalanceViews.Systems;
using _Project.Develop.Runtime.Presentation.BusinessViews.Systems;
using _Project.Develop.Runtime.Presentation.UpgradeViews.Systems;
using _Project.Develop.Runtime.Domain.BusinessFeature.Models;
using _Project.Develop.Runtime.Domain.BusinessFeature.Systems;
using _Project.Develop.Runtime.Domain.BalanceFeature.Systems;
using _Project.Develop.Runtime.Domain.SaveLoadFeature.Systems;
using _Project.Develop.Runtime.Domain.LevelUpFeature.Systems;
using _Project.Develop.Runtime.Domain.UpgradeFeature.Systems;
using _Project.Develop.Runtime.Domain.Shared;

namespace _Project.Develop.Runtime.Startup.Installers
{
    public class SystemsInstaller
    {
        public void Init(
            EcsSystems systems,
            EcsSystems appSystems,
            BusinessInitData[] businessInitData,
            SceneData sceneData,
            ServiceInstaller services,
            MapsInstaller mapsInstaller)
        {
            systems
                // 1. Инициализация данных
                .Add(new BusinessInitSystem(businessInitData))
                .Add(new BalanceInitSystem())
                .Add(new LoadSystem(services.SaveLoadService))

                // 2. Логика
                .Add(new IncomeProgressSystem(services.TimeService))
                .Add(new IncomeCollectSystem())
                .Add(new LevelUpSystem(mapsInstaller.IdEntityMap))
                .Add(new UpgradePurchaseSystem(mapsInstaller.IdEntityMap))
                .Add(new BalanceUpdateSystem())

                // 3. View инициализация
                .Add(new BalanceViewInitSystem(sceneData.BalanceView))
                .Add(new BusinessViewInitSystem(sceneData.BusinessListView,
                    sceneData.BusinessPrefab, mapsInstaller.IdEntityMap, mapsInstaller.IdViewMap))

                // 4. View обновление
                .Add(new BalanceViewUpdateSystem(sceneData.BalanceView))
                .Add(new BusinessViewUpdateSystem(mapsInstaller.IdViewMap))
                .Add(new UpgradeViewUpdateSystem(mapsInstaller.IdViewMap))
                .Add(new BusinessProgressViewUpdateSystem(mapsInstaller.IdViewMap))

                // OneFrame компоненты
                .OneFrame<ViewInitRequest>()
                .OneFrame<UpdateUIFlag>();

            // Добавление системы сохранения
            appSystems.Add(new SaveSystem(services.SaveLoadService));
        }
    }
}