# SceneBudget — Product Requirements

## 1. Project overview and purpose

SceneBudget is a project that builds a 3D virtual stage in Unity. On the stage, an avatar moves according to the movement input it receives, the camera and lighting change through effects (an effect is a predefined change of camera and lighting), and the person operating the stage (the operator) can select camera views during recording.

The stage is built scene by scene. A scene is the space, effects, and movement of one performance that is recorded separately; it does not mean a Unity Scene file.

The purpose of this project is to make not only the finished stage reviewable but also the decisions made while building and optimizing it, together with the basis for those decisions. A visual demonstration alone does not show under which runtime and recording conditions the requirements were met, or what an optimization change altered.

Whether the requirements are met is verified by actually running the stage in the reference environment (Section 2). How much an optimization change (a change made to the stage to improve performance) altered performance is confirmed by feeding the recorded input and controls in again and comparing before and after the change.

The stage is delivered as a Unity project and a runnable build, together with setup and operating instructions, demonstration videos, and the comparison conditions, results, and decision rationale.

This document contains the requirements that apply to every scene. Each scene's content and conditions are defined by that scene's document. A scene document contains only what it adds to this document and points to the sections of this document for the common requirements.

## 2. Scope and constraints

For each scene, the work configures the virtual stage's space, background, materials, and lighting, and designs the camera views and transitions, and the camera and lighting effects, suited to the avatar's movement and the subjects to be shown. Which views and effects a scene has is defined by that scene's document.

The following functions are implemented so that the designed configuration actually runs. The avatar moves according to the movement it is given. The operator starts effects and selects camera views. The operator returns the camera and lighting to their pre-recording settings so that another take can be recorded.

The work also implements two functions: recording both the movement input the stage receives and the operator's controls, and replaying that record (the recorded input) by feeding it in again. The same input and controls have to be fed in again before and after an optimization change so that the stage before and after the change can be compared.

Verifying the built stage's visuals, functions, and runtime performance, optimizing runtime performance, and writing down the conditions, results, and decision rationale are also within scope. The work investigates the causes that affect performance and changes them, compares performance before and after the change, and verifies again that the visual and functional requirements are still met after the change.

Which movement the avatar uses in each scene, and where that movement comes from, is defined by that scene's document. There can be more than one scene, and scenes can differ in the movement they use and its source.

When a scene uses real-time tracking, an external tracking program does the tracking and SceneBudget receives and uses the values that program sends. SceneBudget receives the avatar's bone rotations and expression values. When the scene's document says so, it also receives joint positions (e.g., the wrist position). Finding a person's joint points in the images from a physical camera (pose estimation) and computing the avatar's bone rotations and joint positions from those points (mapping) are not done by SceneBudget.

The avatar's final pose is decided by SceneBudget. When it receives a joint position, SceneBudget sets the bone poses so that the avatar's joint is at the received position (e.g., when it receives a wrist position, it sets the arm's pose so that the wrist is at that position). This counts as deciding the final pose, not as mapping.

The reference environment for running and recording is one Windows PC running the Unity stage, OBS, and the input program the scene uses, all at the same time. The input program is the program that sends the scene's movement input to the stage; in a scene that uses real-time tracking it is a tracking program, which this project does not build. In a scene that uses no input program, the reference environment is the Unity stage and OBS. In this configuration, SceneBudget is responsible for the effects and controls, OBS for recording, and the input program for the movement input.

Only assets whose terms allow the use, modification, and distribution needed in this project are used for the stage and avatar.

## 3. Users and use cases

The people who use the stage are the operator, the performer, and developers and reviewers. The operator uses the stage for effects and recording. When a scene uses real-time tracking, the performer moves the avatar with their own movement. Developers and reviewers use the stage and the verification materials (the records of runtime and recording conditions, changes, measurements, and decision rationale) to check whether the stage meets the requirements and how much an optimization change altered performance. One person may take several roles.

### 3.1 Operating and recording the stage

Before recording, the operator chooses the scene to record and which of that scene's effects and views to use. The operator runs the scene's input program, checks on the running stage that the avatar moves according to the incoming values and that the camera switches when the view is changed, and then starts recording in OBS.

During recording, the operator starts the prepared camera and lighting effects and selects views to suit the subjects to be shown and the avatar's movement. The changes in effects and views, and the avatar's movement throughout, are recorded.

When the take is finished, the operator stops the recording. To record again from the beginning, the operator returns the camera and lighting to their pre-recording settings and starts a new recording. If the intended visuals or behavior do not appear, the operator corrects the cause (e.g., effect settings, view, input state) and records again.

### 3.2 Measuring and deciding on optimization changes

In an assessment run, a developer checks whether the stage meets the requirements. An assessment run is a recorded run in which the stage, OBS, and the input program all run as in the reference environment (conditions in Section 5.2). In an assessment run, the developer observes and measures visuals, functions, runtime performance, and resource usage, and records the input program used and its version and settings.

The developer investigates the causes that lower performance and changes the stage (an optimization change). How much the change altered performance is measured with a comparison run. A comparison run feeds the same recorded input (Section 2) into the stage before the change and the stage after the change, and runs and records both (conditions in Section 5.3). Because the input and controls are the same, differences in them can be ruled out as causes of a performance difference; and because a comparison run can be repeated many times without the input program or an operator, it is used to choose which candidate changes to accept. However, because a comparison run does not run the input program and the stage reads the recorded input directly, the resources that program uses are missing from the measurements. For a change selected for acceptance, an assessment run confirms again whether the changed stage meets the requirements.

The developer compares visuals, functions, runtime performance, and resource usage before and after the change, considers them together, and decides whether to accept the change and what problems remain. The criteria and what to record are in Section 5.3.

### 3.3 Using records to repeat verification

When a developer or reviewer re-checks an existing result or tests a condition that has not yet been checked, they read the verification materials, decide the conditions to test, and run again. To re-check whether the stage meets the requirements, they do an assessment run; to re-measure the performance difference before and after a change, they do a comparison run. A comparison run needs the stage, the stage and OBS settings, the test procedure, and the recorded input; it needs neither the input program nor an operator. An assessment run needs the input program actually running, so the input program noted in the records must also be available, in the recorded version and with the recorded settings; in a scene that uses real-time tracking, an input device (e.g., a physical camera) and a performer are also needed.

The developer or reviewer compares the results of the repeated run with the existing records (method in Section 5.4) and decides whether to keep or change the earlier judgments (that the requirements are met, that a change is accepted).

## 4. Requirements

### 4.1 Displaying movement, views, and effects

The stage must show on screen the avatar's movement, the camera view the operator selected, and the camera and lighting effects the operator started (the views and effects are those defined by the scene's document). The avatar's movement follows the input the scene uses (Section 2). When a scene uses real-time tracking, the avatar must move according to the received bone rotations and expression values, and when a joint position is received, the avatar's joint must be at the received position. The final pose is decided by the stage as described in Section 2. The allowed delay between an input or control and its appearance on screen, and the coordinate reference of received joint positions, are open items in Section 7.

### 4.2 Visibility and visual appearance

Each camera view and effect must show the subjects and movements to be shown so that they can be identified while the scene's movement input is being received. Lighting, materials, and background must meet the visual appearance criteria defined by the scene's document.

### 4.3 Behavior during transitions and effects, interrupted input, and additional controls

The avatar must keep moving according to the input while a camera transition or an effect is in progress. If the movement input is interrupted, the stage and the recording must continue, and the stage must detect the interruption. Values that cannot be applied to the avatar (e.g., non-numeric or out-of-range values) must not be applied. What the stage does when the operator uses a control other than the restore action (Section 4.4) during a transition or effect must be defined. The time after which input counts as interrupted, what the avatar does during the interruption, whether and how to notify the operator, and what the stage does with additional controls are open items in Section 7.

### 4.4 Restoring settings for another take

When the operator triggers the restore action, the stage must stop the camera and lighting effects in progress and return the camera and lighting to the pre-recording settings defined by the scene's document. After restoration, the operator must be able to select the initial view and start a new recording. The time allowed for restoration and what the stage does with controls during restoration are open items in Section 7.

### 4.5 Runtime and recording quality

In the reference environment (Section 2), the stage must meet the criteria for output resolution, frame rate, and sustained execution time, and the recording must meet the criteria for missed and late frames. The criteria values have not yet been decided (Section 7). The recording must contain the effects, avatar movement, and view changes during the take in the order in which they occurred.

### 4.6 Recording and replaying input and controls

The stage must record the movement input it receives and the operator's controls, and be able to produce the same movement and the same changes of view and effects by feeding the record in again in the same order and at the same time intervals as it was received. Replaying must require neither the input program nor an operator. Recorded input must remain usable after the stage has been changed. This function is used for comparison runs (Section 3).

### 4.7 Optimization comparison materials

The comparison materials for an optimization change (the verification materials recording a comparison run's conditions and results) must make it possible to judge how much performance changed and whether the visual and functional requirements are still met. The materials must distinguish the changed element, the conditions held constant, and the results before and after the change.

### 4.8 Materials for assessing requirements and repeating verification

The verification materials must link the tested scene, the stage version (the project version), the execution environment (the actual PC specification and settings of the reference environment), the input and control conditions (including the input program used with its version and settings, and the recorded input), the measurements and observations, and the decision rationale. For each scene and requirement, they must show whether it is met (tested and satisfied), unmet (tested and not satisfied), or not evaluated (not yet tested), and they must show the materials needed to repeat a test and any restrictions on using them.

## 5. Verification and success criteria

### 5.1 Connecting requirements to tests

Verification is done per scene. Each requirement in Section 4 is linked to that scene's test conditions, verification method, expected result, and the evidence to retain, and then tested. For requirements whose conditions or assessment criteria do not yet exist, define them first; if they cannot be defined or the evidence cannot be obtained, leave the requirement as not evaluated, with the reason.

### 5.2 Functional, visual, runtime, and recording verification

Runtime and recording quality (Section 4.5) and the functions in which the input program is involved (movement display, interruption detection) are assessed with an assessment run (Section 3). The results of an assessment run hold only for the input program used at the time and its version and settings. If a requirement is unmet because of the input program's load, the input program's share is not separated out; the requirement is left as unmet under those conditions. A comparison run (Section 3) is used only for comparing before and after an optimization change and for finding causes, not for assessing whether requirements are met.

#### Functional verification

Functions are checked by actually operating the stage. For each test, record the stage version, execution environment, and input and control conditions, and retain the evidence below.

| Related requirement | Conditions and method | Evidence to retain | What to assess |
|---|---|---|---|
| 4.1 Movement display | Feed the scene's input and compare the input with the avatar's movement on screen. In a scene that uses real-time tracking, compare the received values with the avatar's movement | The input fed in and its record, and the corresponding screen records | Does the avatar move according to the input under the defined conditions? |
| 4.1 View selection and effects | Select a view and start an effect, then compare what appears on screen with the selected view and with the camera and lighting changes that effect defines | Control records, the selected view and started effect, and screen records | Do the selected view and the started effect appear on screen? |
| 4.3 Movement during transitions and effects | Keep feeding input while running transitions and effects and watch the avatar's movement | Input and control records, and screen records of the transition and effect intervals | Does the avatar keep moving according to the input during transitions and effects? |
| 4.3 Interrupted input and values that cannot be applied | Stop the input program or break the connection and watch whether the stage and recording continue and whether the interruption is detected; for values that cannot be applied, replay recorded input containing such values and watch whether they are left unapplied | The time and conditions of the interruption, the stage's indication or record that it detected the interruption, and screen and recording records | Do the stage and recording continue and is the interruption detected? Are the values that cannot be applied left unapplied? |
| 4.3 Additional controls | After the expected result is defined, use additional controls during transitions and effects | The conditions and controls introduced, what the stage did, and screen records | Does the stage behave as defined? |
| 4.4 Restoration | Trigger the restore action during an effect, compare the camera and lighting settings with the scene's pre-recording settings, then select a view and start a new recording | Settings before and after restoration, control records, and screen records | Do the effects stop, do the settings return to the pre-recording settings, and can a view be selected and a new recording started? |
| 4.6 Input and control recording and replay | Record the input and controls, then replay them without the input program or an operator and compare the avatar's movement and the changes of view and effects with those at recording time. Also replay the same recorded input after the stage has been changed | The recorded input, and screen records at recording time and at replay | Are the same movement and the same changes of view and effects produced? Does the replay also work on the changed stage? |

Results are written as met or unmet against the requirement's criteria; items without conditions, criteria, or evidence are left as not evaluated, as Section 5.1 describes.

#### Visual verification

Visuals are checked after defining the subjects and movements to be shown (Section 4.2), the scene's movement, and the visual appearance criteria. Look at the reference images chosen as the basis for comparison, still images during the test, and the output during movement and camera transitions, and do not judge the behavior as a whole from still images alone. For each test, record the stage version, execution environment, view, effect, and movement conditions, and the reference images.

| Related requirement | Conditions and method | Evidence to retain | What to assess |
|---|---|---|---|
| 4.2 Visibility of subjects and movements | Feed the movement defined for each view and effect, and check the subjects and movements in still images and in the output during movement and transitions | Movement, view, and effect conditions, reference images, and image and video records | Can the subjects and movements to be shown be identified? |
| 4.2 Lighting, materials, and background | Select the defined views and start the defined effects, compare with the reference images, and also look at the output during movement, transitions, and effects | The scene's visual appearance criteria, reference images, and image and video records | Are the scene's visual appearance criteria (including the allowed differences) met? |

Results are written per tested interval and condition; items that cannot be judged because criteria or records are insufficient are left as not evaluated, as Section 5.1 describes.

#### Runtime and recording verification

Runtime and recording are checked with an assessment run. Feed movement input, start effects, and use controls for the defined duration, and look separately at the stage's output and sustained execution, OBS's missed and late frame information, and the recorded file. Record the stage version; the reference environment's PC configuration; the settings of the stage, OBS, and the input program; the input and control conditions; and the test duration.

| Related requirement | Conditions and method | Evidence to retain | What to assess |
|---|---|---|---|
| 4.5 Stage output and sustained execution | Run for the defined duration and observe and measure output resolution, runtime performance (the frame rate and frame time actually rendered), resource usage, and visuals and functions | Output checks, measurements over time, and visual and functional observations | Do the output and the visuals, functions, and performance during sustained execution meet the criteria? |
| 4.5 Missed and late frames during recording | In the same interval, look at the missed and late frame information OBS shows | OBS's missed and late frame records and the intervals with problems | Are missed and late frames, measured by the defined metrics, within the defined criteria? |
| 4.5 Recorded output | Look at the file's resolution and frame rate information, and compare playback with the input, effect, and view transition records to check order, omissions, and freezes | The recorded file, file information, compared intervals, and observations | Does the recording meet the output and quality criteria and preserve the order of effects, movement, and views? |

Neither the settings nor the file's frame rate information alone establishes actual frame delivery or recording quality. Metrics, collection methods, observation intervals, and criteria values are defined before testing.

### 5.3 Comparing optimization changes and deciding whether to accept them

How much an optimization change altered performance is measured with a comparison run. All conditions other than the changed element are kept the same: the input and controls, and the effects they trigger, are matched by replaying the same recorded input; the execution environment and recording conditions are also kept the same; and the same metrics and collection and calculation methods are used. Check the variation between repeated runs, missing measurements, and conditions that could not be matched to judge whether the comparison evidence is valid, and separate usable results from what cannot be judged, with reasons. The repeat count and validity criteria are defined before testing.

Alongside the performance comparison, run those functional and visual checks from Section 5.2 that do not need the input program, with the same criteria, in the comparison runs before and after the change, to check that visuals and functions are preserved. Write down separately how much performance changed and whether visuals and functions are preserved, then consider them together, and record whether the change is accepted or deferred, and on what basis. If performance improves but visuals or functions are not preserved, write that down. If the comparison evidence is insufficient or the developer cannot confirm that visuals and functions are preserved, defer the change and write down the items to check further. Whether the stage meets the requirements with an accepted change applied is assessed with an assessment run as described in Section 5.2.

### 5.4 Repeating verification using the provided materials

The comparison materials of Section 4.7 and the verification materials of Section 4.8 are checked by asking whether the recorded conditions and the provided materials allow a test to be done again and its results to be compared with the existing records. What a repeated run needs is listed in Section 3. Check whether the required stage version, settings, input materials (files fed into the stage other than recorded input), and procedure can be found and used as recorded, whether the changed element, the conditions held constant, and the before-and-after results can be distinguished in the comparison materials, and whether the earlier test corresponding to a new result can be identified. If information or materials are insufficient or conditions cannot be matched, write down the items that could not be checked and the limitations. Record separately whether the test could be repeated and whether its results agree with the existing records. Agreement is judged, for an assessment run, by whether the met or unmet assessment is the same, and for a comparison run, by whether the metrics are within the defined allowed range; if the results differ, write down the differences and what to check further.

### 5.5 Deciding whether the project goals are achieved

The project goals are achieved when, for each scene in the agreed scope, the Section 4 requirements are met under the conditions they define, the performance change from optimization, and its limits, can be explained with valid comparison evidence, and the deliverables of Section 6 have been provided under the delivery conditions it defines. Which scenes are included is part of the agreed scope. To assess this, link the results of Sections 5.2–5.4 to each requirement and mark each requirement as met, unmet, or not evaluated; consider together how much performance changed, whether visuals and functions are preserved, and the basis for accepting or deferring each change; and check that delivery matches the contents and conditions of Section 6. While any requirement remains unmet or not evaluated, or any delivery condition remains unmet or unchecked, do not record the goals as achieved; distinguish finished items from remaining items and write down how the remaining items affect use, delivery, repeating verification, and the interpretation of results.

## 6. Deliverables and delivery conditions

A Windows build that runs the stage is provided, together with the Unity project in which its configuration and code can be inspected, modified, and built. Which stage version each build corresponds to is stated.

The setup and operating instructions tell the reader how to run and record the stage. They cover the required execution environment, external tools, and input materials; how to obtain the input program each scene uses (see the last paragraph of this section) and how to connect it to the stage; the preparation steps; how to start effects, select views, and record another take; and known limitations. What differs per scene is added by that scene's document.

A demonstration video is provided for each scene. It shows the result of the implemented effects and controls and states the stage version and the recording conditions (including the input program used and its version), so that the provided behavior and visuals can be checked before running the stage.

Verification materials are provided for each scene. Their contents are those defined in Sections 4.7 and 4.8, and the raw measurements and observations are provided with them so that the basis for judgments can be checked.

Of what is needed to repeat verification (Section 3), the following are provided: the stage version, the stage and OBS settings, the test procedure, and the recorded input used in comparison runs (how much recorded input is provided follows the last paragraph of this section). How to obtain the rest is explained. For the input program used in assessment runs, how to obtain it is explained (see the last paragraph of this section), and its version and settings are written in the verification materials (Section 4.8). Any part of the conditions that cannot be reproduced, and its effect, is stated.

Code, assets, input materials, and recorded input are provided to the extent their usage and distribution terms allow. What cannot be provided, and why, is written under the usage restrictions in Section 4.8, and the extent to which recorded input is provided is defined by the scene's document. For anything that this project does not build (Section 2) and that must be obtained separately (e.g., the input program), the source, required version, terms, and how to obtain it are explained. All information needed to run, review, and repeat verification must be in the provided materials.

## 7. Open decisions and risks

### 7.1 Open development and verification conditions

Decisions that belong to each scene (views and effects, visual appearance criteria, the movement used, the settings to restore) are not listed as open items in this section; the scene's document defines them.

#### Details of input interruption

**Open:** the time after which the input counts as interrupted, what to do with the avatar during the interruption, and whether and how to notify the operator.

**Impact:** the conditions and expected results for testing the interruption requirement in Section 4.3 are not defined. How the avatar looks during the interruption is captured in the recording.

**Evidence needed for the decision:** the rate at which the scene's input program sends values and the way interruptions occur, and the effect of an interruption on the screen during effects and recording.

**Related sections:** 4.3; 5.2 functional verification.

#### Coordinate reference of received joint positions

**Open:** what origin and axes the received joint positions are expressed in.

**Impact:** in a scene that receives joint positions, the computation that places the joint at that position and the test conditions are not defined.

**Evidence needed for the decision:** the definition of the values sent by the scene's input program.

**Related sections:** 2; 4.1; 5.2 functional verification.

#### Additional controls, display delay, and restoration time

**Open:** what the stage does with additional controls during effects and transitions, the allowed delay between an input or control and its appearance on screen, the time allowed for restoration, and what the stage does with controls during restoration.

**Impact:** the expected results and functional test conditions for Sections 4.3 and 4.4 are not defined.

**Evidence needed for the decision:** what the operator intends to keep doing in each situation, and the response time needed for effects and recording.

**Related sections:** 4.1; 4.3; 4.4; 5.2 functional verification.

#### Criteria values and measurement conditions for output and sustained execution

**Open:** the criteria values for output resolution, frame rate, and sustained execution time, the criteria for missed and late frames in the recording, the thresholds for performance metrics, and the repeat count and validity criteria for comparison runs.

**Impact:** Section 4.5 cannot be assessed, and the validity of comparisons in Section 5.3 cannot be judged.

**Evidence needed for the decision:** for the criteria values and thresholds, the quality needed for recording and the observations and measurements from assessment runs; for the repeat count and validity criteria, the spread of measurements across repeated comparison runs.

**Related sections:** 4.5; 5.2 runtime and recording verification; 5.3.

### 7.2 Risks

#### Optimization decisions

If movement or runtime conditions cannot be matched, the effect of a change cannot be isolated, and a change that improves performance can leave visual or functional requirements unmet. Because a comparison run leaves out the resources the input program uses (Section 3), an assessment run can give different results. The checks are in Section 5.3.

#### Dependence on the values the tracking program produces

When a scene uses real-time tracking, the movement that can be shown in real time depends on the values the tracking program produces. If that program does not produce the movement the scene needs, the movement defined by the scene's document is not shown, and assessment results hold only for that program's version (Section 5.2). Related sections: 2; 4.1; 5.2.

#### Delivery and reproduction

Where and how the deliverables will be published, and the conditions for obtaining what must be obtained separately (Section 6), have not been decided. If assets or input materials cannot be used under their terms or cannot be obtained, the stage might not run or might not be distributable, and if an earlier version of the input program can no longer be obtained, an assessment run cannot be repeated. The terms of the materials and programs actually used, and any substitute materials or delivery limits needed, must be checked. Related sections: 5.4; 6.

### 7.3 Document changes after decisions

For each open item, the affected requirements, verification, and deliverables and the evidence needed for the decision are written together; for each risk, its impact, the checks, and the related sections. Once an item is decided, it is moved into the requirements of the relevant section, and any remaining limitation is written into the assessment in Section 5.5 together with its effect on running the stage and interpreting results.
