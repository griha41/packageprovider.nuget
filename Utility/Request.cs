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

namespace OneGet.ProtocolProvider.NuGet.Utility {
    using System;
    using System.Collections.Generic;
    using Callback = System.Func<string, System.Collections.Generic.IEnumerable<object>, object>;

    internal class Request : IDisposable {
        private Func<string, IEnumerable<object>, object> _callback;

        internal Request(Func<string, IEnumerable<object>, object> c) {
            _callback = c;
        }

        internal bool IsDisposed {
            get {
                return _callback == null;
            }
        }

        #region generate-dispatcher service-apis

        private GetNuGetExePath _GetNuGetExePath;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public string GetNuGetExePath( ) {
            CheckDisposed();
            return  (_GetNuGetExePath ?? (_GetNuGetExePath = (_callback.Resolve<GetNuGetExePath>() ?? (()=> default(string) ) )))();
        }

        private GetNuGetDllPath _GetNuGetDllPath;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public string GetNuGetDllPath( ) {
            CheckDisposed();
            return  (_GetNuGetDllPath ?? (_GetNuGetDllPath = (_callback.Resolve<GetNuGetDllPath>() ?? (()=> default(string) ) )))();
        }

        private DownloadFile _DownloadFile;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public string DownloadFile(string remoteLocation ,string localLocation  ) {
            CheckDisposed();
            return  (_DownloadFile ?? (_DownloadFile = (_callback.Resolve<DownloadFile>() ?? ((premoteLocation,plocalLocation)=> default(string) ) )))(remoteLocation,localLocation);
        }

        private AddPinnedItemToTaskbar _AddPinnedItemToTaskbar;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void AddPinnedItemToTaskbar(string item  ) {
            CheckDisposed();
             (_AddPinnedItemToTaskbar ?? (_AddPinnedItemToTaskbar = (_callback.Resolve<AddPinnedItemToTaskbar>() ?? ((pitem)=> { } ) )))(item);
        }

        private RemovePinnedItemFromTaskbar _RemovePinnedItemFromTaskbar;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void RemovePinnedItemFromTaskbar(string item  ) {
            CheckDisposed();
             (_RemovePinnedItemFromTaskbar ?? (_RemovePinnedItemFromTaskbar = (_callback.Resolve<RemovePinnedItemFromTaskbar>() ?? ((pitem)=> { } ) )))(item);
        }

        private CreateShortcutLink _CreateShortcutLink;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool CreateShortcutLink(string linkPath ,string targetPath ,string description ,string workingDirectory ,string arguments  ) {
            CheckDisposed();
            return  (_CreateShortcutLink ?? (_CreateShortcutLink = (_callback.Resolve<CreateShortcutLink>() ?? ((plinkPath,ptargetPath,pdescription,pworkingDirectory,parguments)=> default(bool) ) )))(linkPath,targetPath,description,workingDirectory,arguments);
        }

        private UnzipFileIncremental _UnzipFileIncremental;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public IEnumerable<string> UnzipFileIncremental(string zipFile ,string folder  ) {
            CheckDisposed();
            return  (_UnzipFileIncremental ?? (_UnzipFileIncremental = (_callback.Resolve<UnzipFileIncremental>() ?? ((pzipFile,pfolder)=> default(IEnumerable<string>) ) )))(zipFile,folder);
        }

        private UnzipFile _UnzipFile;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public IEnumerable<string> UnzipFile(string zipFile ,string folder  ) {
            CheckDisposed();
            return  (_UnzipFile ?? (_UnzipFile = (_callback.Resolve<UnzipFile>() ?? ((pzipFile,pfolder)=> default(IEnumerable<string>) ) )))(zipFile,folder);
        }

        private AddFileAssociation _AddFileAssociation;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void AddFileAssociation( ) {
            CheckDisposed();
             (_AddFileAssociation ?? (_AddFileAssociation = (_callback.Resolve<AddFileAssociation>() ?? (()=> { } ) )))();
        }

        private RemoveFileAssociation _RemoveFileAssociation;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void RemoveFileAssociation( ) {
            CheckDisposed();
             (_RemoveFileAssociation ?? (_RemoveFileAssociation = (_callback.Resolve<RemoveFileAssociation>() ?? (()=> { } ) )))();
        }

        private AddExplorerMenuItem _AddExplorerMenuItem;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void AddExplorerMenuItem( ) {
            CheckDisposed();
             (_AddExplorerMenuItem ?? (_AddExplorerMenuItem = (_callback.Resolve<AddExplorerMenuItem>() ?? (()=> { } ) )))();
        }

        private RemoveExplorerMenuItem _RemoveExplorerMenuItem;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void RemoveExplorerMenuItem( ) {
            CheckDisposed();
             (_RemoveExplorerMenuItem ?? (_RemoveExplorerMenuItem = (_callback.Resolve<RemoveExplorerMenuItem>() ?? (()=> { } ) )))();
        }

        private SetEnvironmentVariable _SetEnvironmentVariable;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool SetEnvironmentVariable(string variable ,string value ,string context  ) {
            CheckDisposed();
            return  (_SetEnvironmentVariable ?? (_SetEnvironmentVariable = (_callback.Resolve<SetEnvironmentVariable>() ?? ((pvariable,pvalue,pcontext)=> default(bool) ) )))(variable,value,context);
        }

        private RemoveEnvironmentVariable _RemoveEnvironmentVariable;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool RemoveEnvironmentVariable(string variable ,string context  ) {
            CheckDisposed();
            return  (_RemoveEnvironmentVariable ?? (_RemoveEnvironmentVariable = (_callback.Resolve<RemoveEnvironmentVariable>() ?? ((pvariable,pcontext)=> default(bool) ) )))(variable,context);
        }

        private AddFolderToPath _AddFolderToPath;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void AddFolderToPath( ) {
            CheckDisposed();
             (_AddFolderToPath ?? (_AddFolderToPath = (_callback.Resolve<AddFolderToPath>() ?? (()=> { } ) )))();
        }

        private RemoveFolderFromPath _RemoveFolderFromPath;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void RemoveFolderFromPath( ) {
            CheckDisposed();
             (_RemoveFolderFromPath ?? (_RemoveFolderFromPath = (_callback.Resolve<RemoveFolderFromPath>() ?? (()=> { } ) )))();
        }

        private InstallMSI _InstallMSI;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void InstallMSI( ) {
            CheckDisposed();
             (_InstallMSI ?? (_InstallMSI = (_callback.Resolve<InstallMSI>() ?? (()=> { } ) )))();
        }

        private RemoveMSI _RemoveMSI;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void RemoveMSI( ) {
            CheckDisposed();
             (_RemoveMSI ?? (_RemoveMSI = (_callback.Resolve<RemoveMSI>() ?? (()=> { } ) )))();
        }

        private StartProcess _StartProcess;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void StartProcess( ) {
            CheckDisposed();
             (_StartProcess ?? (_StartProcess = (_callback.Resolve<StartProcess>() ?? (()=> { } ) )))();
        }

        private InstallVSIX _InstallVSIX;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void InstallVSIX( ) {
            CheckDisposed();
             (_InstallVSIX ?? (_InstallVSIX = (_callback.Resolve<InstallVSIX>() ?? (()=> { } ) )))();
        }

        private UninstallVSIX _UninstallVSIX;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void UninstallVSIX( ) {
            CheckDisposed();
             (_UninstallVSIX ?? (_UninstallVSIX = (_callback.Resolve<UninstallVSIX>() ?? (()=> { } ) )))();
        }

        private InstallPowershellScript _InstallPowershellScript;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void InstallPowershellScript( ) {
            CheckDisposed();
             (_InstallPowershellScript ?? (_InstallPowershellScript = (_callback.Resolve<InstallPowershellScript>() ?? (()=> { } ) )))();
        }

        private UninstallPowershellScript _UninstallPowershellScript;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void UninstallPowershellScript( ) {
            CheckDisposed();
             (_UninstallPowershellScript ?? (_UninstallPowershellScript = (_callback.Resolve<UninstallPowershellScript>() ?? (()=> { } ) )))();
        }

        private SearchForExecutable _SearchForExecutable;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void SearchForExecutable( ) {
            CheckDisposed();
             (_SearchForExecutable ?? (_SearchForExecutable = (_callback.Resolve<SearchForExecutable>() ?? (()=> { } ) )))();
        }

        private GetUserBinFolder _GetUserBinFolder;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void GetUserBinFolder( ) {
            CheckDisposed();
             (_GetUserBinFolder ?? (_GetUserBinFolder = (_callback.Resolve<GetUserBinFolder>() ?? (()=> { } ) )))();
        }

        private GetSystemBinFolder _GetSystemBinFolder;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void GetSystemBinFolder( ) {
            CheckDisposed();
             (_GetSystemBinFolder ?? (_GetSystemBinFolder = (_callback.Resolve<GetSystemBinFolder>() ?? (()=> { } ) )))();
        }

        private CopyFile _CopyFile;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool CopyFile(string sourcePath ,string destinationPath  ) {
            CheckDisposed();
            return  (_CopyFile ?? (_CopyFile = (_callback.Resolve<CopyFile>() ?? ((psourcePath,pdestinationPath)=> default(bool) ) )))(sourcePath,destinationPath);
        }

        private CopyFolder _CopyFolder;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void CopyFolder( ) {
            CheckDisposed();
             (_CopyFolder ?? (_CopyFolder = (_callback.Resolve<CopyFolder>() ?? (()=> { } ) )))();
        }

        private Delete _Delete;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void Delete(string path  ) {
            CheckDisposed();
             (_Delete ?? (_Delete = (_callback.Resolve<Delete>() ?? ((ppath)=> { } ) )))(path);
        }

        private DeleteFolder _DeleteFolder;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void DeleteFolder(string folder  ) {
            CheckDisposed();
             (_DeleteFolder ?? (_DeleteFolder = (_callback.Resolve<DeleteFolder>() ?? ((pfolder)=> { } ) )))(folder);
        }

        private CreateFolder _CreateFolder;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void CreateFolder(string folder  ) {
            CheckDisposed();
             (_CreateFolder ?? (_CreateFolder = (_callback.Resolve<CreateFolder>() ?? ((pfolder)=> { } ) )))(folder);
        }

        private DeleteFile _DeleteFile;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void DeleteFile(string filename  ) {
            CheckDisposed();
             (_DeleteFile ?? (_DeleteFile = (_callback.Resolve<DeleteFile>() ?? ((pfilename)=> { } ) )))(filename);
        }

        private BeginTransaction _BeginTransaction;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void BeginTransaction( ) {
            CheckDisposed();
             (_BeginTransaction ?? (_BeginTransaction = (_callback.Resolve<BeginTransaction>() ?? (()=> { } ) )))();
        }

        private AbortTransaction _AbortTransaction;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void AbortTransaction( ) {
            CheckDisposed();
             (_AbortTransaction ?? (_AbortTransaction = (_callback.Resolve<AbortTransaction>() ?? (()=> { } ) )))();
        }

        private EndTransaction _EndTransaction;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void EndTransaction( ) {
            CheckDisposed();
             (_EndTransaction ?? (_EndTransaction = (_callback.Resolve<EndTransaction>() ?? (()=> { } ) )))();
        }

        private GenerateUninstallScript _GenerateUninstallScript;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public void GenerateUninstallScript( ) {
            CheckDisposed();
             (_GenerateUninstallScript ?? (_GenerateUninstallScript = (_callback.Resolve<GenerateUninstallScript>() ?? (()=> { } ) )))();
        }

        private GetKnownFolder _GetKnownFolder;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public string GetKnownFolder(string knownFolder  ) {
            CheckDisposed();
            return  (_GetKnownFolder ?? (_GetKnownFolder = (_callback.Resolve<GetKnownFolder>() ?? ((pknownFolder)=> default(string) ) )))(knownFolder);
        }

        private IsElevated _IsElevated;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool IsElevated( ) {
            CheckDisposed();
            return  (_IsElevated ?? (_IsElevated = (_callback.Resolve<IsElevated>() ?? (()=> default(bool) ) )))();
        }

        #endregion

        #region generate-dispatcher core-apis

        private Warning _Warning;
        // Core Callbacks that we'll both use internally and pass on down to providers.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool Warning(string messageCode ,string message ,params object[] args ) {
            CheckDisposed();
            return  (_Warning ?? (_Warning = (_callback.Resolve<Warning>() ?? ((pmessageCode,pmessage,pargs)=> default(bool) ) )))(messageCode,message,args);
        }

        private Message _Message;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool Message(string messageCode ,string message ,params object[] args ) {
            CheckDisposed();
            return  (_Message ?? (_Message = (_callback.Resolve<Message>() ?? ((pmessageCode,pmessage,pargs)=> default(bool) ) )))(messageCode,message,args);
        }

        private Error _Error;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool Error(string messageCode ,string message ,params object[] args ) {
            CheckDisposed();
            return  (_Error ?? (_Error = (_callback.Resolve<Error>() ?? ((pmessageCode,pmessage,pargs)=> default(bool) ) )))(messageCode,message,args);
        }

        private Debug _Debug;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool Debug(string messageCode ,string message ,params object[] args ) {
            CheckDisposed();
            return  (_Debug ?? (_Debug = (_callback.Resolve<Debug>() ?? ((pmessageCode,pmessage,pargs)=> default(bool) ) )))(messageCode,message,args);
        }

        private Verbose _Verbose;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool Verbose(string messageCode ,string message ,params object[] args ) {
            CheckDisposed();
            return  (_Verbose ?? (_Verbose = (_callback.Resolve<Verbose>() ?? ((pmessageCode,pmessage,pargs)=> default(bool) ) )))(messageCode,message,args);
        }

        private ExceptionThrown _ExceptionThrown;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool ExceptionThrown(string exceptionType ,string message ,string stacktrace  ) {
            CheckDisposed();
            return  (_ExceptionThrown ?? (_ExceptionThrown = (_callback.Resolve<ExceptionThrown>() ?? ((pexceptionType,pmessage,pstacktrace)=> default(bool) ) )))(exceptionType,message,stacktrace);
        }

        private Progress _Progress;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool Progress(int activityId ,string activity ,int progress ,string message ,params object[] args ) {
            CheckDisposed();
            return  (_Progress ?? (_Progress = (_callback.Resolve<Progress>() ?? ((pactivityId,pactivity,pprogress,pmessage,pargs)=> default(bool) ) )))(activityId,activity,progress,message,args);
        }

        private ProgressComplete _ProgressComplete;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool ProgressComplete(int activityId ,string activity ,string message ,params object[] args ) {
            CheckDisposed();
            return  (_ProgressComplete ?? (_ProgressComplete = (_callback.Resolve<ProgressComplete>() ?? ((pactivityId,pactivity,pmessage,pargs)=> default(bool) ) )))(activityId,activity,message,args);
        }

        private GetHostDelegate _GetHostDelegate;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public Callback GetHostDelegate( ) {
            CheckDisposed();
            return  (_GetHostDelegate ?? (_GetHostDelegate = (_callback.Resolve<GetHostDelegate>() ?? (()=> default(Callback) ) )))();
        }

        private IsCancelled _IsCancelled;
        /// <summary>
    ///     The provider can query to see if the operation has been cancelled.
    ///     This provides for a gentle way for the caller to notify the callee that
    ///     they don't want any more results.
    /// </summary>
    /// <returns>returns TRUE if the operation has been cancelled.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool IsCancelled( ) {
            CheckDisposed();
            return  (_IsCancelled ?? (_IsCancelled = (_callback.Resolve<IsCancelled>() ?? (()=> default(bool) ) )))();
        }

        #endregion

        #region generate-dispatcher request-apis

        private OkToContinue _OkToContinue;
        /// <summary>
    ///     The provider can query to see if the operation has been cancelled.
    ///     This provides for a gentle way for the caller to notify the callee that
    ///     they don't want any more results. It's essentially just !IsCancelled()
    /// </summary>
    /// <returns>returns FALSE if the operation has been cancelled.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool OkToContinue( ) {
            CheckDisposed();
            return  (_OkToContinue ?? (_OkToContinue = (_callback.Resolve<OkToContinue>() ?? (()=> default(bool) ) )))();
        }

        private YieldPackage _YieldPackage;
        /// <summary>
    ///     Used by a provider to return fields for a SoftwareIdentity.
    /// </summary>
    /// <param name="fastPath"></param>
    /// <param name="name"></param>
    /// <param name="version"></param>
    /// <param name="versionScheme"></param>
    /// <param name="summary"></param>
    /// <param name="source"></param>
    /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool YieldPackage(string fastPath ,string name ,string version ,string versionScheme ,string summary ,string source  ) {
            CheckDisposed();
            return  (_YieldPackage ?? (_YieldPackage = (_callback.Resolve<YieldPackage>() ?? ((pfastPath,pname,pversion,pversionScheme,psummary,psource)=> default(bool) ) )))(fastPath,name,version,versionScheme,summary,source);
        }

        private YieldSource _YieldSource;
        /// <summary>
    ///     Used by a provider to return fields for a package source (repository)
    /// </summary>
    /// <param name="name"></param>
    /// <param name="location"></param>
    /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool YieldSource(string name ,string location ,bool isTrusted  ) {
            CheckDisposed();
            return  (_YieldSource ?? (_YieldSource = (_callback.Resolve<YieldSource>() ?? ((pname,plocation,pisTrusted)=> default(bool) ) )))(name,location,isTrusted);
        }

        private YieldMetadataDefinition _YieldMetadataDefinition;
        /// <summary>
    ///     Used by a provider to return the fields for a Metadata Definition
    ///     The cmdlets can use this to supply tab-completion for metadata to the user.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="expectedType"></param>
    /// <param name="permittedValues"></param>
    /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool YieldMetadataDefinition(string name ,string expectedType ,IEnumerable<string> permittedValues  ) {
            CheckDisposed();
            return  (_YieldMetadataDefinition ?? (_YieldMetadataDefinition = (_callback.Resolve<YieldMetadataDefinition>() ?? ((pname,pexpectedType,ppermittedValues)=> default(bool) ) )))(name,expectedType,permittedValues);
        }

        private YieldInstallationOptionsDefinition _YieldInstallationOptionsDefinition;
        /// <summary>
    ///     Used by a provider to return the fields for an Installation Option Definition
    ///     The cmdlets can use this to supply tab-completion for installation options to the user.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="expectedType"></param>
    /// <param name="required"></param>
    /// <param name="permittedValues"></param>
    /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool YieldInstallationOptionsDefinition(string name ,string expectedType ,bool required ,IEnumerable<string> permittedValues  ) {
            CheckDisposed();
            return  (_YieldInstallationOptionsDefinition ?? (_YieldInstallationOptionsDefinition = (_callback.Resolve<YieldInstallationOptionsDefinition>() ?? ((pname,pexpectedType,prequired,ppermittedValues)=> default(bool) ) )))(name,expectedType,required,permittedValues);
        }

        #endregion

        #region generate-dispatcher host-apis

        private GetMetadataKeys _GetMetadataKeys;
        /// <summary>
    ///     Used by a provider to request what metadata keys were passed from the user
    /// </summary>
    /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public IEnumerable<string> GetMetadataKeys( ) {
            CheckDisposed();
            return  (_GetMetadataKeys ?? (_GetMetadataKeys = (_callback.Resolve<GetMetadataKeys>() ?? (()=> default(IEnumerable<string>) ) )))();
        }

        private GetMetadataValues _GetMetadataValues;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public IEnumerable<string> GetMetadataValues(string key  ) {
            CheckDisposed();
            return  (_GetMetadataValues ?? (_GetMetadataValues = (_callback.Resolve<GetMetadataValues>() ?? ((pkey)=> default(IEnumerable<string>) ) )))(key);
        }

        private GetInstallationOptionKeys _GetInstallationOptionKeys;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public IEnumerable<string> GetInstallationOptionKeys( ) {
            CheckDisposed();
            return  (_GetInstallationOptionKeys ?? (_GetInstallationOptionKeys = (_callback.Resolve<GetInstallationOptionKeys>() ?? (()=> default(IEnumerable<string>) ) )))();
        }

        private GetInstallationOptionValues _GetInstallationOptionValues;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public IEnumerable<string> GetInstallationOptionValues(string key  ) {
            CheckDisposed();
            return  (_GetInstallationOptionValues ?? (_GetInstallationOptionValues = (_callback.Resolve<GetInstallationOptionValues>() ?? ((pkey)=> default(IEnumerable<string>) ) )))(key);
        }

        private PackageSources _PackageSources;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public IEnumerable<string> PackageSources( ) {
            CheckDisposed();
            return  (_PackageSources ?? (_PackageSources = (_callback.Resolve<PackageSources>() ?? (()=> default(IEnumerable<string>) ) )))();
        }

        private GetConfiguration _GetConfiguration;
        /// <summary>
    ///     Returns a string collection of values from a specified path in a hierarchal
    ///     configuration hashtable.
    /// </summary>
    /// <param name="path">
    ///     Path to the configuration key. Nodes are traversed by specifying a '/' character:
    ///     Example: "Providers/Module" ""
    /// </param>
    /// <returns>
    ///     A collection of string values from the configuration.
    ///     Returns an empty collection if no data is found for that path
    /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public IEnumerable<string> GetConfiguration(string path  ) {
            CheckDisposed();
            return  (_GetConfiguration ?? (_GetConfiguration = (_callback.Resolve<GetConfiguration>() ?? ((ppath)=> default(IEnumerable<string>) ) )))(path);
        }

        private ShouldContinueWithUntrustedPackageSource _ShouldContinueWithUntrustedPackageSource;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool ShouldContinueWithUntrustedPackageSource(string package ,string packageSource  ) {
            CheckDisposed();
            return  (_ShouldContinueWithUntrustedPackageSource ?? (_ShouldContinueWithUntrustedPackageSource = (_callback.Resolve<ShouldContinueWithUntrustedPackageSource>() ?? ((ppackage,ppackageSource)=> default(bool) ) )))(package,packageSource);
        }

        private ShouldProcessPackageInstall _ShouldProcessPackageInstall;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool ShouldProcessPackageInstall(string packageName ,string version ,string source  ) {
            CheckDisposed();
            return  (_ShouldProcessPackageInstall ?? (_ShouldProcessPackageInstall = (_callback.Resolve<ShouldProcessPackageInstall>() ?? ((ppackageName,pversion,psource)=> default(bool) ) )))(packageName,version,source);
        }

        private ShouldProcessPackageUninstall _ShouldProcessPackageUninstall;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool ShouldProcessPackageUninstall(string packageName ,string version  ) {
            CheckDisposed();
            return  (_ShouldProcessPackageUninstall ?? (_ShouldProcessPackageUninstall = (_callback.Resolve<ShouldProcessPackageUninstall>() ?? ((ppackageName,pversion)=> default(bool) ) )))(packageName,version);
        }

        private ShouldContinueAfterPackageInstallFailure _ShouldContinueAfterPackageInstallFailure;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool ShouldContinueAfterPackageInstallFailure(string packageName ,string version ,string source  ) {
            CheckDisposed();
            return  (_ShouldContinueAfterPackageInstallFailure ?? (_ShouldContinueAfterPackageInstallFailure = (_callback.Resolve<ShouldContinueAfterPackageInstallFailure>() ?? ((ppackageName,pversion,psource)=> default(bool) ) )))(packageName,version,source);
        }

        private ShouldContinueAfterPackageUninstallFailure _ShouldContinueAfterPackageUninstallFailure;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool ShouldContinueAfterPackageUninstallFailure(string packageName ,string version ,string source  ) {
            CheckDisposed();
            return  (_ShouldContinueAfterPackageUninstallFailure ?? (_ShouldContinueAfterPackageUninstallFailure = (_callback.Resolve<ShouldContinueAfterPackageUninstallFailure>() ?? ((ppackageName,pversion,psource)=> default(bool) ) )))(packageName,version,source);
        }

        private ShouldContinueRunningInstallScript _ShouldContinueRunningInstallScript;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool ShouldContinueRunningInstallScript(string packageName ,string version ,string source ,string scriptLocation  ) {
            CheckDisposed();
            return  (_ShouldContinueRunningInstallScript ?? (_ShouldContinueRunningInstallScript = (_callback.Resolve<ShouldContinueRunningInstallScript>() ?? ((ppackageName,pversion,psource,pscriptLocation)=> default(bool) ) )))(packageName,version,source,scriptLocation);
        }

        private ShouldContinueRunningUninstallScript _ShouldContinueRunningUninstallScript;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool ShouldContinueRunningUninstallScript(string packageName ,string version ,string source ,string scriptLocation  ) {
            CheckDisposed();
            return  (_ShouldContinueRunningUninstallScript ?? (_ShouldContinueRunningUninstallScript = (_callback.Resolve<ShouldContinueRunningUninstallScript>() ?? ((ppackageName,pversion,psource,pscriptLocation)=> default(bool) ) )))(packageName,version,source,scriptLocation);
        }

        private AskPermission _AskPermission;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool AskPermission(string permission  ) {
            CheckDisposed();
            return  (_AskPermission ?? (_AskPermission = (_callback.Resolve<AskPermission>() ?? ((ppermission)=> default(bool) ) )))(permission);
        }

        private WhatIf _WhatIf;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Generated Code")]
        public bool WhatIf( ) {
            CheckDisposed();
            return  (_WhatIf ?? (_WhatIf = (_callback.Resolve<WhatIf>() ?? (()=> default(bool) ) )))();
        }

        #endregion

        #region generate-dispatcher protocol-apis

        #endregion

        public void Dispose() {
            // Clearing all of these ensures that the transient APIs 
            // can't be called outside of the appropriate scope.

            #region dispose-dispatcher service-apis
            _GetNuGetExePath = null;
            _GetNuGetDllPath = null;
            _DownloadFile = null;
            _AddPinnedItemToTaskbar = null;
            _RemovePinnedItemFromTaskbar = null;
            _CreateShortcutLink = null;
            _UnzipFileIncremental = null;
            _UnzipFile = null;
            _AddFileAssociation = null;
            _RemoveFileAssociation = null;
            _AddExplorerMenuItem = null;
            _RemoveExplorerMenuItem = null;
            _SetEnvironmentVariable = null;
            _RemoveEnvironmentVariable = null;
            _AddFolderToPath = null;
            _RemoveFolderFromPath = null;
            _InstallMSI = null;
            _RemoveMSI = null;
            _StartProcess = null;
            _InstallVSIX = null;
            _UninstallVSIX = null;
            _InstallPowershellScript = null;
            _UninstallPowershellScript = null;
            _SearchForExecutable = null;
            _GetUserBinFolder = null;
            _GetSystemBinFolder = null;
            _CopyFile = null;
            _CopyFolder = null;
            _Delete = null;
            _DeleteFolder = null;
            _CreateFolder = null;
            _DeleteFile = null;
            _BeginTransaction = null;
            _AbortTransaction = null;
            _EndTransaction = null;
            _GenerateUninstallScript = null;
            _GetKnownFolder = null;
            _IsElevated = null;
            #endregion

            #region dispose-dispatcher core-apis
            _Warning = null;
            _Message = null;
            _Error = null;
            _Debug = null;
            _Verbose = null;
            _ExceptionThrown = null;
            _Progress = null;
            _ProgressComplete = null;
            _GetHostDelegate = null;
            _IsCancelled = null;
            #endregion

            #region dispose-dispatcher request-apis
            _OkToContinue = null;
            _YieldPackage = null;
            _YieldSource = null;
            _YieldMetadataDefinition = null;
            _YieldInstallationOptionsDefinition = null;
            #endregion

            #region dispose-dispatcher host-apis
            _GetMetadataKeys = null;
            _GetMetadataValues = null;
            _GetInstallationOptionKeys = null;
            _GetInstallationOptionValues = null;
            _PackageSources = null;
            _GetConfiguration = null;
            _ShouldContinueWithUntrustedPackageSource = null;
            _ShouldProcessPackageInstall = null;
            _ShouldProcessPackageUninstall = null;
            _ShouldContinueAfterPackageInstallFailure = null;
            _ShouldContinueAfterPackageUninstallFailure = null;
            _ShouldContinueRunningInstallScript = null;
            _ShouldContinueRunningUninstallScript = null;
            _AskPermission = null;
            _WhatIf = null;
            #endregion

            #region dispose-dispatcher protocol-apis

            #endregion

            _callback = null;
        }

        private void CheckDisposed() {
            if (IsDisposed) {
                throw new Exception("Invalid State--past call lifespan");
            }
        }
    }
}