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
        static readonly RMC.Common.Entitas.Components.Destroy.DestroyComponent destroyComponent = new RMC.Common.Entitas.Components.Destroy.DestroyComponent();

        public bool willDestroy {
            get { return HasComponent(ComponentIds.Destroy); }
            set {
                if (value != willDestroy) {
                    if (value) {
                        AddComponent(ComponentIds.Destroy, destroyComponent);
                    } else {
                        RemoveComponent(ComponentIds.Destroy);
                    }
                }
            }
        }

        public Entity WillDestroy(bool value) {
            willDestroy = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherDestroy;

        public static IMatcher Destroy {
            get {
                if (_matcherDestroy == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Destroy);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherDestroy = matcher;
                }

                return _matcherDestroy;
            }
        }
    }
}
