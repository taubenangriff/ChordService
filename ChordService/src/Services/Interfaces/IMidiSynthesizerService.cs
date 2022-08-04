namespace ChordService.src.Services.Interfaces
{
    public interface IMidiSynthesizerService
    {
        public Task<Stream> GetSynthesizedWaveAudioAsync(Stream midiFile, CancellationToken ct = default(CancellationToken));
    }
}
