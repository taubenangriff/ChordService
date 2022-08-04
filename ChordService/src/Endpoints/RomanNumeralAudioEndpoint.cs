using ChordService.src.Contracts.Requests;
using ChordService.src.Contracts.Responses;
using ChordService.src.Models.Audio;
using ChordService.src.Models.Chords;
using ChordService.src.Services.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace ChordService.src.Endpoints
{
    [HttpGet("audio/roman"), AllowAnonymous]
    public class RomanNumeralAudioEndpoint : Endpoint<RomanNumeralAudioRequest, FileResponse>
    {
        private readonly IRomanNumeralChordService _romanNumeralChordService;
        private readonly IChordAsMidiService _chordAsMidiService;
        private readonly IMidiSynthesizerService _midiSynthesizerService;

        public RomanNumeralAudioEndpoint(
            IRomanNumeralChordService romanNumeralChordService,
            IChordAsMidiService chordAsMidiService,
            IMidiSynthesizerService midiSynthesizerService)
        {
            _romanNumeralChordService = romanNumeralChordService;
            _chordAsMidiService = chordAsMidiService;
            _midiSynthesizerService = midiSynthesizerService;
        }

        public override async Task HandleAsync(RomanNumeralAudioRequest request, CancellationToken ct)
        {
            //fuck the compiler... also move this to somewhere else in the future
            //parse enums
            RomanNumeral numeral = default(RomanNumeral);
            AudioType audiotype = default(AudioType);
            ChordMode mode = default(ChordMode);
            bool parse_success = Enum.TryParse(request.Mode, out mode)
                                && Enum.TryParse(request.Numeral, out numeral)
                                && Enum.TryParse(request.AudioType, out audiotype);
            if (!parse_success)
            {
                await SendErrorsAsync(StatusCodes.Status400BadRequest, ct);
                return;
            }

            //get chord
            RomanNumeralChord RomanNumeralChord = new RomanNumeralChord(mode, numeral);
            var chord = await _romanNumeralChordService.GetChordAsync(RomanNumeralChord, request.Inversion, request.Transposition);

            //write the generated chord to a midi stream
            var resultstream = await _chordAsMidiService.GetMidiAsync(chord);

            //if requested, synthesize it here.
            if (audiotype == AudioType.Wave)
            {
                resultstream = await _midiSynthesizerService.GetSynthesizedWaveAudioAsync(resultstream, ct);
            }
            await SendStreamAsync(resultstream, audiotype == AudioType.Midi ? "audio.mid" : "audio.wav", cancellation : ct) ;
        }
    }
}
