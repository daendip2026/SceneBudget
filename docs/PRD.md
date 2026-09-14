# SceneBudget — Product Requirements

**Status:** Requirements draft; detailed conditions remain open.

## 1. Project overview and purpose

SceneBudget is a project to build a 3D virtual stage in Unity. The stage displays an avatar's movement and lets an operator run camera and lighting effects or select camera views during recording. The project provides a case in which the implementation and the decisions made during development and optimization can be reviewed together.

A visual demonstration alone does not establish the operating and recording conditions under which the stage meets its functional, visual quality, and performance requirements, or the effects of optimization changes. The project therefore implements the intended effects and controls, and optimizes runtime performance while preserving visual quality and functional requirements. Requirements and optimization effects are verified through observation and measurement with the stage and OBS, the recording tool, running together, followed by comparisons under matched conditions before and after changes.

The stage is provided as a Unity project whose configuration and code can be inspected and modified, and as a runnable build. It includes setup and operating instructions, a demonstration video, and comparison conditions, results, and decision rationale. Stage operators can run the stage and use its effects in recordings. Developers and reviewers can use the comparison evidence to judge which stage configurations and optimization changes to use under the verified conditions, repeat tests under recorded conditions, or test conditions that have not yet been evaluated.

## 2. Scope and constraints

The work includes configuring the virtual stage's space, background, materials, and lighting, and designing camera views, transitions, and camera and lighting effects suited to the avatar's movement and the subjects being recorded. Implementation includes displaying avatar movement and providing controls to start effects and select camera views. It also includes restoring camera and lighting settings to their values before recording so that another take can be recorded.

The scope includes checking visual quality, functionality, and runtime performance, and investigating causes that affect performance using observations and measurements. Changes arising from those investigations are applied and compared with the previous configuration, followed by verification that visual quality and functional requirements are still met. The work also includes documenting comparison conditions, results, and reasons for adopting or deferring changes.

Recording content and avatar movement are not limited to a single scenario or fixed action. The supported range of movement and the input method used to convey that movement to the avatar will be defined separately. Whether to integrate real-time motion input remains open. Section 7 explains how these decisions affect development and verification scope and what evidence is needed to make them.

The reference environment runs the Unity stage and OBS together on one Windows PC. This project implements the stage's effects and controls; OBS records the stage's output. Assets for the stage and avatar are selected according to whether their terms permit the use, modification, and distribution needed for their intended use in the project.

## 3. Users and use cases

A stage operator uses the stage to control effects and make recordings. Developers and reviewers use the stage and verification materials to assess requirements and the effects of changes. One person may perform several roles. The following use cases describe three purposes: operating and recording the stage, measuring performance and judging optimization changes, and using records to repeat verification.

### 3.1 Operating and recording the stage

The operator chooses effects and camera views for the intended recording, runs the stage, and checks that avatar movement appears on screen and camera transitions behave as intended before starting OBS recording. During recording, the operator runs the prepared camera and lighting effects and selects views according to the subjects to show and the avatar's movement. The stage displays the avatar's movement and changes in effects and views, and OBS records that output.

At the end of the take, the operator stops OBS recording. To record again from the beginning, the operator uses the restore function to return the camera and lighting to their settings before recording, selects the initial view, and starts a new recording. If the intended visuals or behavior do not appear, the operator checks the conditions in which the problem occurred, makes the necessary adjustments, and records again.

### 3.2 Measurement and optimization decisions

To check whether the stage meets requirements or determine where to optimize it, a developer defines the execution environment, movement input, effects, and control conditions. The developer runs the stage and OBS together under those conditions and observes and measures visual quality, functional behavior, runtime performance, and resource usage during recording. The results inform an investigation of causes affecting performance. The developer then applies a proposed change and repeats execution and recording, matching conditions except for the changed element, to obtain evidence for a before-and-after comparison.

The developer compares performance using the collected evidence and checks that visual quality and functional requirements are preserved to decide whether to adopt the change and identify remaining problems. Comparison conditions, results, and rationale are recorded even when there is no improvement or requirements are not met. If the evidence is insufficient to judge the effect of a change or whether requirements are met, the adoption decision is deferred, with the missing evidence and further checks documented.

### 3.3 Using records to repeat verification

A developer or reviewer examines recorded execution conditions, changes, and results to check a previous result or test conditions that have not yet been evaluated. To verify a previous result, they prepare to reproduce its recorded conditions. For a new test, they identify what to change from the previous conditions. After obtaining the required project version, settings, and input materials, they run the stage under the defined conditions and observe and measure the results.

The developer or reviewer compares the results with the existing records, taking differences in test conditions into account when updating judgments about requirements and the adoption of optimization changes. If results differ, they record the conditions and results that differ and what remains unexplained. If required materials cannot be obtained or conditions cannot be reproduced, they record which items could not be checked and why. These records inform the next materials to examine or conditions to test.

## 4. Requirements

### 4.1 Displaying movement, views, and effects

- The stage must display the avatar's movement in its output.
- When the operator selects a camera view, the stage must display that view.
- When the operator starts camera and lighting effects, the stage must display the corresponding camera and lighting changes.

**Open conditions:** The movement input method and supported range, the specific views and effects, and the allowed delay between input or operator action and its appearance on screen have not been defined. These conditions must be specified before assessing each requirement. Section 7 describes the evidence needed for these decisions.

### 4.2 Visibility and visual appearance

- Each camera view and effect must show the intended subjects and movements clearly enough to identify them under the supported movement conditions.
- The stage's lighting, materials, and background must meet the visual appearance criteria defined for the view and effects in use.

**Open conditions:** Supported movement conditions, the subjects and movements to show in each view and effect, the body parts or features needed for identification, and on-screen visibility criteria remain open. Reference images and allowed differences for lighting, materials, and background appearance are also open. These conditions must be specified before assessing each requirement. Section 7 describes the evidence needed for these decisions.

### 4.3 Behavior and input handling during transitions and effects

- The stage must continue displaying avatar movement while camera transitions or camera and lighting effects are in progress.

**Open behavior:** How to handle an ongoing action and a new command when the operator selects another view or starts additional effects during a transition or effect has not been defined. The response to an interruption of avatar movement input is also open. The expected result for each situation must be defined before specifying its behavior as a requirement. The restore action for another take is covered separately in Section 4.4.

**Open conditions:** Movement input support and allowed delay follow the open conditions in Section 4.1. Conditions for detecting interrupted input will be specified after selecting the input method. Section 7 describes the evidence needed for these decisions.

### 4.4 Restoring settings for another take

- When the operator invokes the restore action for another take, the stage must stop camera and lighting effects that are in progress.
- The restore action must return camera and lighting settings to their values before recording.
- Once restoration is complete, the operator must be able to select the initial view for a new take and start camera and lighting effects again.

**Open conditions:** The camera and lighting settings to restore, their reference values, the allowed time to complete restoration, and the response to additional commands during restoration remain open. These conditions must be specified before assessing the restored settings and the availability of subsequent controls. Section 7 describes the evidence needed for these decisions.

### 4.5 Runtime and recording quality

The following requirements apply when the stage and OBS run and record together on the same Windows PC, as specified in Section 2.

- The stage output and the recorded output must each meet their defined resolution and frame rate requirements.
- For the defined duration of sustained execution, the stage must meet visual quality, functional, and runtime performance requirements, and the OBS recording must meet its defined recording quality criteria.
- The recording must include camera and lighting effects, avatar movement, and view changes in the order in which they appeared in the stage output during recording.

**Open conditions:** The reference PC's configuration at test time, stage and OBS settings, the target resolution and frame rate for each output, sustained execution duration, and performance and resource metrics and thresholds have not been finalized for these requirements. Metrics and thresholds for recording quality, including missing frames and stutter, also remain open. These conditions must be specified before assessing compliance. Section 5 covers verification methods; Section 7 covers the evidence needed to decide the conditions.

### 4.6 Optimization comparison materials

- Comparison materials must distinguish the element changed for optimization from the execution environment, input, effects, operator actions, and recording conditions held constant for comparison.
- Performance measurements and visual quality and functional checks from before and after a change must be linked to the corresponding change and test conditions.
- Comparison materials must support an adoption decision by allowing performance changes and compliance with visual quality and functional requirements to be considered together. Missing results or conditions that could not be matched must be identified, along with their limits on the judgment.

**Related conditions:** Detailed criteria for performance, visuals, and functionality will be specified through the open conditions in Sections 4.1–4.5. Section 5 covers how to assess the validity of comparison evidence. Section 4.7 covers materials for assessing individual requirements and repeating verification.

### 4.7 Materials for assessing requirements and repeating verification

- For each requirement, verification materials must link the tested stage version, execution environment, input, effects, operator actions, and recording conditions to measurements, observations, and the rationale for the assessment.
- Verification materials must state whether each requirement is **met**, **unmet**, or **not evaluated**, with supporting evidence. Items that have not been tested, or lack sufficient criteria or evidence for a judgment, must be marked **not evaluated**, with a reason.
- Verification materials must identify the project version, settings, input materials, and procedure needed to repeat verification. Restrictions on using or obtaining materials, or conditions that cannot be reproduced, must be identified with their impact on repeating verification.

**Related conditions:** Section 5 covers verification methods and assessment procedures. Section 6 covers the materials to provide and instructions for obtaining external materials. Requirements with open detailed conditions are assessed after those conditions are specified.

## 5. Verification and success criteria

### 5.1 Connecting requirements to tests

Before verification, connect each requirement in Section 4 to test conditions, a verification method, an expected result, and the evidence to retain. Specify expected results so that compliance can be judged. If test conditions or assessment criteria remain open, first complete the relevant requirement. Items whose conditions or criteria remain undefined, or for which the necessary evidence cannot be obtained, are recorded as not evaluated, with reasons.

### 5.2 Functional, visual, runtime, and recording verification

#### Functional verification

Verify functionality through actual operation after specifying the input and control conditions and expected results in Sections 4.1, 4.3, and 4.4. Link each test to the stage version, execution environment, and input and control conditions used, and retain evidence for the results below.

| Related requirement | Test conditions and method | Evidence to retain | Result to assess |
|---|---|---|---|
| 4.1 Movement display | Apply supported movement input and compare the input with the avatar's movement on screen. | Input materials or a record of the input, and corresponding screen records | Is the input movement displayed under the defined conditions? |
| 4.1 View selection and effects | Select views and start effects; compare the selected views and effects with the changes on screen. | Operator actions, selected views and effects, and corresponding screen records | Are the selected views and started effects displayed? |
| 4.3 Movement during transitions and effects | Maintain movement input while running camera transitions and effects; check avatar movement throughout them. | Input and operator actions, and screen records covering transitions and effects | Does movement continue to be displayed during transitions and effects? |
| 4.3 Additional commands and interrupted input | Once expected responses are defined, separately test additional commands during transitions or effects and interruption of movement input. | Conditions and actions introduced, handling results, and screen records | Does each situation produce the defined response? |
| 4.4 Restoration for another take | Invoke restoration during effects, compare camera and lighting settings with reference values, and check view selection and restarting effects. | Settings before and after restoration, operator actions, and corresponding screen records | Do effects stop, settings return to their reference values, and controls become available for another take? |

Compare each result with the relevant requirement's criteria and mark it met or unmet. Items lacking conditions, criteria, or evidence are marked not evaluated as described in Section 5.1. Define methods for measuring input and control delay and restoration time alongside their criteria.

#### Visual verification

Verify visuals after specifying the subjects and movements to show for each view and effect, supported movement conditions, and visual appearance criteria in Section 4.2. Link each test to its stage version, execution environment, views, effects, movement conditions, and reference images.

| Related requirement | Test conditions and method | Evidence to retain | Result to assess |
|---|---|---|---|
| 4.2 Subject and movement visibility | Apply the movement defined for each view and effect. Check subjects and movements in still images and during movement and camera transitions. | Movement, view, and effect conditions; reference images; corresponding image and video records | Can the intended subjects and movements be identified according to the visibility criteria? |
| 4.2 Lighting, materials, and background | Run the defined views and effects and compare their appearance with reference images. Also inspect output during movement, camera transitions, and effects. | Appearance criteria, reference images, and corresponding image and video records | Are the visual appearance criteria and allowed differences met? |

Meeting criteria in a still image does not establish visual quality throughout movement, transitions, and effects. Record results by tested interval and condition. Items that cannot be assessed because criteria or records are insufficient are marked not evaluated under Section 5.1.

#### Runtime and recording verification

After specifying the output, sustained execution, and recording quality criteria in Section 4.5, run the stage build and OBS together on the reference PC. Link each record to the tested build version, PC configuration, stage and OBS settings, input and control conditions, and test duration.

| Related requirement | Test conditions and method | Evidence to retain | Result to assess |
|---|---|---|---|
| 4.5 Stage output and sustained execution | Run movement, effects, and controls for the defined duration; observe and measure output resolution, runtime performance, resource usage, visual quality, and functional behavior. | Output checks, measurements over time, and visual and functional observations | Do output and sustained visual quality, functionality, and performance meet requirements? |
| 4.5 Recording processing state | Check OBS recording processing state and information about missing or delayed frames during the same execution interval. | OBS processing records and the intervals in which issues occur | Does recording processing meet the defined metrics and thresholds? |
| 4.5 Recorded output, quality, and content | Check the file's resolution and frame rate information. Compare playback with input, effects, and view transition records to inspect content order, omissions, and stutter. | Recorded file, file information, compared intervals, and observations | Does the recording meet output and quality criteria and preserve the order of effects, movement, and view changes? |

Check the stage's execution records, OBS processing state, and recorded file separately and record their respective results. Settings or the file's frame rate information alone do not establish actual frame delivery or recording quality. Define metrics, collection methods, observation intervals, and thresholds before testing. Items with insufficient evidence are marked not evaluated under Section 5.1.

### 5.3 Optimization comparisons and adoption decisions

Compare optimization effects using results from before and after a change, matching the execution environment, input, effects, operator actions, and recording conditions except for the changed element. Apply the same metrics and collection and calculation methods. Check variation between repeated runs, missing measurements, and conditions that could not be matched. Assess whether these differences or gaps affect judgments about the change, and distinguish usable comparison results from aspects that cannot be judged, with reasons. Define the repeat count and criteria for assessing the validity of comparison evidence before testing.

Apply the functional and visual checks in Section 5.2 to the same stage versions used for the performance comparison, using the same requirements and criteria to check that visual quality and functionality are preserved. Record performance changes and requirement compliance separately, then consider them together when documenting reasons to adopt or defer the change. State any visual or functional requirements that are not met even if performance improves. If comparison evidence is insufficient or compliance cannot be confirmed, defer adoption and record the items requiring further verification.

### 5.4 Repeating verification using the provided materials

Verify the materials requirement in Section 4.7 by checking whether the recorded conditions and provided materials allow a test to be prepared and performed again, and its results compared with the existing records. Check whether the required project version, settings, input materials, and procedure can be found and applied, and whether the previous test corresponding to the new result can be identified. If information or materials are insufficient, or conditions cannot be matched, state the items that could not be checked and the limitations. Record separately whether the test could be repeated and whether its results agree with the existing records. If results differ, document the differences and further checks needed.

### 5.5 Assessing achievement of project goals

Project goals are achieved when the Section 4 requirements within the agreed scope are met under the specified conditions, optimization effects and their limits can be explained using valid comparison evidence, and the Section 6 deliverables have been provided under the defined delivery conditions.

To assess this, connect the results from Sections 5.2–5.4 to individual requirements and check their met, unmet, or not evaluated status. Review the performance changes, preservation of visuals and functionality, and reasons to adopt or defer optimization changes together. Check delivery against the contents and composition specified in Section 6, including usage and distribution terms and instructions for materials that must be obtained separately.

Do not mark project goals as achieved while requirements within the agreed scope remain unmet or not evaluated, or delivery conditions remain unmet or unchecked. Distinguish completed items from remaining items and record how the latter affect use, delivery, repeat verification, or interpretation of results.

## 6. Deliverables and delivery conditions

Provide a runnable Windows build and a Unity project that allows the stage's configuration and code to be inspected, modified, and built. Identify the corresponding Unity project version for each executable build so that the running stage can be connected to the project being reviewed or modified.

Setup and operating instructions include the required environment, external tools and input materials, preparation steps, how to start effects, select views, and record another take, and known limitations. These instructions allow users to run and record the stage. A demonstration video shows the implemented effects and the visual changes caused by operator actions, allowing readers to inspect behavior and appearance before running the stage. Identify the stage version and recording conditions for the video so that its configuration is clear.

Verification materials include test conditions, methods, results, raw measurements and observations, optimization comparisons, reasons for adoption or deferral, and unmet or not evaluated items, so that the basis for judgments can be reviewed. Provide the project version, settings, input materials, and procedure needed to prepare the tests again under the recorded conditions, or explain how to obtain them. If identical conditions cannot be reproduced, identify the limitation and its impact on comparison and interpretation.

Provide code, assets, and input materials according to their respective usage and distribution terms. For items users must obtain separately, document the source, required version, terms, and acquisition method. Instructions needed to run, modify, review, and repeat verification must be available in the provided materials, so users can understand the necessary information and restrictions without access to private working documents.

## 7. Open decisions and risks

### 7.1 Open development and verification conditions

#### Movement input and supported range

**Open decisions**

The movement input method, supported range of movement, and whether to integrate real-time motion input remain open.

**Impact on development and verification**

The selected input changes the implementation scope for applying movement to the avatar and the movement conditions to test. Whether the same movement conditions can be reproduced in comparison tests must also be checked.

**Evidence needed for the decision**

- Checks of whether candidate input materials can be obtained.
- Results of checking behavior when the input is applied to the stage.
- Checks of whether the same movement conditions can be applied repeatedly.

**Related sections:** Section 2 scope; Sections 4.1 and 4.3 input and behavior requirements; Section 5.2 functional verification; Section 5.3 optimization comparisons.

#### Camera and effect design and visual appearance criteria

**Open decisions**

The specific camera views, transitions, and effects, the subjects and movements that must be visible, and visual appearance criteria for lighting, materials, and background remain open.

**Impact on development and verification**

These criteria determine the views, transitions, and effects to build and the conditions for judging visual quality. Check whether the intended subjects and movements are visible under supported movement conditions. The defined criteria are also used to assess whether visual quality is preserved after optimization changes.

**Evidence needed for the decision**

- Reference images showing intended subjects, movements, lighting, materials, and background appearance.
- Results showing how subjects and movements appear under supported movement conditions with the proposed views, transitions, and effects.
- A comparison of reference images and applied results to assess allowed differences and areas needing revision.

**Related sections:** Section 2 scope; Section 4.1 controls and effects; Section 4.2 visual requirements; Section 5.2 visual verification; Section 5.3 optimization comparisons.

#### Additional commands, interrupted input, and restoration for another take

**Open decisions**

The allowed delay before movement input, view selection, or starting effects appears on screen remains open. Responses to selecting another view or starting additional effects during transitions or effects, and conditions for detecting and handling interrupted movement input, have not been defined. The camera and lighting settings to restore, their reference values, the allowed restoration time, and responses to additional commands during restoration also remain open.

**Impact on development and verification**

The delay and handling of each situation affect whether movement, effects, and view changes can be recorded at the intended time, whether ongoing work can continue, and whether another recording can be prepared. Establish expected results and functional test conditions by examining the response times needed for operation and recording, the work the operator intends to continue, and the settings to restore.

**Evidence needed for the decision**

- An assessment of when movement input, view selection, and starting effects are needed during operation and recording, and how delay affects the intended recording.
- Use cases describing the work the operator intends to continue and the required on-screen results after additional commands or interruption of movement input.
- Checks of the information and conditions available to detect interrupted input with the selected input method.
- An assessment of camera and lighting settings to restore for a new recording, the allowed preparation time, and controls needed during restoration.

**Related sections:** Section 3.1 operation and recording; Section 4.1 display of movement, views, and effects; Section 4.3 input handling; Section 4.4 restoration; Section 5.2 functional verification.

#### Output, sustained execution, and performance measurement criteria

**Open decisions**

Target resolution and frame rate for the stage and recorded outputs, sustained execution duration, and metrics and thresholds for performance, resources, and recording quality remain open. The reference PC configuration, stage and OBS settings, metric collection and calculation methods, measurement intervals, and repeat count also need to be specified.

**Impact on development and verification**

These conditions determine the runtime and recording quality the stage must sustain and the criteria for assessing optimization results. Define requirements and test conditions by considering both the quality needed for recording and observations and measurements from the reference environment. Thresholds based only on current performance could accept results that fail to meet the required recording quality, so distinguish the quality needed from the level currently achieved.

**Evidence needed for the decision**

- An assessment of the resolution, frame rate, duration, and allowed missing frames and stutter for the intended recording.
- Observations of performance, resource usage, preservation of visuals and functionality, and recording quality with the stage and OBS running and recording together on the reference PC.
- Checks of whether candidate metrics and collection methods capture the relevant behavior, and how missing measurements or variation between runs affect judgments.

**Related sections:** Section 2 reference environment and constraints; Section 4.5 runtime and recording quality; Section 5.2 runtime and recording verification; Section 5.3 optimization comparisons.

### 7.2 Comparison and delivery risks and required checks

#### Unmatched comparison conditions and loss of visual quality or functionality

**Risk and impact on optimization decisions**

If movement or execution conditions cannot be matched before and after a change, it is difficult to determine whether an observed performance difference comes from the optimization change. A change that improves performance can also cause visual quality or functional requirements to be unmet. Adoption decisions require checking both the reproducibility of comparison conditions and compliance with requirements after the change.

**Evidence needed for verification**

- Records comparing input, execution environment, effects, operator actions, and recording conditions before and after the change, including unmatched conditions.
- An assessment of how variation between repeated runs and missing measurements affect judgments about the change.
- Visual and functional verification results from the stage versions used in the performance comparison, including unmet and not evaluated items.

**Related sections:** Section 4.6 comparison materials; Section 4.7 assessment and repeat verification materials; Section 5.2 functional and visual verification; Section 5.3 comparisons and adoption decisions.

#### Deliverable distribution and access to external materials

**Open decisions and risks**

Publication locations, delivery methods, and conditions for obtaining external assets and input materials remain open. Usage and distribution terms or access restrictions may prevent materials from being included in the deliverables or obtained separately by users.

**Impact on use, delivery, and repeat verification**

If required materials cannot be provided or obtained, running or modifying the stage or repeating verification under recorded conditions may be difficult. Check the conditions for each material to decide what to provide directly and what to document for separate acquisition. For materials that are difficult to obtain, assess possible substitutes or limits on what can be provided. If substitutes are used, check whether comparison with existing results under the same conditions is possible.

**Evidence needed for decisions and verification**

- Sources, versions, and usage and distribution terms for the code, assets, and input materials actually used.
- Checks of access conditions and acquisition methods for materials that must be obtained separately.
- An assessment of how substitutes affect running, modifying, and repeating verification, and which conditions differ from the originals.
- Checks that the required deliverables and instructions can be accessed and downloaded from the delivery location.

**Related sections:** Section 2 external materials and constraints; Section 4.7 assessment and repeat verification materials; Section 5.4 repeat verification; Section 6 deliverables and delivery conditions.

### 7.3 Incorporating decisions

Connect each open decision and risk to the affected scope, requirements, verification, and deliverables, and to the evidence needed for a decision. After reviewing that evidence and making a decision, update the relevant section and this section's open status and remaining checks. State how any remaining limitations affect running, delivering, and repeating verification of the stage and interpreting results, so readers can distinguish applicable conditions from aspects that have not been checked.
