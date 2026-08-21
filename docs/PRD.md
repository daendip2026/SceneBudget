# SceneBudget — Product Requirements

**Status:** Draft

---

## 1. Problem

Live streaming platform encoding guidance specifies 1080p at 60 fps as a standard delivery preset, which fixes the frame budget at 16.67 ms independently of any scene. A broadcast scene must hold that budget continuously and under live conditions.

Whether a scene holds that budget, and what each of its elements costs, is established by measurement. Any change to the scene invalidates the previous answer.

SceneBudget builds a VRM broadcast scene and the measurement procedure that answers both questions repeatably.

## 2. Goal

A VRM broadcast scene whose frame cost is known per element and verifiable on demand.

Deliverables:

1. the Unity scene,
2. a measurement procedure reproducible by a third party (§5),
3. a per-element cost record — what each element group costs when removed from the full scene,
4. a decision record: optimizations applied with before/after figures, optimizations reverted with figures and the reason, and optimizations considered and not applied with the reason.

## 3. Scope

**Subject.** One broadcast set: a VRM character on a stage, with set dressing, a lighting rig, and a post-processing stack.

**Element groups.** The units of measurement are five:

1. **VRM character**
2. **Stage** — floor, walls, and backdrop geometry
3. **Set dressing** — props and decorative objects placed on the stage
4. **Lighting rig** — light sources, shadow settings, light probes, and reflection probes
5. **Post-processing stack** — screen-space effects applied after the scene renders

**Fixed conditions.** The camera path is fixed across all passes and is not a measured variable.

**Material and shader work** is in scope as an optimization applied to the element groups above, not as a separate measured group.

## 4. Non-goals

Excluded explicitly:

- Capture-device integration
- Real-time face or motion capture input
- Live operation tooling
- Tuning the encoder or streaming pipeline
- Multi-character or crowd scenes

## 5. Measurement

A **pass** is one scene configuration. A **run** is one measurement of a pass under one condition of §5.2. Six passes in two conditions produce twelve runs.

**Method: ablation.** The full set is built first, then measured with one element group removed at a time. Each figure is the marginal cost of removing that group from the full set and is valid only as such.

One consequence is accepted: removal costs do not sum to the total, because elements interact.

### 5.1 Metrics

Recorded on every run:

- **End-to-end frame time** — the interval between presented frames
- **CPU frame time** and **GPU frame time**, separately
- **Distribution of end-to-end frame time**, reported as 99th percentile and 0.1% low
- **SetPass calls**, with batch and draw call counts alongside
- **Triangle and vertex counts**
- **Graphics memory in use**

Each answers a different question — which processor is the bottleneck, whether the budget survives the worst frames, what drives CPU-side rendering cost, whether a cost is geometry or shading, and whether memory is a binding constraint. Dropping any one leaves a cut decision unsupported.

### 5.2 Conditions

Rendering and encoding run on the same machine.

Measurement runs on a standalone development build. Editor play mode is not a valid measurement condition, because it includes the editor's own cost in the result.

Render scale is fixed at 1.0 for every run. Lowering it is an optimization recorded under §2.4, not a measurement condition.

Every pass is measured in two conditions: renderer alone, and with a capture/encode process running concurrently. The encode configuration matches the delivery preset of §1 and is fixed across all runs; the configuration used, including whether encoding is performed in hardware or software, is recorded with the result.

The renderer-alone figure is what optimization work acts on. The concurrent figure is what §6 is judged against.

The effect of concurrent encoding on render frame time is a result the measurement produces, not an input to it. It is not assumed in either direction.

### 5.3 Procedure

The procedure must specify scene state, warm-up, sample duration, repeat count, and the rule for discarding a run.

Sample duration must extend past the point where the machine reaches a steady thermal state. §1 requires the budget to hold continuously; a run that ends before thermal steady state does not test that requirement.

## 6. Acceptance criteria

**Target: 1080p, 60 fps — a frame budget of 16.67 ms.** Taken from the delivery preset in §1.

**Criterion.** The full-set pass meets 99th percentile end-to-end frame time ≤ 16.67 ms, under the concurrent-encode condition of §5.2, in every valid run of that pass.

The ablation passes are diagnostic. They produce the per-element costs of §2.3 and are not judged against this criterion.

1% low / 99th percentile is the established metric for frame pacing. A mean is not used, because a mean can sit inside budget while individual frames miss it. A 100% threshold is not used, because it makes a single outlier frame a failure. The 0.1% low is recorded as a diagnostic, not as a pass condition.

Acceptance applies to a single reference environment, specified by model and configuration in the measurement record rather than here.

## 7. Stop rule

Optimization work closes for a given scene configuration when **either** condition holds:

1. the acceptance criterion in §6 is met, or
2. the criterion is not met, and the decision record of §2.4 identifies the binding constraint from the metrics of §5.1.

Closing without either is not permitted.

A change to the scene produces a new configuration. The criterion is re-evaluated against it; this is normal operation, not a reopening of closed work.
