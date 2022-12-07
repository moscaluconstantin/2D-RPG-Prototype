using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.UI.Stats
{
    public class CharacterStatsWindow : MonoBehaviour
    {
        [SerializeField] CharacterStatsView _viewPrefab;
        [SerializeField] Transform _container;

        private List<CharacterStatsView> _views;

        private void Awake()
        {
            _views = new List<CharacterStatsView>();

            var existingViews = _container.GetComponentsInChildren<CharacterStatsView>(true);
            foreach (var existingView in existingViews)
                Destroy(existingView.gameObject);

            CharacterStats[] stats = ServiceProvider.GetService<IStatsManager>().Stats;
            foreach (var stat in stats)
            {
                var view = Instantiate(_viewPrefab, _container);
                view.Initialize(stat);
                _views.Add(view);
            }
        }

        private void OnEnable()
        {
            if (_views == null || _views.Count <= 0)
                return;

            foreach (var view in _views)
                view.Refresh();
        }
    }
}
