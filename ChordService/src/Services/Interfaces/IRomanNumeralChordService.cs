using ChordService.src.Models.Chords;

namespace ChordService.src.Services.Interfaces
{
    public interface IRomanNumeralChordService
    {
        Task<Chord> GetChordAsync(RomanNumeralChord chord, CancellationToken ct = default(CancellationToken));
        Task<Chord> GetChordAsync(RomanNumeralChord chord, int? inversion, int? transposition, CancellationToken ct = default(CancellationToken));
    }
}
