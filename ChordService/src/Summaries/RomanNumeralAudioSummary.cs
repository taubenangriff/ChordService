using ChordService.src.Contracts.Requests;
using ChordService.src.Contracts.Responses;
using ChordService.src.Endpoints;
using ChordService.src.Models.Audio;
using ChordService.src.Models.Chords;
using FastEndpoints;

using System.Linq;

namespace ChordService.src.Summaries
{
    public class RomanNumeralAudioSummary : Summary<RomanNumeralAudioEndpoint>
    {
        public static String RomanNumeralDesc = $"Roman Numeral of the Chord root: \n\n" + EnumSummary.GetEnumDocumentation<RomanNumeral>();
        public static String ModeDesc = $"Mode of the Chord: \n\n" + EnumSummary.GetEnumDocumentation<ChordMode>();
        public static String TypeDesc = $"Audio Type: \n\n" + EnumSummary.GetEnumDocumentation<AudioType>();

        public static String TranspositionDesc = "Key Center for the Roman Numeral to relate to. value corresponds to MIDI Note values. Standard: 60 (Middle C)";
        public static String InversionDesc = "Inversion of the Chord. Standard: 0";

        public RomanNumeralAudioSummary()
        {
            Summary = "Generates Audio for a chord based on roman numerals";
            Description = "Returns the Chord on the specified Roman Numeral in the specified Mode as Audio";
            Params[nameof(RomanNumeralAudioRequest.Numeral)] = RomanNumeralDesc;
            Params[nameof(RomanNumeralAudioRequest.Mode)] = ModeDesc;
            Params[nameof(RomanNumeralAudioRequest.Transposition)] = TranspositionDesc;
            Params[nameof(RomanNumeralAudioRequest.Inversion)] = InversionDesc;
            Params[nameof(RomanNumeralAudioRequest.AudioType)] = TypeDesc;
            Response<ChordResponse>(StatusCodes.Status200OK, "Successfully generated the Audio");
        }
    }
}
