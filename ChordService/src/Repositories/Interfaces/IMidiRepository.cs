using ChordService.src.Models.Chords;

namespace ChordService.src.Repositories.Interfaces
{
    public interface IMidiRepository
    {
        public Task<Stream> GetAsync(Chord c, CancellationToken ct = default(CancellationToken));
    }
}
