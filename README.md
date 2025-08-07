# BuildVersionIncrementor

Unity Editor script to prompt build version bump before building.

## What it does
- Automatically shows a dialog before build;
- Allows incrementing `PlayerSettings.bundleVersion`;
- Supports suffixes like `-beta`, `.dev`, etc.

## Example
- `1.2.3` → `1.2.4`
- `1.0.7-beta` → `1.0.8-beta`

## Installation
-  import [`BuildVersionIncrementor.unitypackage`](./BuildVersionIncrementor.unitypackage) into Unity via:
