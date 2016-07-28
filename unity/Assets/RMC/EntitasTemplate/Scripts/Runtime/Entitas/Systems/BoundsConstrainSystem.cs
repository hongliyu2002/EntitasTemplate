﻿using Entitas;
using UnityEngine;
using RMC.Common.Entitas.Components;
using System;
using System.Collections.Generic;

namespace RMC.Common.Entitas.Systems
{
    /// <summary>
    /// Constains the paddle's y position within the screenbounds.
    /// Great example of a system that operates ONLY when a component (position) is changed. Efficient!
    /// </summary>
    public class BoundsConstrainSystem : ISystem, ISetPool, IEnsureComponents
    {
        // ------------------ Constants and statics

        // ------------------ Events

        // ------------------ Serialized fields and properties

        // ------------------ Non-serialized fields
        private Group _group;
        private Entity _gameEntity;
        private GroupObserver _onPaddlePositionUpdated;

        // ------------------ Methods

        public IMatcher ensureComponents { get { return Matcher.View; } }

        // Implement ISetPool to get the pool used when calling
        // pool.CreateSystem<MoveSystem>();
        public void SetPool(Pool pool) 
        {
            _group = pool.GetGroup(Matcher.AllOf(Matcher.Paddle, Matcher.Position, Matcher.View));
            _group.OnEntityUpdated += OnPaddlePositionUpdated;

            //By design: Systems created before Entities, so wait :)
            pool.GetGroup(Matcher.AllOf(Matcher.Game, Matcher.Bounds, Matcher.Score)).OnEntityAdded += OnGameEntityAdded;

        }

        private void OnGameEntityAdded (Group group, Entity entity, int index, IComponent component)
        {
            //TODO: I expect this to be called on game start and game restart, but not every StartNextRound, why - srivello
            //Debug.Log("added _gameEntity: " + _gameEntity);
            _gameEntity = group.GetSingleEntity();
        }

        //I explored several ways to have this system only respond when a paddle's position changes
        //1. _group.OnEntityUpdated += OnPaddlePositionUpdated; I'm using this now.
        //2. I couldn't find a way to do it with "public TriggerOnEvent trigger". its more about entity add than components, yes?
        //3. _onPaddlePositionUpdated = _group.CreateObserver(GroupEventType.OnEntityAdded). its more about entity add than components, yes?
        private void OnPaddlePositionUpdated (Group group, Entity paddleEntity, int index, IComponent previousComponent, IComponent newComponent)
        {

            Bounds bounds = _gameEntity.bounds.bounds;
            float sizeY = paddleEntity.view.bounds.size.y / 2;
            Vector3 nextPosition = paddleEntity.position.position;

            //Be careful only to call paddleEntity.ReplacePosition() within the 'if' to prevent an infinite loop - srivello
            if (paddleEntity.position.position.y - sizeY < bounds.min.y)
            {
                nextPosition = new Vector3(nextPosition.x, bounds.min.y + sizeY, nextPosition.z);
                paddleEntity.ReplacePosition(nextPosition);
            }
            else if (paddleEntity.position.position.y + sizeY > bounds.max.y)
            {
                nextPosition = new Vector3(nextPosition.x, bounds.max.y - sizeY, nextPosition.z);
                paddleEntity.ReplacePosition(nextPosition);
            }



        }

    }
}


