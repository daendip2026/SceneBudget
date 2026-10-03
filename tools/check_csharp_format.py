"""Check selected C# files; configuration changes check all tracked scripts."""

import subprocess
import sys
from pathlib import Path


def main():
    scripts = Path("Assets/Scripts")
    paths = sys.argv[1:]
    if any(not path.startswith("Assets/Scripts/") or not path.endswith(".cs") for path in paths):
        result = subprocess.run(
            ["git", "ls-files", "-z", "--", "Assets/Scripts"],
            check=True,
            stdout=subprocess.PIPE,
        )
        paths = result.stdout.decode("utf-8").rstrip("\0").split("\0")

    # dotnet format resolves --include relative to its folder, not the Git root.
    included = [
        str(Path(path).relative_to(scripts))
        for path in paths
        if path.endswith(".cs") and Path(path).is_file()
    ]
    if not included:
        return 0

    return subprocess.run(
        ["dotnet", "format", "whitespace", str(scripts), "--folder",
         "--verify-no-changes", "--verbosity", "quiet", "--include", *included],
        check=False,
    ).returncode


if __name__ == "__main__":
    sys.exit(main())
