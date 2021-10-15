using System;
using System.IO;
using Mutagen.Bethesda.Synthesis.Settings;

namespace RecombinePatch
{
    public class Settings
    {
        [SynthesisOrder]
        [SynthesisSettingName("External Config Path")]
        [SynthesisDescription("An external path for all configuration settings")]
        public string ExternalConfigurationPath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Recombine");

        [SynthesisOrder]
        [SynthesisSettingName("Launch Config Editor")]
        [SynthesisDescription("Will interrupt patch process by launching the configuration editor")]
        public bool LaunchConfigEditor { get; set; } = true;
    }
}
