using _Project.Develop.Runtime.Data.Save.Services;
using _Project.Develop.Runtime.Domain.SaveLoadFeature.Interfaces;
using _Project.Develop.Runtime.Domain.SaveLoadFeature.Models;
using _Project.Develop.Runtime.Domain.Shared;

namespace _Project.Develop.Runtime.Startup.Installers
{
    public class ServiceInstaller
    {
        public ISaveLoadService SaveLoadService { get; private set; }
        public GameSaveData GameSaveData { get; private set; }
        public TimeService TimeService { get; private set; }

        public void Init()
        {
            SaveLoadService = new SaveLoadServiceFile();

            var loadedData = SaveLoadService.Load();
            GameSaveData = loadedData ?? new GameSaveData();

            TimeService = new TimeService();
        }
    }
}