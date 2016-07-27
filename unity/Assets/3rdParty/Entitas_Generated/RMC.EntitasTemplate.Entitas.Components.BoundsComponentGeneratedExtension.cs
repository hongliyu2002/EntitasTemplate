//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentExtensionsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Entitas {
    public partial class Entity {
        public RMC.EntitasTemplate.Entitas.Components.BoundsComponent bounds { get { return (RMC.EntitasTemplate.Entitas.Components.BoundsComponent)GetComponent(ComponentIds.Bounds); } }

        public bool hasBounds { get { return HasComponent(ComponentIds.Bounds); } }

        public Entity AddBounds(UnityEngine.Bounds newBounds) {
            var component = CreateComponent<RMC.EntitasTemplate.Entitas.Components.BoundsComponent>(ComponentIds.Bounds);
            component.bounds = newBounds;
            return AddComponent(ComponentIds.Bounds, component);
        }

        public Entity ReplaceBounds(UnityEngine.Bounds newBounds) {
            var component = CreateComponent<RMC.EntitasTemplate.Entitas.Components.BoundsComponent>(ComponentIds.Bounds);
            component.bounds = newBounds;
            ReplaceComponent(ComponentIds.Bounds, component);
            return this;
        }

        public Entity RemoveBounds() {
            return RemoveComponent(ComponentIds.Bounds);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherBounds;

        public static IMatcher Bounds {
            get {
                if (_matcherBounds == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Bounds);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherBounds = matcher;
                }

                return _matcherBounds;
            }
        }
    }
}
