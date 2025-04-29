using System.Collections.Generic;
using Leopotam.Ecs;
using _Project.Develop.Runtime.Presentation.BusinessViews.Views;


namespace _Project.Develop.Runtime.Startup.Installers
{
    public class MapsInstaller
    {
        public Dictionary<int, EcsEntity> IdEntityMap { get; private set; }
        public Dictionary<int, BusinessView> IdViewMap { get; private set; }

        public void Init()
        {
            IdEntityMap = new Dictionary<int, EcsEntity>();

            IdViewMap = new Dictionary<int, BusinessView>();
        }

    }
}