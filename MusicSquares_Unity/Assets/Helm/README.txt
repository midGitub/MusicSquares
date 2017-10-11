# Helm Unity Plugin

Helm is a synthesizer that you can play live inside of Unity.

## Getting started

### Editor

In an AudioMixerGroup, load the Helm plugin.
Pick a patch from the list.
Make sure the speaker icon is clicked at the top of the Scene window.

### Trigger notes in scene

In your scene load the HelmController script onto an empty GameObject.
Hook up the AudioSource target mixer that appears to the AudioMixerGroup that the Helm instance is on.
Make sure the "Channel" value matches the "Channel" value in the Helm instance.
You can call NoteOn and NoteOff on this script to trigger notes from code. Just pass the midi key you would like to play.

### Make music with the sequencer

In your scene load the HelmSequencer script onto an empty GameObject.
Like above, hook up the AudioSource target mixer that appears to the AudioMixerGroup that the Helm instance is on.
Click to create a note, click again to delete. Click and drage to create a repeating sequencer.
Play the scene to hear the sequence.

# TODO

## Features

PlayNoteScheduled
Expose aftertouch, pitch bend, modulation wheel controls
Variable BPM without resyncing
Sequencer time signature/division size
Take unused AudioSource first instead of round robin.
UI widgets for better tweaking in Unity?
iOS/android/ps4/xbox support?

## Known Issues

Sample Sequencer is inefficient with a lot of notes
Sequencer changes lock audio thread
Sometimes playhead is out of sync?
BPM Change notes stay around
Low refresh rate for playhead updates

