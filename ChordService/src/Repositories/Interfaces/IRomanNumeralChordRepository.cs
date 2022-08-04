using ChordService.src.Models.Chords;

namespace ChordService.src.Repositories.Interfaces
{
    public interface IRomanNumeralChordRepository
    {
        public Task<Chord> GetAsync(RomanNumeralChord chord, CancellationToken ct = default(CancellationToken));
    }
}
