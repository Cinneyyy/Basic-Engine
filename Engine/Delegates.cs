﻿using System.Windows.Forms;
using Engine.Audio;

namespace Engine;

public delegate void GenericCallback();
public delegate void GenericCallback<T>(T arg);
public delegate void GenericCallback<T1, T2>(T1 arg1, T2 arg2);
public delegate void GenericCallback<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);

public delegate void GameLoopUpdateCallback(float deltaTime);
public delegate void ActorMoveCallback(Vec2 moveDelta);

public delegate void InputKeyCallback(Keys key);
public delegate void InputWheelScrollCallback(int delta);
public delegate void InputCursorMovedCallback(Vec2i screen, Vec2 world);
public delegate void InputCursorClickedCallback(MouseButton mb, Vec2i pixelPos, Vec2 worldPos);

public delegate void OnAudioFileStop(AudioFile audio, bool wasManualStop);