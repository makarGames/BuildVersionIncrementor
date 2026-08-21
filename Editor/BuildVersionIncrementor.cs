using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BuildVersionIncrementor.Editor
{
    /// <summary>
    /// Shows a pre-build dialog to bump <see cref="PlayerSettings.bundleVersion"/>
    /// (e.g. 1.2.3 → 1.2.4, preserving suffixes like -beta).
    /// </summary>
    public sealed class BuildVersionIncrementor : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            var currentVersion = PlayerSettings.bundleVersion;
            var suggestedVersion = currentVersion;
            var canIncrement = false;
            var warningMessage = string.Empty;

            // Example: 1.2.3, 1.2.3b, 1.2.3-beta, 1.2.3.dev, etc.
            var versionParts = currentVersion.Split('.');
            if (versionParts.Length >= 3)
            {
                var match = Regex.Match(versionParts[2], @"^(\d+)([\-\._]?[a-zA-Z0-9]+)?$");

                if (match.Success)
                {
                    var buildNumber = int.Parse(match.Groups[1].Value);
                    var suffix = match.Groups[2].Value;

                    buildNumber++;
                    versionParts[2] = buildNumber + suffix;
                    suggestedVersion = string.Join(".", versionParts);
                    canIncrement = true;
                }
                else
                {
                    warningMessage = "Couldn't detect numeric build part in version string.";
                }
            }
            else
            {
                warningMessage = "Version format is not in expected 'major.minor.build' structure.";
            }

            var dialogMessage =
                $"Current version: {currentVersion}\n" +
                $"Suggested new version: {(canIncrement ? suggestedVersion : "—")}\n\n" +
                (canIncrement
                    ? "Choose an action:\nYes – Increment version;\nNo – Build without changes;\nCancel – Abort the build."
                    : warningMessage + "\n\nBuild will proceed unless canceled.\n\nContinue anyway?");

            var option = EditorUtility.DisplayDialogComplex(
                "Build Version Control",
                dialogMessage,
                canIncrement ? "Yes" : "Continue",
                "No",
                "Cancel");

            switch (option)
            {
                case 2:
                    throw new BuildFailedException("Build aborted by user.");
                case 0:
                    if (canIncrement)
                    {
                        PlayerSettings.bundleVersion = suggestedVersion;
                        Debug.Log($"[Build Version Incrementor] Version increased from {currentVersion} to {suggestedVersion}");
                    }
                    else
                    {
                        Debug.LogWarning($"[Build Version Incrementor] Version not incremented. Reason: {warningMessage}");
                    }
                    break;
                case 1:
                    Debug.Log("[Build Version Incrementor] Build continued without version change.");
                    break;
            }
        }
    }
}
