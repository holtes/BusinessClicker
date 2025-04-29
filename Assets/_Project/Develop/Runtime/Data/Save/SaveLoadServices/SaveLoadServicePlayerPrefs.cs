using UnityEngine;
using _Project.Develop.Runtime.Domain.SaveLoadFeature.Interfaces;
using _Project.Develop.Runtime.Domain.SaveLoadFeature.Models;

namespace _Project.Develop.Runtime.Data.Save.Services
{
    public class SaveLoadServicePlayerPrefs : ISaveLoadService
    {
        private const string SaveKey = "GameSaveData";

        public void Save(GameSaveData saveData)
        {
            string json = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }

        public GameSaveData Load()
        {
            if (!PlayerPrefs.HasKey(SaveKey))
                return null;

            string json = PlayerPrefs.GetString(SaveKey);
            return JsonUtility.FromJson<GameSaveData>(json);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(SaveKey);
        }
    }
}