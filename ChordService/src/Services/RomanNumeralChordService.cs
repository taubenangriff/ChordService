using ChordService.src.Models.Chords;
using ChordService.src.Repositories.Interfaces;
using ChordService.src.Services.Interfaces;

namespace ChordService.src.Services
{
    public class RomanNumeralChordService : IRomanNumeralChordService
    {
        private readonly IRomanNumeralChordRepository _romanNumeralChordRepository;

        public RomanNumeralChordService(IRomanNumeralChordRepository repository)
        {
            _romanNumeralChordRepository = repository;
        }

        public async Task<Chord> GetChordAsync(RomanNumeralChord _chord, CancellationToken ct = default(CancellationToken))
        {
            return await _romanNumeralChordRepository.GetAsync(_chord, ct);
        }

        public async Task<Chord> GetChordAsync(RomanNumeralChord chord, int? inversion, int? transposition, CancellationToken ct = default(CancellationToken))
        {
            Chord c = await _romanNumeralChordRepository.GetAsync(chord, ct);
            c = transposition.HasValue ? c.Transpose(transposition.Value) : c;
            c = inversion.HasValue ? c.Invert(inversion.Value) : c;
            return c;
        }
    }
}
