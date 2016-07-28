﻿using UnityEngine;
using Entitas;
using UnityEngine.UI;
using System;

namespace RMC.EntitasTemplate.Entitas.Controllers
{
	/// <summary>
	/// Replace me with description.
	/// </summary>
	public class CanvasController : MonoBehaviour
	{
		// ------------------ Constants and statics

		// ------------------ Events

		// ------------------ Serialized fields and properties
				private Group _gameScore;

		// ------------------ Methods


        public Text _scoreText;
        public Button _restartButton;
		public Button _pauseButton;

		// ------------------ Non-serialized fields
        private Entity _gameEntity;
		// ------------------ Methods

        protected void Start()
        {

            Group group = Pools.pool.GetGroup(Matcher.AllOf(Matcher.Game, Matcher.Bounds, Matcher.Score));
            SetGameGroup(group);

            _restartButton.onClick.AddListener (OnRestartButtonClicked);
            _pauseButton.onClick.AddListener (OnPauseButtonClicked);

        }

        protected void OnDestroy()
        {
            _restartButton.onClick.RemoveListener (OnRestartButtonClicked);
            _pauseButton.onClick.RemoveListener (OnPauseButtonClicked);

        }

        private void SetGameGroup (Group group)
        {
            Debug.Log("ad: " + group.count);
            //The group should have 1 thing, always, but...
            //FIXME: after multiple restarts (via 'r' button in HUD) this fails - srivello
            if (group.count == 1) 
            {
                _gameEntity = group.GetSingleEntity();
                _gameEntity.OnComponentReplaced += OnComponentReplaced;

                //set first value
                var scoreComponent = _gameEntity.score;
                SetText (scoreComponent.whiteScore, scoreComponent.blackScore, _gameEntity.time.timeSinceGameStartUnpaused);

            }
            else
            {
                Debug.LogWarning ("CC _scoreGroup failed, should have one entity, has count: " + group.count);    
            }
        }

        private void OnComponentReplaced(Entity entity, int index, IComponent previousComponent, IComponent newComponent)
        {
            SetText(entity.score.whiteScore, entity.score.blackScore, entity.time.timeSinceGameStartUnpaused);
        }

        private void SetText(int whiteScore, int blackScore, float time)
        {
            _scoreText.text = string.Format ("White: {0}\t\tBlack: {1}\t\tTime: {2:000}", whiteScore, blackScore, time);
        }
            
		private void OnRestartButtonClicked()
        {
           GameController.Instance.Restart();
        }
		private void OnPauseButtonClicked()
        {
           GameController.Instance.TogglePause();
        }

	}
}