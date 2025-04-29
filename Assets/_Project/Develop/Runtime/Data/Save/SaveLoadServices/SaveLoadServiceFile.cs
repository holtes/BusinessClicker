using UnityEngine;
using System.IO;
using _Project.Develop.Runtime.Domain.SaveLoadFeature.Interfaces;
using _Project.Develop.Runtime.Domain.SaveLoadFeature.Models;

namespace _Project.Develop.Runtime.Data.Save.Services
{
    public class SaveLoadServiceFile : ISaveLoadService
    {
        private readonly string _filePath;

        public SaveLoadServiceFile(string fileName = "gamesave.json")
        {
            _filePath = Path.Combine(Application.persistentDataPath, fileName);
        }

        public void Save(GameSaveData saveData)
        {
            string json = JsonUtility.ToJson(saveData, prettyPrint: true);
            File.WriteAllText(_filePath, json);
        }

        public GameSaveData Load()
        {
            if (!File.Exists(_filePath))
                return null;

            string json = File.ReadAllText(_filePath);
            return JsonUtility.FromJson<GameSaveData>(json);
        }

        public void Clear()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);
        }
    }
}