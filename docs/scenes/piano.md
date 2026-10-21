# SceneBudget — Piano Scene Requirements

## 1. Scene overview

This scene is a live performance in which the avatar plays a virtual piano on the stage. The movement of the performer's face, upper body, and playing hands is applied to the avatar by real-time tracking. When the performer presses keys on the computer keyboard, the avatar presses the piano keys mapped to them, and the piano sound plays.

This document contains only what this scene defines or adds on top of the common PRD (`docs/PRD.md`).

## 2. Scope and input

In addition to the work in Section 2 of the common PRD, the work for this scene builds a virtual piano whose keys move with the keyboard keys mapped to them. The stage produces the piano sound. The spacing of the virtual piano keys matches the spacing of the keyboard keys. The tracked hand's finger spacing then matches the piano key spacing, and the correction (defined below) pulls the fingers a shorter distance.

This scene uses three inputs:

- the values tracked from the front camera's image of the performer: face, head, torso, and shoulders (the front tracking values)
- the values tracked from the image from a camera above the keyboard, which looks down at the playing hands: wrist position, hand rotation, and finger rotations (the hand tracking values)
- the key input from the keyboard

The tracked values are sent by the tracking program (common PRD Section 2). The wrist position is a joint position as defined in Section 2 of the common PRD; this scene receives it. In this document, "key" on its own means a keyboard key.

Because the playing hands are not visible to the front camera, the hands' movement comes from the hand tracking values. The piano key played and the sound come from the key input. At the moment a keyboard key is pressed, the finger that pressed it is moved onto the piano key mapped to that keyboard key. This is called the correction (the rule for choosing the finger is open; Section 7.1, item 3). This scene has no predefined playing animation.

The front tracking values and the hand tracking values affect the same chain of bones, from the shoulders to the hands. Because the stage decides the final pose (common PRD Section 2), this scene applies the front tracking values to the torso, shoulders, and head first. It then sets the arm pose so that the wrist is at the received position. After that, it applies the hand rotation and finger rotations and adds the correction from the key input. If the two sets of values were applied independently of each other, the hands would come off the piano keys when the torso leans.

This scene's input program (common PRD Section 2) is the tracking program for the images from both cameras. The input devices of this scene (common PRD Section 3.3) are the front camera, the camera above the keyboard, and the keyboard.

The piano model and the piano sound data are assets. Their terms must meet the asset condition in Section 2 of the common PRD.

## 3. Use cases

In this scene, the performer sits facing the front camera, with the hands under the camera above the keyboard, and plays on the keyboard. The operator is the same person (one person in several roles, common PRD Section 3).

During the performance, the operator uses controls without taking the hands off the keyboard. The playing hands are tracked, so a hand that leaves the keyboard is shown leaving it.

Controls use one control key, pressed with the thumb. The keyboard keys mapped to the piano keys (the playing keys) are in the letter rows. While playing the letter rows, the thumb stays on the row below and does not reach the letter keys. So four fingers play and the thumb is free.

The pieces played must be playable with four fingers within the pitch range of the key mapping table. The range and the table are open (Section 7.1, item 3).

Because there is only one control key, effects and view changes are prepared before the performance as an ordered cue list. During the performance, pressing the control key advances the cue list to the next cue. One cue defines a lighting state and a view (Section 4.2). In this scene, the view selection of Section 3.1 of the common PRD is limited to following the cue list in order. There is no control for choosing an arbitrary view during the performance.

To the checks before recording (common PRD Section 3.1), the operator adds the following. Are the face and torso in the front camera's image? Are the hands on the keyboard in the image from the camera above the keyboard? Does a key press move a piano key and play its sound? Does the control key advance the cue list?

A playing hand may leave the keyboard and the field of view of the camera above the keyboard (e.g., when the performer lifts a hand between pieces). Then the hand tracking values show that the hand is absent, and when the hand returns, it is tracked again. How the tracking values indicate the absence depends on the tracking program in use (Section 7.2). What the avatar's hand does in the meantime is defined in Section 4.3.

## 4. Requirements

Only what this scene adds to the requirements in Section 4 of the common PRD is written here. The subsection numbers are those of the common PRD subsections the additions belong to.

### 4.1 Applying movement

The front tracking values must be applied to the head, face, torso, and shoulders. The hand tracking values must be applied to the wrist, hand, and fingers. The arm pose must be set in the order given in Section 2: apply the torso values first, then set the arm so that the wrist is at the received position. While the performer's hand is on the keyboard, the avatar's hand must be on the piano keys. The lower body is not tracked and stays in a seated pose.

When a key is pressed, the mapped piano key must move and its sound must play without waiting for the tracking values. The allowed delay is open; see "Additional controls, display delay, and restoration time" in Section 7.1 of the common PRD. The finger that pressed the key must be shown on the pressed piano key. The control key must not move a piano key, play a sound, or trigger the correction.

When the correction pulls a finger, and when the finger returns to its tracked position after the key is released, the finger and wrist must not move farther from one frame to the next than the allowed value. Movement beyond that value is called a jump. The allowed value is open (Section 7.1, item 1).

### 4.2 Views, effects, and visual criteria

This scene has three views: the full view (stage, piano, and avatar), the face-and-upper-body view, and the hands-and-keys view. The effects of this scene are the cues of the cue list. One cue defines a lighting state and a view; advancing to a cue changes the lighting and the view to those it defines.

In the full view, the position and pose of the avatar, the position of the piano, and the layout of the stage must be identifiable. In the face-and-upper-body view, the facial expression and the movement of the head and torso must be identifiable. In the hands-and-keys view, each finger and each pressed piano key must be identifiable. Each view has a reference image. The reference images are open (Section 7.1, item 2).

The visual appearance criteria for lighting, materials, and background are open (Section 7.1, item 2).

### 4.3 When a hand is not tracked

While a hand is not tracked, the sound must still play and the piano keys must still move according to the key input. During a loss of tracking shorter than the hold time, that hand and arm hold their last pose. If the loss lasts longer, the hand moves over the transition time into a resting pose on the knee. When the hand is tracked again, it must follow the tracking again. While the last pose is held, the correction is still applied when a key is pressed; once the hand is in the resting pose, it is not applied. The hold time and the transition time are open (Section 7.1, item 1). Interruption of the tracking input itself, as opposed to a hand leaving the camera's view, is covered by Section 4.3 of the common PRD.

### 4.4 Restoration

The pre-recording settings of this scene (common PRD Section 4.4) are the state of the first cue. When the stage receives the restore action, it returns the lighting and the view to the first cue's state and resets the cue position to the first cue. It also releases all pressed piano keys and stops the sound. The tracking values keep arriving; restoration does not affect them. The restore action must not be triggered by mistake during the performance. It is therefore received through an input other than the playing keys and the control key. The initial view after restoration (common PRD Section 4.4) is the view of the first cue.

### 4.5 Sound and recording

The recording must contain the piano sound the stage produces, together with the video. The time difference between the sound and the movement of the piano key and finger must be within the allowed value. The allowed value is open (Section 7.1, item 1).

### 4.6 Recording and replaying the playing keys

The key input from the playing keys must also be recorded on the same timeline as the tracking values and replayed with them. In the replay, the piano key movements, the sound, and the correction must occur as they did at recording time. The control key is an operator control and is covered by the control recording of Section 4.6 of the common PRD.

Nothing is added to Section 4.7 (optimization comparison materials).

### 4.8 Verification materials

To the verification materials (common PRD Section 4.8), this scene adds the following:

- the position and height of the two cameras
- what was played
- the version of the key mapping table
- the versions of the piano model and the sound data
- the reference image of each view
- the measurements of Section 5: jumps, the times and distances when a hand is not tracked, the time difference between sound and video, and the tracking delay

The camera positions and heights and what was played are conditions that affect the tracking results.

## 5. Verification

In an assessment run (common PRD Section 3.2), the performer actually plays.

In functional verification (common PRD Section 5.2), the developer checks the following by actually operating the stage:

- Does a key press move the piano key, play the sound, and apply the correction?
- Does the control key leave the piano keys, the sound, and the correction untouched?
- Does the control key advance the cue list in order?
- Does restoration return the stage to the state described in Section 4.4?
- When a hand is lifted out of view and brought back, does the avatar follow the sequence in Section 4.3 (hold, move to the resting pose, return)?
- When recorded input that includes the playing keys is replayed, do the piano key movements, the sound, the correction, and the cue changes occur as at recording time?

In visual verification (common PRD Section 5.2), the identification criteria of Section 4.2 are checked for each of the three views, using the reference image and the video during playing. For the hands-and-keys view, the video must come from actual playing.

Jumps (Section 4.1) are measured in the recorded file as the distance a finger or wrist moves from one frame to the next. The file's frame rate is recorded with them. The behavior when a hand is not tracked (Section 4.3) is measured as the number of frames of the hold and of the transition. The distance per frame during the transition is measured as well. Until the allowed values are decided, the measurements and observations are recorded and the items are left as not evaluated.

The time difference between sound and video (Section 4.5) is measured in the recorded file. It is the difference between the moment the sound starts and the frame in which the piano key moves and the finger touches it.

The tracking delay is measured by filming the performer's hand and the stage's screen together with a separate camera. The larger the delay, the farther the correction has to pull the finger, so this value is evidence for the allowed jump (Section 7.1, item 1).

The replay in a comparison run (common PRD Section 3.2) includes the playing keys and the control key. So only the tracking program's load is absent, while the load from the piano key movement, the sound, and the cue changes is present.

## 6. Deliverables

The demonstration video (common PRD Section 6) shows the three views and the cue changes, the playing hands and the piano keys, and the piano sound. Its stated recording conditions also include the position and height of the two cameras and the version of the key mapping table.

The recorded input used to repeat verification consists of the tracking values from the two cameras and the key input (playing keys and control key). It contains no camera video. How much of the recorded input is provided is open (Section 7.1, item 5).

To the setup and operating instructions (common PRD Section 6), the following is added for this scene:

- how to check the placement, height, and field of view of the two cameras
- the settings of the tracking program
- which key is the control key
- how to create and change the key mapping table and the cue list

The input materials of this scene (common PRD Section 5.4) are the key mapping table and the cue list. The piano model and the sound data are assets (Section 2); their source and terms are stated.

## 7. Open decisions and risks

Only the open items, risks, and possible later changes of this scene that do not overlap with Section 7 of the common PRD are written here. The items follow the format of Section 7.1 of the common PRD.

### 7.1 Open items

#### Item 1 — Allowed values and times

**Open:** the allowed jump (Section 4.1); the hold time and transition time when a hand is not tracked (Section 4.3); the allowed time difference between sound and video (Section 4.5).

**Effect:** the three requirements cannot be assessed, and the measurements of Section 5 remain observations only.

**Evidence needed for the decision:** the tracking delay and the sound-to-video time difference measured in assessment runs (Section 5), and the length and frequency of hand tracking losses observed in assessment runs.

**Related sections:** 4.1; 4.3; 4.5; 5.

#### Item 2 — Visual appearance criteria and reference images

**Open:** the visual appearance criteria for lighting, materials, and background, and the reference image of each view (Section 4.2).

**Effect:** until they are defined, the visual verification of Section 5 (for Section 4.2) cannot be done.

**Evidence needed for the decision:** images of the built stage.

**Related sections:** 4.2; 5.

#### Item 3 — Key mapping table, pitch range, and correction rule

**Open:** which piano key each keyboard key plays (the mapping table), the pitch range, and the rule for choosing the finger in the correction. Placing the playing keys in the letter rows (Section 3) is decided. Because the fingers are tracked, no fixed fingering (a finger assigned to each key in advance) is needed; the open rule is how to choose, among the tracked fingers, the one the correction moves.

**Effect:** the test conditions for the piano key and correction requirements of Section 4.1 are not defined.

**Evidence needed for the decision:** the key layout of the keyboard and the pitch range of the pieces to be played.

**Related sections:** 2; 4.1; 4.8; 6.

#### Item 4 — Expression values and bones used

**Open:** which of the received expression values and bones are used.

**Effect:** the scope of Section 4.1 and the identification criteria of the face-and-upper-body view (Section 4.2) are not defined.

**Evidence needed for the decision:** the values the tracking program in use produces.

**Related sections:** 4.1; 4.2.

#### Item 5 — Extent of recorded input provided

**Open:** how much of the recorded input (Section 6) is provided.

**Effect:** which recorded input is delivered under Section 6, and the usage restrictions to write under Section 4.8 of the common PRD, are not defined.

**Evidence needed for the decision:** the extent of the performer's consent.

**Related sections:** 6.

The coordinate reference of the received wrist position is covered by "Coordinate reference of received joint positions" in Section 7.1 of the common PRD. Pressing the control key while a cue change is in progress is covered by the additional controls in "Additional controls, display delay, and restoration time" in the same section.

### 7.2 Risks

#### Dependence on the hand tracking values

This scene requires two values from the hand tracking values: the wrist position and an indication that a hand is absent. Both are outside the values that the stage receives by default under Section 2 of the common PRD (bone rotations and expression values). A tracking program that produces them is needed. Without it, the hand requirements of Section 4.1 and Section 4.3 remain not evaluated. This is this scene's instance of the risk "Dependence on the values the tracking program produces" in Section 7.2 of the common PRD. Check: before building, confirm from the output definition of the tracking program to be used that it produces both values. Related sections: 2; 3; 4.1; 4.3; 5.

#### Precision in the hands-and-keys view

In the close-up of the hands-and-keys view, the precision of the finger tracking may not be enough to meet the identification criteria of Section 4.2. Check: after building, check the video during actual playing (visual verification, Section 5). Related sections: 4.2; 5.

### Possible later changes

Camera movement and visual effects other than lighting. They can be added after the three views and the cue list have been verified. Adding them changes the views and cues of Section 4.2, the reference images, and the scope of visual verification in Section 5.

A physical piano keyboard. The key input for the playing keys could come from a physical piano keyboard instead of the computer keyboard. What changes:

- the source of the key input
- the spacing of the virtual piano keys (Section 2)
- the field of view and height of the camera above the keyboard, because the keys are wider
- the use of the thumb
- the control input

What does not change: the stage still produces the sound, and the hand and torso tracking and the cue method stay the same.
