﻿# ChordService

A small and lightweight API to generate Chord data and sound examples using Roman Numerals.

Chords are generated in midi note representation, which means each integer is a note in 12-tone-equal temperament and middle C equals a transposition of 60.

# API definition

| **Endpoint** | **Description** | **Parameters** |
|-|-|-|
|`GET /randomchord`| gets a random chord | none |
|`GET /chords/roman/{numeral}/{mode}`| gets a chord with the specified RomanNumeral `numeral` and Mode `mode`. | `transposition`, `inversion`|
|`GET /audio/midi/chords/roman/{numeral}/{mode}`| gets a midi file that plays the chord with the specified RomanNumeral `numeral` and Mode `mode`. | `transposition`, `inversion`|
|`GET /audio/wave/chords/roman/{numeral}/{mode}`| gets a wave file that plays the chord with the specified RomanNumeral `numeral` and Mode `mode`. | `transposition`, `inversion`|

## Supported Modes
- Major
- Minor
- Augmented
- Diminished
- HalfDiminished

## Supported RomanNumerals
- One
- TwoMinor
- TwoMajor
- ThreeMinor
- ThreeMajor
- Four
- Tritone
- Five 
- SixMinor
- SixMajor
- SevenMinor
- SevenMajor

# Sample Usage 

Here is the api call that gets a I major chord on C5 in first inversion

`/chords/roman/One/Major?transposition=72`

# Credits

This project uses 

- [SimpleSynth](https://github.com/Connor14/SimpleSynth) by Connor14
- [DryWetMidi](https://github.com/melanchall/drywetmidi) by melanchall

