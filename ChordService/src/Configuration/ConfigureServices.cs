using ChordService.src.Services.Interfaces;
using ChordService.src.Services;
using ChordService.src.Repositories;
using ChordService.src.Repositories.Interfaces;

namespace ChordService.src.Configuration
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IRomanNumeralChordService, RomanNumeralChordService>();
            services.AddSingleton<IMidiSynthesizerService, MidiSynthesizerService>();
            services.AddSingleton<IChordAsMidiService, ChordAsMidiService>();
            services.AddSingleton<IMidiRepository, MidiRepository>();
            services.AddSingleton<IRomanNumeralChordRepository, RomanNumeralChordRepository>();

            return services;
        }
    }
}
