using ChordService.src.Models.Chords;

namespace ChordService.src.Services.Interfaces
{
    public interface IChordAsMidiService
    {
        Task<Stream> GetMidiAsync(Chord _chord, CancellationToken ct = default(CancellationToken));
    }
}
