using System;

namespace Eflatun.SceneReference.Editor.Toolbox
{
    internal interface ITool
    {
        void Draw(Action closeToolbox);
        float GetHeight();
    }
}
