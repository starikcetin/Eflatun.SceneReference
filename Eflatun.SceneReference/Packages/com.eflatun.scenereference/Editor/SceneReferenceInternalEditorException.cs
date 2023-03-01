using System.Runtime.Serialization;

namespace Eflatun.SceneReference.Editor
{
    internal class SceneReferenceInternalEditorException : SceneReferenceInternalException
    {
        public static SceneReferenceInternalException UnexpectedSceneBuildSettingsState(string location, SceneBuildSettingsState state) =>
            new SceneReferenceInternalEditorException(location, $"Unexpected Scene build settings state. State: \"{state}\"");

        protected SceneReferenceInternalEditorException(string location, string info) : base(location, info)
        {
        }

        private protected SceneReferenceInternalEditorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
