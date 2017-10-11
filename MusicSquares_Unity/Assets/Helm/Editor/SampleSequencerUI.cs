// Copyright 2017 Matt Tytel

using UnityEditor;
using UnityEngine;

namespace Helm
{
    [CustomEditor(typeof(SampleSequencer))]
    class SampleSequencerUI : Editor
    {
        const float keyboardWidth = 30.0f;
        const float scrollWidth = 15.0f;

        SerializedProperty length;
        SerializedProperty allNotes;

        SequencerUI sequencer = new SequencerUI(keyboardWidth, scrollWidth + 1);
        SequencerPositionUI sequencerPosition = new SequencerPositionUI(keyboardWidth, scrollWidth);
        SequencerVelocityUI velocities = new SequencerVelocityUI(keyboardWidth, scrollWidth);

        float positionHeight = 10.0f;
        float velocitiesHeight = 40.0f;
        float sequencerHeight = 400.0f;
        const float minWidth = 200.0f;

        void OnEnable()
        {
            length = serializedObject.FindProperty("length");
            allNotes = serializedObject.FindProperty("allNotes");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Color prev_color = GUI.backgroundColor;
            GUILayout.Space(5f);
            SampleSequencer sampleSequencer = target as SampleSequencer;
            Sampler sampler = sampleSequencer.GetComponent<Sampler>();
            if (sampler)
            {
                sequencer.minKey = sampler.GetMinKey();
                sequencer.maxKey = sampler.GetMaxKey();
            }
            else
            {
                sequencer.minKey = 0;
                sequencer.maxKey = Utils.kMidiSize - 1;
            }

            Rect sequencerPositionRect = GUILayoutUtility.GetRect(minWidth, positionHeight, GUILayout.ExpandWidth(true));
            float seqHeight = Mathf.Min(sequencerHeight, sequencer.GetMaxHeight());
            Rect rect = GUILayoutUtility.GetRect(minWidth, seqHeight, GUILayout.ExpandWidth(true));
            Rect velocitiesRect = GUILayoutUtility.GetRect(minWidth, velocitiesHeight, GUILayout.ExpandWidth(true));

            if (sequencer.DoSequencerEvents(rect, sampleSequencer, allNotes))
                Repaint();
            if (velocities.DoVelocityEvents(velocitiesRect, sampleSequencer))
                Repaint();

            sequencerPosition.DrawSequencerPosition(sequencerPositionRect, sampleSequencer);
            velocities.DrawSequencerPosition(velocitiesRect, sampleSequencer);

            if (rect.height == seqHeight)
                sequencer.DrawSequencer(rect, sampleSequencer, allNotes);
            GUILayout.Space(5f);
            GUI.backgroundColor = prev_color;

            if (GUILayout.Button("Clear Sequencer"))
            {
                for (int i = 0; i < allNotes.arraySize; ++i)
                {
                    SerializedProperty noteRow = allNotes.GetArrayElementAtIndex(i);
                    SerializedProperty notes = noteRow.FindPropertyRelative("notes");
                    notes.ClearArray();
                }
            }

            EditorGUILayout.IntSlider(length, 1, Sequencer.kMaxLength);
            serializedObject.ApplyModifiedProperties();
        }
    }
}