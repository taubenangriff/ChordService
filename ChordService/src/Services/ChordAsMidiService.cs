using ChordService.src.Models.Chords;
using ChordService.src.Repositories.Interfaces;
using ChordService.src.Services.Interfaces;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace ChordService.src.Services
{
    public class ChordAsMidiService : IChordAsMidiService
    {
        private readonly IMidiRepository _midiRepository; 
        public ChordAsMidiService(IMidiRepository midiRepository)
        {
            _midiRepository = midiRepository;
        }

        public async Task<Stream> GetMidiAsync(Chord chord, CancellationToken ct = default(CancellationToken))
        {
            return await _midiRepository.GetAsync(chord, ct);
        }
    }

}
