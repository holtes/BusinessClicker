using _Project.Develop.Runtime.Domain.SaveLoadFeature.Models;

namespace _Project.Develop.Runtime.Domain.SaveLoadFeature.Interfaces
{
    public interface ISaveLoadService
    {
        public void Save(GameSaveData saveData);
        public GameSaveData Load();
        public void Clear();
    }
}
