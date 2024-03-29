﻿using Assets._2D_RPG_Prototype.Code.Player;
using System;
using System.Linq;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public class SaveLoadService : ISaveLoadService
    {
        public event Action OnSave;
        public PlayerData PlayerData => _playerData;

        private PlayerData _playerData;

        public SaveLoadService() =>
            _playerData = PlayerData.Load();

        public void Save()
        {
            _playerData.Save();
            OnSave?.Invoke();
            PlayerPrefs.Save();
        }

        public static void Save(string key, string value) =>
            PlayerPrefs.SetString(key, value);

        public static string Load(string key, string defaultValue) =>
            PlayerPrefs.GetString(key, defaultValue);

        public static void Save<T>(string key, T value) =>
            Save(key, JsonUtility.ToJson(value));

        public static T Load<T>(string key, T defaultValue)
        {
            if (!PlayerPrefs.HasKey(key))
                return defaultValue;

            return JsonUtility.FromJson<T>(Load(key, ""));
        }
    }
}
