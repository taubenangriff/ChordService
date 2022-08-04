namespace ChordService.src.Contracts.Requests
{
    public class RomanNumeralAudioRequest : RomanNumeralChordRequest
    {
        public String? AudioType { get; init; } = Models.Audio.AudioType.Midi.ToString();
    }
}
