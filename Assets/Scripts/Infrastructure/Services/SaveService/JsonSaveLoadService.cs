using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Features.Inventory;
using Features.Player.Stats;
using Infrastructure.Factory;
using Infrastructure.Services.Progress;
using Infrastructure.Utils;
using Newtonsoft.Json;
using UnityEngine;

namespace Infrastructure.Services.SaveService
{
    public class JsonSaveLoadService : ISaveLoadService
    {
        private const string SavePath = "SaveData/";
        private const string FileName = "save.json";
        private const int DefaultBufferSize = 4096;

        private readonly IProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.None,
            Converters = new List<JsonConverter>
            {
                new InterfaceTypeConverter<ICharacterStat>(),
                new InterfaceTypeConverter<IItem>()
            }
        };

        public JsonSaveLoadService(IProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public async UniTask SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);

            var path = Path.Combine(Application.persistentDataPath, SavePath);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var json = JsonConvert.SerializeObject(_progressService.Progress, _jsonSettings);

            var fullPath = Path.Combine(path, FileName);
            await using var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, DefaultBufferSize, true);
            await using var writer = new StreamWriter(fs);
            await writer.WriteAsync(json);

            Debug.Log($"[SaveLoad] Progress saved: {fullPath}");
        }

        public async UniTask<PlayerProgress> LoadProgress()
        {
            var fullPath = Path.Combine(Application.persistentDataPath, SavePath, FileName);

            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("[SaveLoad] No save data. Start new progress");
                _progressService.Progress = new PlayerProgress();
                return _progressService.Progress;
            }

            await using var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, true);
            using var reader = new StreamReader(fs);
            var json = await reader.ReadToEndAsync();

            var progress = JsonConvert.DeserializeObject<PlayerProgress>(json, _jsonSettings) ?? new PlayerProgress();

            _progressService.Progress = progress;
            return progress;
        }
    }
}