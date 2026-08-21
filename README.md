# Build Version Incrementor

[![Unity 2022.3+](https://img.shields.io/badge/Unity-2022.3%2B-black.svg?style=flat&logo=unity)](https://unity.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

Editor utility that asks whether to bump your **Player Settings** build version **before every build** — so you do not ship another APK/IPA with the same `bundleVersion` by accident.

---

## Features

- **Pre-build dialog** via `IPreprocessBuildWithReport`
- **Smart bump** of the third version segment (`1.2.3` → `1.2.4`)
- **Keeps suffixes** such as `-beta`, `.dev`, `_rc`
- **Three choices**: increment, build unchanged, or cancel the build
- **Editor-only** — no runtime code in player builds

### Examples

| Current | Suggested |
|---------|-----------|
| `1.2.3` | `1.2.4` |
| `1.0.7-beta` | `1.0.8-beta` |
| `2.1.0.dev` | `2.1.1.dev` |

---

## Requirements

| Requirement | Version |
|-------------|---------|
| Unity | **2022.3** or newer |

---

## Installation

### Option A — Git URL (recommended)

1. Open your Unity project.
2. Go to **Window → Package Manager**.
3. Click the **+** button in the top-left corner.
4. Choose **Add package from git URL...**
5. Paste:

```
https://github.com/makarGames/BuildVersionIncrementor.git
```

6. Click **Add**.

Git URL: [https://github.com/makarGames/BuildVersionIncrementor.git](https://github.com/makarGames/BuildVersionIncrementor.git)

### Option B — Specific version / tag

```
https://github.com/makarGames/BuildVersionIncrementor.git#v1.0.0
```

---

## Usage

1. Set **Edit → Project Settings → Player → Version** to a `major.minor.build` style string (optional suffix allowed).
2. Start a build as usual (**File → Build Settings → Build**, or your build pipeline).
3. When the dialog appears:
   - **Yes** — write the suggested version into `PlayerSettings.bundleVersion`, then continue
   - **No** — continue with the current version
   - **Cancel** — abort the build

If the version string cannot be parsed, the dialog warns you and still lets you continue or cancel.

---

## Package layout

```
com.makargames.build-version-incrementor/
├── package.json
├── README.md
├── CHANGELOG.md
├── LICENSE
├── Editor/
│   ├── BuildVersionIncrementor.Editor.asmdef
│   └── BuildVersionIncrementor.cs
└── documentation/
```

---

## License

This project is licensed under the [MIT License](LICENSE).
