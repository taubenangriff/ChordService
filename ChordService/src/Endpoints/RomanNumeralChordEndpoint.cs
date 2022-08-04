using ChordService.src.Contracts.Requests;
using ChordService.src.Contracts.Responses;
using ChordService.src.Services.Interfaces;
using ChordService.src.Models.Chords;
using Microsoft.AspNetCore.Authorization;

using FastEndpoints;

namespace ChordService.src.Endpoints
{
    [HttpGet("chords/roman/{Numeral}/{Mode}"), AllowAnonymous]
    public class RomanNumeralChordEndpoint : Endpoint<RomanNumeralChordRequest, ChordResponse>
    {
        private readonly IRomanNumeralChordService _romanNumeralChordService;

        public RomanNumeralChordEndpoint(IRomanNumeralChordService romanNumeralChordService)
        {
            _romanNumeralChordService = romanNumeralChordService;
        }

        public override async Task HandleAsync(RomanNumeralChordRequest request, CancellationToken ct)
        {
            RomanNumeral numeral = default(RomanNumeral);
            ChordMode mode = default(ChordMode);
            bool parse_success = Enum.TryParse(request.Mode, out mode)
                                && Enum.TryParse(request.Numeral, out numeral);
            if (!parse_success)
            {
                await SendErrorsAsync(StatusCodes.Status400BadRequest, ct);
                return;
            }

            RomanNumeralChord RomanNumeralChord = new RomanNumeralChord(mode, numeral);
            var chord = await _romanNumeralChordService.GetChordAsync(RomanNumeralChord, request.Inversion, request.Transposition!);
            await SendOkAsync(new ChordResponse() { Chord = chord }, ct);
        }
    }
}
