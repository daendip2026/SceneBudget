# Repository Instructions

## Audience and scope

- This file is the shared baseline for every contributor and coding agent working in this repository. Do not put contributor-specific preferences or local-only workflows here.
- Keep instructions factual and current. Do not document commands, directories, or workflows that do not yet exist in the repository.

## Repository boundaries

- Repository documentation, build steps, and verification evidence must depend only on tracked files and explicitly documented external inputs.
- Do not commit machine-specific state. When environment characteristics matter, document a sanitized and reproducible description instead.
- Preserve unrelated work already present in the worktree. Keep changes focused on the requested scope.

## Engineering quality

- Treat the project as maintained production work, not as a disposable portfolio demo. Apply a quality bar appropriate to a well-maintained open-source or internal project.
- Prefer simple, explicit designs with clear ownership and maintenance paths. Avoid speculative infrastructure and abstractions without a current consumer.
- Before adding a file, template, dependency, or process, identify its current purpose, consumer, and verification path.
- Prevent placeholders, duplicate documents, and disposable artifacts instead of creating cleanup or deletion-history documents later.
- Existing documents and templates are inputs to evaluate, not requirements to preserve unchanged. Replace them only when the result has a clear current use and improves correctness, clarity, or maintainability.

## Evidence and verification

- Base architecture, compatibility, performance, and workflow claims on evidence appropriate to their risk. Prefer current primary sources such as official documentation, specifications, and original research.
- Distinguish sourced facts, inferences, and recommendations. Record material assumptions and comparison conditions.
- Do not generalize from a single measurement or example beyond the conditions it actually supports.
- Make performance claims reproducible by recording the relevant environment, configuration, workload, metric, and comparison method.
- Run validation proportionate to the change. Report what was changed, what was verified, and what remains unverified.

## Collaboration and review

- Surface conflicting or ambiguous repository requirements before changing the affected area. Continue safe work that is independent of the conflict.
- Explain material tradeoffs when proposing a direction or disagreeing with a request.
- Keep pull requests and commits reviewable: use focused changes, describe the reason and verification evidence, and avoid mixing unrelated cleanup.
