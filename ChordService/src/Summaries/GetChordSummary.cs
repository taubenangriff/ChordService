using ChordService.src.Contracts.Requests;
using ChordService.src.Contracts.Responses;
using ChordService.src.Endpoints;
using ChordService.src.Models.Chords;
using FastEndpoints;

using System.Linq;

namespace ChordService.src.Summaries
{
    public class RomanNumeralChordSummary : Summary<RomanNumeralChordEndpoint>
    {
        static String RomanNumeralDesc = $"Roman Numeral of the Chord root: \n\n - " +
            $"{String.Join("\n - ", Enum.GetValues(typeof(RomanNumeral)).Cast<RomanNumeral>().Select(x => x.ToString()))}";

        static String ModeDesc = $"Mode of the Chord: \n\n - " +
            $"{String.Join("\n - ", Enum.GetValues(typeof(ChordMode)).Cast<ChordMode>().Select(x => x.ToString()))}";

        static String TranspositionDesc = "Key Center for the Roman Numeral to relate to. value corresponds to MIDI Note values. Standard: 60 (Middle C)";

        static String InversionDesc = "Inversion of the Chord. Standard: 0";

        public RomanNumeralChordSummary()
        {
            Summary = "Returns a Chord as Midi Notes";
            Description = "Returns the Chord on the specified Roman Numeral in the specified Mode as Midi Note Heights";
            Params[nameof(RomanNumeralChordRequest.Numeral)] = RomanNumeralDesc;
            Params[nameof(RomanNumeralChordRequest.Mode)] = ModeDesc;
            Params[nameof(RomanNumeralChordRequest.Transposition)] = TranspositionDesc;
            Params[nameof(RomanNumeralChordRequest.Inversion)] = InversionDesc;
            Response<ChordResponse>(StatusCodes.Status200OK, "Successfully generated the Chord");
        }
    }
}
