using SimpleSynth.Parameters;
using SimpleSynth.Parsing;
using SimpleSynth.Providers;
using SimpleSynth.Synths;

namespace ChordService.AudioGeneration
{
    public class AudioFromMidiBuilder
    {
        public Stream GetWave(Stream midiFile)
        {
            var interpretation = new MidiInterpretation(midiFile, new DefaultNoteSegmentProvider());
            var synth = BuildSynthesizer(interpretation);
            return synth.GenerateWAV();
        }

        private MidiSynth BuildSynthesizer(MidiInterpretation interpretation)
        {
            return new BasicSynth(
                interpretation,
                new DefaultAdsrEnvelopeProvider(AdsrParameters.Short), 
                new DefaultBalanceProvider());
        }
    }
}
