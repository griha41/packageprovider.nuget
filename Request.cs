// 
//  Copyright (c) Microsoft Corporation. All rights reserved. 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  

namespace OneGet.PackageProvider.NuGet {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Callback = System.Object;

    public abstract class Request : IDisposable {

        #region copy core-apis

        public abstract string GetMessageString(string message);

        public abstract bool Warning(string message);

        public abstract bool Error(string message);

        public abstract bool Message(string message);

        public abstract bool Verbose(string message);

        public abstract bool Debug(string message);

        public abstract bool ExceptionThrown(string exceptionType, string message, string stacktrace);

        public abstract int StartProgress(int parentActivityId, string message);

        public abstract bool Progress(int activityId, int progress, string message);

        public abstract bool CompleteProgress(int activityId, bool isSuccessful);

        /// <summary>
        ///     The provider can query to see if the operation has been cancelled.
        ///     This provides for a gentle way for the caller to notify the callee that
        ///     they don't want any more results.
        /// </summary>
        /// <returns>returns TRUE if the operation has been cancelled.</returns>
        public abstract bool IsCancelled();
        #endregion

        #region copy host-apis

        public abstract object GetPackageManagementService();

        /// <summary>
        ///     Used by a provider to request what metadata keys were passed from the user
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<string> GetOptionKeys(string category);

        public abstract IEnumerable<string> GetOptionValues(string category, string key);

        public abstract IEnumerable<string> GetSpecifiedPackageSources();

        public abstract bool ShouldContinueWithUntrustedPackageSource(string package, string packageSource);

        public abstract bool ShouldProcessPackageInstall(string packageName, string version, string source);

        public abstract bool ShouldProcessPackageUninstall(string packageName, string version);

        public abstract bool ShouldContinueAfterPackageInstallFailure(string packageName, string version, string source);

        public abstract bool ShouldContinueAfterPackageUninstallFailure(string packageName, string version, string source);

        public abstract bool ShouldContinueRunningInstallScript(string packageName, string version, string source, string scriptLocation);

        public abstract bool ShouldContinueRunningUninstallScript(string packageName, string version, string source, string scriptLocation);

        public abstract bool AskPermission(string permission);

        public abstract bool WhatIf();
        #endregion

        #region copy service-apis

        public abstract string GetNuGetExePath(Callback c);

        public abstract string GetNuGetDllPath(Callback c);

        public abstract string DownloadFile(string remoteLocation, string localLocation, Callback c);

        public abstract void AddPinnedItemToTaskbar(string item, Callback c);

        public abstract void RemovePinnedItemFromTaskbar(string item, Callback c);

        public abstract bool CreateShortcutLink(string linkPath, string targetPath, string description, string workingDirectory, string arguments, Callback c);

        public abstract IEnumerable<string> UnzipFileIncremental(string zipFile, string folder, Callback c);

        public abstract IEnumerable<string> UnzipFile(string zipFile, string folder, Callback c);

        public abstract void AddFileAssociation();

        public abstract void RemoveFileAssociation();

        public abstract void AddExplorerMenuItem();

        public abstract void RemoveExplorerMenuItem();

        public abstract bool SetEnvironmentVariable(string variable, string value, string context, Callback c);

        public abstract bool RemoveEnvironmentVariable(string variable, string context, Callback c);

        public abstract void AddFolderToPath();

        public abstract void RemoveFolderFromPath();

        public abstract void InstallMSI();

        public abstract void RemoveMSI();

        public abstract void StartProcess();

        public abstract void InstallVSIX();

        public abstract void UninstallVSIX();

        public abstract void InstallPowershellScript();

        public abstract void UninstallPowershellScript();

        public abstract void SearchForExecutable();

        public abstract void GetUserBinFolder();

        public abstract void GetSystemBinFolder();

        public abstract bool CopyFile(string sourcePath, string destinationPath, Callback c);

        public abstract void CopyFolder();

        public abstract void Delete(string path, Callback c);

        public abstract void DeleteFolder(string folder, Callback c);

        public abstract void CreateFolder(string folder, Callback c);

        public abstract void DeleteFile(string filename, Callback c);

        public abstract void BeginTransaction();

        public abstract void AbortTransaction();

        public abstract void EndTransaction();

        public abstract void GenerateUninstallScript();

        public abstract string GetKnownFolder(string knownFolder, Callback c);

        public abstract bool IsElevated(Callback c);

        public abstract object GetPackageManagementService(Callback c);
        #endregion

        #region copy request-apis

        /// <summary>
        ///     The provider can query to see if the operation has been cancelled.
        ///     This provides for a gentle way for the caller to notify the callee that
        ///     they don't want any more results. It's essentially just !IsCancelled
        /// </summary>
        /// <returns>returns FALSE if the operation has been cancelled.</returns>
        public abstract bool OkToContinue();

        /// <summary>
        ///     Used by a provider to return fields for a SoftwareIdentity.
        /// </summary>
        /// <param name="fastPath"></param>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <param name="versionScheme"></param>
        /// <param name="summary"></param>
        /// <param name="source"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public abstract bool YieldPackage(string fastPath, string name, string version, string versionScheme, string summary, string source, string searchKey);

        public abstract bool YieldPackageDetails(object serializablePackageDetailsObject);

        public abstract bool YieldPackageSwidtag(string fastPath, string xmlOrJsonDoc);

        /// <summary>
        ///     Used by a provider to return fields for a package source (repository)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public abstract bool YieldPackageSource(string name, string location, bool isTrusted);

        /// <summary>
        ///     Used by a provider to return the fields for a Metadata Definition
        ///     The cmdlets can use this to supply tab-completion for metadata to the user.
        /// </summary>
        /// <param name="category"> one of ['provider', 'source', 'package', 'install']</param>
        /// <param name="name">the provider-defined name of the option</param>
        /// <param name="expectedType"> one of ['string','int','path','switch']</param>
        /// <param name="isRequired">if the parameter is mandatory</param>
        /// <returns></returns>
        public abstract bool YieldDynamicOption(int category, string name, int expectedType, bool isRequired);

        public abstract bool YieldKeyValuePair(string key, string value);

        public abstract bool YieldValue(string value);
        #endregion

        #region copy Request-implementation
public bool Warning(string message, params object[] args) {
            return Warning(string.Format(GetMessageString(message) ?? message, args));
        }

        public bool Error(string message, params object[] args) {
            return Error(string.Format(GetMessageString(message) ?? message, args));
        }

        public bool Message(string message, params object[] args) {
            return Message(string.Format(GetMessageString(message) ?? message, args));
        }

        public bool Verbose(string message, params object[] args) {
            return Verbose(string.Format(GetMessageString(message) ?? message, args));
        }

        public bool Debug(string message, params object[] args) {
            return Debug(string.Format(GetMessageString(message) ?? message, args));
        }

        public int StartProgress(int parentActivityId, string message, params object[] args) {
            return StartProgress(parentActivityId, string.Format(GetMessageString(message) ?? message, args));
        }

        public bool Progress(int activityId, int progress, string message, params object[] args) {
            return Progress(activityId, progress, string.Format(GetMessageString(message) ?? message, args));
        }

        public void Dispose() {
        }

        #endregion

    }

    #region copy dynamicextension-implementation
public static class DynamicExtensions {

        private static dynamic _dynamicInterface;

        public static dynamic DynamicInterface {
            get {
                return _dynamicInterface;
            }
            set {
                // Write Once Property
                if (_dynamicInterface == null) {
                    _dynamicInterface = value;
                    // _dynamicInterface = AppDomain.CurrentDomain.GetData("DynamicInterface");
                }
            }
        }

        public static T As<T>(this object instance) {
            return DynamicInterface.Create<T>(instance);
        }
        public static T Extend<T>(this object obj, params object[] objects) {
            return DynamicInterface.Create<T>(objects, obj);
        }
    }

    #endregion

}