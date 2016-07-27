//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentExtensionsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Entitas;

namespace Entitas {
    public partial class Entity {
        public RMC.EntitasTemplate.Entitas.Components.GameComponent game { get { return (RMC.EntitasTemplate.Entitas.Components.GameComponent)GetComponent(ComponentIds.Game); } }

        public bool hasGame { get { return HasComponent(ComponentIds.Game); } }

        public Entity AddGame(int newDummy) {
            var component = CreateComponent<RMC.EntitasTemplate.Entitas.Components.GameComponent>(ComponentIds.Game);
            component.dummy = newDummy;
            return AddComponent(ComponentIds.Game, component);
        }

        public Entity ReplaceGame(int newDummy) {
            var component = CreateComponent<RMC.EntitasTemplate.Entitas.Components.GameComponent>(ComponentIds.Game);
            component.dummy = newDummy;
            ReplaceComponent(ComponentIds.Game, component);
            return this;
        }

        public Entity RemoveGame() {
            return RemoveComponent(ComponentIds.Game);
        }
    }

    public partial class Pool {
        public Entity gameEntity { get { return GetGroup(Matcher.Game).GetSingleEntity(); } }

        public RMC.EntitasTemplate.Entitas.Components.GameComponent game { get { return gameEntity.game; } }

        public bool hasGame { get { return gameEntity != null; } }

        public Entity SetGame(int newDummy) {
            if (hasGame) {
                throw new EntitasException("Could not set game!\n" + this + " already has an entity with RMC.EntitasTemplate.Entitas.Components.GameComponent!",
                    "You should check if the pool already has a gameEntity before setting it or use pool.ReplaceGame().");
            }
            var entity = CreateEntity();
            entity.AddGame(newDummy);
            return entity;
        }

        public Entity ReplaceGame(int newDummy) {
            var entity = gameEntity;
            if (entity == null) {
                entity = SetGame(newDummy);
            } else {
                entity.ReplaceGame(newDummy);
            }

            return entity;
        }

        public void RemoveGame() {
            DestroyEntity(gameEntity);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherGame;

        public static IMatcher Game {
            get {
                if (_matcherGame == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Game);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherGame = matcher;
                }

                return _matcherGame;
            }
        }
    }
}
