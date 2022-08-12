using System;

namespace Eflatun.SceneReference
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SceneReferenceOptionsAttribute : Attribute
    {
        public ColoringBehaviour Coloring = ColoringBehaviour.DoNotOverride;
        public UtilityLineBehaviour UtilityLine = UtilityLineBehaviour.DoNotOverride;
    }
}
