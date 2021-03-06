﻿using Entitas;
using RMC.Common.Entitas.Components.Input;
using RMC.EntitasTemplate.Entitas.Controllers.Singleton;
using UnityEngine;

namespace RMC.EntitasTemplate.Entitas.Systems
{
	/// <summary>
	/// Process input
	/// </summary>
    public class AcceptInputSystem : ISetPool, IInitializeSystem, IExecuteSystem 
	{
		// ------------------ Constants and statics

		// ------------------ Events

		// ------------------ Serialized fields and properties

		// ------------------ Non-serialized fields
        private Pool _pool;
		private Group _inputGroup;
        private Entity _gameEntity;
        private Group _acceptInputGroup;

		// ------------------ Methods

		// Implement ISetPool to get the pool used when calling
		// pool.CreateSystem<MoveSystem>();
		public void SetPool(Pool pool) 
		{
            _pool = pool;
        }

        public void Initialize()
        {
            // Get the group of entities that have a Move and Position component
            _inputGroup = _pool.GetGroup(Matcher.AllOf(Matcher.Input));
            _acceptInputGroup = _pool.GetGroup(Matcher.AllOf(Matcher.AcceptInput));

            //By design: Systems created before Entities, so wait :)
            _pool.GetGroup(Matcher.AllOf(Matcher.Score)).OnEntityAdded += OnGameEntityAdded;
        }

        private void OnGameEntityAdded (Group group, Entity entity, int index, IComponent component)
        {
            _gameEntity = group.GetSingleEntity();
        }


		public void Execute() 
		{
			
            foreach (var inputEntity in _inputGroup.GetEntities()) 
            {

                //We choose to listen to axis, not buttons. But either is possible - srivello
                if (inputEntity.input.inputType == InputComponent.InputType.Axis)
                {
                    foreach (var acceptInputEntity in _acceptInputGroup.GetEntities())
                    {
                        //Debug.Log("inputEntity.input.inputAxis.y : " + inputEntity.input.inputAxis.y);
                        RMC.Common.UnityEngineReplacement.Vector3 nextVelocity = new RMC.Common.UnityEngineReplacement.Vector3
                            (
                                inputEntity.input.inputAxis.x * GameConstants.VelocityPerInputAxisSpeed,
                                inputEntity.input.inputAxis.y * GameConstants.VelocityPerInputAxisSpeed, 
                                0
                            );
                        acceptInputEntity.ReplaceVelocity(nextVelocity);
                        AddScore();


                    }
                }
                else if (inputEntity.input.inputType == InputComponent.InputType.KeyCode)
                {
                    if (inputEntity.input.inputKeyCode == RMC.Common.UnityEngineReplacement.KeyCode.Space)
                    {
                        GameController.Instance.Restart();
                    }
                }

                inputEntity.WillDestroy(true);
            }

		}

        /// <summary>
        /// Give points while moving. For fun!
        /// </summary>
        private void AddScore()
        {
            int nextScore = _gameEntity.score.score + 1;
            _gameEntity.ReplaceScore(nextScore);
        }


	}
}