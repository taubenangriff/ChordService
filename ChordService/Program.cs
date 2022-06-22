using ChordService.ChordGeneration;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using ChordService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddJsonConsole();

builder.Services.AddSingleton<RomanNumeralService>();
builder.Services.AddSingleton<AudioService>();
builder.Services.AddResponseCaching();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); 
app.UseResponseCaching();

//some extremely lazy error handling
app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features?
        .Get<IExceptionHandlerPathFeature>()?
        .Error;
    var response = new { error = exception?.Message };
    await context.Response.WriteAsJsonAsync(response);
}));

app.MapGet("/", () => @"use /randomchord to get a fancy chord");

app.MapGet("/randomchord", ([FromServices] RomanNumeralService service) =>
{
    return service.GetRandomChord();
});

app.MapGet("/chords/roman/{numeral}/{mode}", (
    [FromServices] RomanNumeralService service, 
    RomanNumeral? numeral, 
    Mode? mode, 
    //query
    int? inversion, 
    int? transposition) =>
{
    RomanNumeralChord _chord = new RomanNumeralChord(mode ?? Mode.Major, numeral ?? RomanNumeral.One);
    return Results.Ok(service.GetChord(_chord, inversion, transposition));
});

app.MapGet("/audio/{type}/cadence", async (
    [FromServices] AudioService service, 
    int? transposition, 
    AudioType? type) =>
{
    if (type is not AudioType audioType) return Results.BadRequest();

    Stream? stream = await service.GetCadenceAsync(audioType, transposition ?? 50);
    return stream is not null ?
        Results.File(stream!, contentType: "audio", service.GetFilename(audioType), enableRangeProcessing: true) :
        Results.NotFound();
});

app.MapGet("/audio/{type}/chords/roman/{numeral}/{mode}", 
    async (
        [FromServices] AudioService service, 
        [FromServices] RomanNumeralService roman_service, 
        AudioType? type,
        RomanNumeral? numeral, 
        Mode? mode, 
        //query
        int ? transposition,
        int? inversion,
        CancellationToken cancellationToken) =>
{
    if (type is not AudioType audioType) return Results.BadRequest();

    var real_transposition = transposition ?? 50;
    RomanNumeralChord _chord = new RomanNumeralChord(mode ?? Mode.Major, numeral ?? RomanNumeral.One);
    var chord = roman_service.GetChord(_chord, inversion, real_transposition);

    Stream? stream = await service.GetSequenceAsync(new Chord[] { chord }, audioType, cancellationToken);
    return stream is not null ?
        Results.File(stream!, contentType: "audio", service.GetFilename(audioType), enableRangeProcessing: true) :
        Results.NotFound();
});

app.Run();