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

namespace NuGet.OneGet {
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Security;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using global::NuGet;
    using RequestImpl = System.MarshalByRefObject;

    public abstract class Request : IDisposable {
        #region copy core-apis

        /* Synced/Generated code =================================================== */
        /// <summary>
        ///     The provider can query to see if the operation has been cancelled.
        ///     This provides for a gentle way for the caller to notify the callee that
        ///     they don't want any more results.
        /// </summary>
        /// <returns>returns TRUE if the operation has been cancelled.</returns>
        public abstract bool IsCancelled();

        /// <summary>
        ///     Returns a reference to the PackageManagementService API
        ///     The consumer of this function should either use this as a dynamic object
        ///     Or DuckType it to an interface that resembles IPacakgeManagementService
        /// </summary>
        /// <param name="requestImpl"></param>
        /// <returns></returns>
        public abstract object GetPackageManagementService(RequestImpl requestImpl);

        /// <summary>
        ///     Returns the interface type for a Request that the OneGet Core is expecting
        ///     This is (currently) neccessary to provide an appropriately-typed version
        ///     of the Request to the core when a Plugin is calling back into the core
        ///     and has to pass a request object.
        /// </summary>
        /// <returns></returns>
        public abstract Type GetIRequestInterface();

        public abstract bool NotifyBeforePackageInstall(string packageName, string version, string source, string destination);

        public abstract bool NotifyPackageInstalled(string packageName, string version, string source, string destination);

        public abstract bool NotifyBeforePackageUninstall(string packageName, string version, string source, string destination);

        public abstract bool NotifyPackageUninstalled(string packageName, string version, string source, string destination);

        public abstract string GetCanonicalPackageId(string providerName, string packageName, string version);

        public abstract string ParseProviderName(string canonicalPackageId);

        public abstract string ParsePackageName(string canonicalPackageId);

        public abstract string ParsePackageVersion(string canonicalPackageId);
        #endregion

        #region copy host-apis

        /* Synced/Generated code =================================================== */
        public abstract string GetMessageString(string messageText);

        public abstract bool Warning(string messageText);

        public abstract bool Error(string id, string category, string targetObjectValue, string messageText);

        public abstract bool Message(string messageText);

        public abstract bool Verbose(string messageText);

        public abstract bool Debug(string messageText);

        public abstract int StartProgress(int parentActivityId, string messageText);

        public abstract bool Progress(int activityId, int progressPercentage, string messageText);

        public abstract bool CompleteProgress(int activityId, bool isSuccessful);

        /// <summary>
        ///     Used by a provider to request what metadata keys were passed from the user
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<string> GetOptionKeys(int category);

        public abstract IEnumerable<string> GetOptionValues(int category, string key);

        public abstract IEnumerable<string> GetSources();

        public abstract string GetCredentialUsername();

        public abstract string GetCredentialPassword();

        public abstract bool ShouldBootstrapProvider(string requestor, string providerName, string providerVersion, string providerType, string location, string destination );

        public abstract bool ShouldContinueWithUntrustedPackageSource(string package, string packageSource);

        public abstract bool ShouldProcessPackageInstall(string packageName, string version, string source);

        public abstract bool ShouldProcessPackageUninstall(string packageName, string version);

        public abstract bool ShouldContinueAfterPackageInstallFailure(string packageName, string version, string source);

        public abstract bool ShouldContinueAfterPackageUninstallFailure(string packageName, string version, string source);

        public abstract bool ShouldContinueRunningInstallScript(string packageName, string version, string source, string scriptLocation);

        public abstract bool ShouldContinueRunningUninstallScript(string packageName, string version, string source, string scriptLocation);

        public abstract bool AskPermission(string permission);

        public abstract bool IsInteractive();

        public abstract int CallCount();
        #endregion

        #region copy service-apis

        /* Synced/Generated code =================================================== */
        public abstract void DownloadFile(Uri remoteLocation, string localFilename, RequestImpl requestImpl);

        public abstract bool IsSupportedArchive(string localFilename, RequestImpl requestImpl);

        public abstract IEnumerable<string> UnpackArchive(string localFilename, string destinationFolder, RequestImpl requestImpl);

        public abstract void AddPinnedItemToTaskbar(string item, RequestImpl requestImpl);

        public abstract void RemovePinnedItemFromTaskbar(string item, RequestImpl requestImpl);

        public abstract void CreateShortcutLink(string linkPath, string targetPath, string description, string workingDirectory, string arguments, RequestImpl requestImpl);

        public abstract void SetEnvironmentVariable(string variable, string value, int context, RequestImpl requestImpl);

        public abstract void RemoveEnvironmentVariable(string variable, int context, RequestImpl requestImpl);

        public abstract void CopyFile(string sourcePath, string destinationPath, RequestImpl requestImpl);

        public abstract void Delete(string path, RequestImpl requestImpl);

        public abstract void DeleteFolder(string folder, RequestImpl requestImpl);

        public abstract void CreateFolder(string folder, RequestImpl requestImpl);

        public abstract void DeleteFile(string filename, RequestImpl requestImpl);

        public abstract string GetKnownFolder(string knownFolder, RequestImpl requestImpl);

        public abstract bool IsElevated(RequestImpl requestImpl);
        #endregion

        #region copy response-apis

        /* Synced/Generated code =================================================== */

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
        /// <param name="fullPath"></param>
        /// <param name="packageFileName"></param>
        /// <returns></returns>
        public abstract bool YieldSoftwareIdentity(string fastPath, string name, string version, string versionScheme, string summary, string source, string searchKey, string fullPath, string packageFileName);

        public abstract bool YieldSoftwareMetadata(string parentFastPath, string name, string value);

        public abstract bool YieldEntity(string parentFastPath, string name, string regid, string role, string thumbprint);

        public abstract bool YieldLink(string parentFastPath, string referenceUri, string relationship, string mediaType, string ownership, string use, string appliesToMedia, string artifact);

        #if M2
        public abstract bool YieldSwidtag(string fastPath, string xmlOrJsonDoc);

        public abstract bool YieldMetadata(string fieldId, string @namespace, string name, string value);

        #endif 

        /// <summary>
        ///     Used by a provider to return fields for a package source (repository)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="isTrusted"></param>
        /// <param name="isRegistered"></param>
        /// <param name="isValidated"></param>
        /// <returns></returns>
        public abstract bool YieldPackageSource(string name, string location, bool isTrusted,bool isRegistered, bool isValidated);

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
/* Synced/Generated code =================================================== */

        public bool Warning(string messageText, params object[] args) {
            return Warning(FormatMessageString(messageText,args));
        }

        internal bool Error( ErrorCategory category, string targetObjectValue, string messageText, params object[] args) {
            return Error(messageText, category.ToString(), targetObjectValue, FormatMessageString(messageText, args));
        }

        internal bool ThrowError(ErrorCategory category, string targetObjectValue, string messageText, params object[] args) {
            Error(messageText, category.ToString(), targetObjectValue, FormatMessageString(messageText, args));
            throw new Exception("MSG:TerminatingError");
        }

        public bool Message(string messageText, params object[] args) {
            return Message(FormatMessageString(messageText,args));
        }

        public bool Verbose(string messageText, params object[] args) {
            return Verbose(FormatMessageString(messageText,args));
        } 

        public bool Debug(string messageText, params object[] args) {
            return Debug(FormatMessageString(messageText,args));
        }

        public int StartProgress(int parentActivityId, string messageText, params object[] args) {
            return StartProgress(parentActivityId, FormatMessageString(messageText,args));
        }

        public bool Progress(int activityId, int progressPercentage, string messageText, params object[] args) {
            return Progress(activityId, progressPercentage, FormatMessageString(messageText,args));
        }

        private static string FixMeFormat(string formatString, object[] args) {
            if (args == null || args.Length == 0 ) {
                // not really any args, and not really expectng any
                return formatString.Replace('{', '\u00ab').Replace('}', '\u00bb');
            }
            return System.Linq.Enumerable.Aggregate(args, "FIXME/Format:" + formatString.Replace('{', '\u00ab').Replace('}', '\u00bb'), (current, arg) => current + string.Format(CultureInfo.CurrentCulture," \u00ab{0}\u00bb", arg));
        }

        internal string GetMessageStringInternal(string messageText) {
            return Messages.ResourceManager.GetString(messageText);
        }

        internal string FormatMessageString(string messageText, object[] args) {
            if (string.IsNullOrEmpty(messageText)) {
                return string.Empty;
            }

            if (messageText.StartsWith(Constants.MSGPrefix, true, CultureInfo.CurrentCulture)) {
                // check with the caller first, then with the local resources, and fallback to using the messageText itself.
                messageText = GetMessageString(messageText.Substring(Constants.MSGPrefix.Length)) ?? GetMessageStringInternal(messageText) ?? messageText;    
            }

            // if it doesn't look like we have the correct number of parameters
            // let's return a fixmeformat string.
            var c = System.Linq.Enumerable.Count( System.Linq.Enumerable.Where(messageText.ToCharArray(), each => each == '{'));
            if (c < args.Length) {
                return FixMeFormat(messageText, args);
            }
            return string.Format(CultureInfo.CurrentCulture, messageText, args);
        }

        public SecureString Password {
            get {
                var p = GetCredentialPassword();
                if (p == null) {
                    return null;
                }
                return p.FromProtectedString("salt");
            }
        }

        public string Username {
            get {
                return  GetCredentialUsername();
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing) {

        }

        public static implicit operator MarshalByRefObject(Request req) {
            return req.RemoteThis;
        }

        public static MarshalByRefObject ToMarshalByRefObject(Request request) {
            return request.RemoteThis;
        }

        internal MarshalByRefObject RemoteThis {
            get {
                return Extend();
            }
        }

        internal MarshalByRefObject Extend(params object[] objects) {
            return RequestExtensions.Extend(this, GetIRequestInterface(), objects);
        }

        #endregion

        private static readonly Regex _rxFastPath = new Regex(@"\$(?<source>[\w,\+,\/,=]*)\\(?<id>[\w,\+,\/,=]*)\\(?<version>[\w,\+,\/,=]*)");
        private static readonly Regex _rxPkgParse = new Regex(@"'(?<pkgId>\S*)\s(?<ver>.*?)'");

        private string _configurationFileLocation;

        internal string ConfigurationFileLocation {
            get {
                if (string.IsNullOrEmpty(_configurationFileLocation)) {
                    // get the value from the request
                    var path = GetValue(OptionCategory.Source, "ConfigFile");
                    if (!string.IsNullOrEmpty(path)) {
                        return path;
                    }

                    //otherwise, use %APPDATA%/NuGet/NuGet.Config
                    _configurationFileLocation = Path.Combine(GetKnownFolder("ApplicationData", RemoteThis), "NuGet", "NuGet.config");
                }
                return _configurationFileLocation;
            }
        }

        internal string[] Tag {
            get {
                return GetValues(OptionCategory.Package, "Tag").ToArray();
            }
        }

        internal string Contains {
            get {
                return GetValue(OptionCategory.Package, "Contains");
            }
        }

        internal string Destination {
            get {
                return Path.GetFullPath(GetValue(OptionCategory.Install, "Destination"));
            }
        }

        internal bool SkipValidate {
            get {
                return GetValue(OptionCategory.Source, "SkipValidate").IsTrue();
            }
        }

        internal bool AllowPrereleaseVersions {
            get {
                return GetValue(OptionCategory.Package, "AllowPrereleaseVersions").IsTrue();
            }
        }

        internal bool AllVersions {
            get {
                return GetValue(OptionCategory.Package, "AllVersions").IsTrue();
            }
        }

        internal bool SkipDependencies {
            get {
                return GetValue(OptionCategory.Install, "SkipDependencies").IsTrue();
            }
        }

        internal bool ContinueOnFailure {
            get {
                return GetValue(OptionCategory.Install, "ContinueOnFailure").IsTrue();
            }
        }

        internal bool ExcludeVersion {
            get {
                return GetValue(OptionCategory.Install, "ExcludeVersion").IsTrue();
            }
        }

        internal string PackageSaveMode {
            get {
                return GetValue(OptionCategory.Install, "PackageSaveMode");
            }
        }

        internal XDocument Config {
            get {
                try {
                    var doc = XDocument.Load(ConfigurationFileLocation);
                    if (doc.Root != null && doc.Root.Name == "configuration") {
                        return doc;
                    }
                    // doc root isn't right. make a new one!
                } catch {
                    // a bad xml doc.
                }
                return XDocument.Load(new MemoryStream(Encoding.UTF8.GetBytes(Constants.DefaultConfig)));
            }
            set {
                if (value == null) {
                    return;
                }

                Verbose("Saving NuGet Config {0}", ConfigurationFileLocation);

                CreateFolder(Path.GetDirectoryName(ConfigurationFileLocation), RemoteThis);
                value.Save(ConfigurationFileLocation);
            }
        }

        internal IDictionary<string, PackageSource> RegisteredPackageSources {
            get {
                try {
                    return Config.XPathSelectElements("/configuration/packageSources/add")
                        .Where(each => each.Attribute("key") != null && each.Attribute("value") != null)
                        .ToDictionaryNicely(each => each.Attribute("key").Value, each => new PackageSource {
                            Name = each.Attribute("key").Value,
                            Location = each.Attribute("value").Value,
                            Trusted = each.Attributes("trusted").Any() && each.Attribute("trusted").Value.IsTrue(),
                            IsRegistered = true,
                            IsValidated = each.Attributes("validated").Any() && each.Attribute("validated").Value.IsTrue(),
                        }, StringComparer.OrdinalIgnoreCase);
                } catch (Exception e) {
                    e.Dump(this);
                }
                return new Dictionary<string, PackageSource>(StringComparer.OrdinalIgnoreCase) {
                    {
                        "nuget.org", new PackageSource {
                            Name = "nuget.org",
                            Location = "https://www.nuget.org/api/v2/",
                            Trusted = false,
                            IsRegistered = false,
                            IsValidated = true,
                        }
                    }
                };
            }
        }

        internal IEnumerable<PackageSource> SelectedSources {
            get {
                var sources = (GetSources() ?? Enumerable.Empty<string>()).ToArray();
                var pkgSources = RegisteredPackageSources;

                if (sources.Length == 0) {
                    // return them all.
                    foreach (var src in pkgSources.Values) {
                        yield return src;
                    }
                    yield break;
                }

                // otherwise, return packaeg sources that match the items given.
                foreach (var src in sources) {

                    // check to see if we have a source with either that name 
                    // or that URI first.
                    if (pkgSources.ContainsKey(src)) {
                        yield return pkgSources[src];
                        continue;
                    }

                    var srcLoc = src;
                    bool found = false;
                    foreach (var byLoc in pkgSources.Values.Where(each => each.Location == srcLoc)) {
                        yield return byLoc;
                        found = true;
                    }
                    if (found) {
                        continue;
                    }

                    // doesn't look like we have this as a source.
                    if (Uri.IsWellFormedUriString(src, UriKind.Absolute)) {
                        // we have been passed in an URI
                        var srcUri = new Uri(src);
                        if (NuGetProvider.SupportedSchemes.Contains(srcUri.Scheme.ToLower())) {
                            // it's one of our supported uri types.
                            var isValidated = false;

                            if (!SkipValidate) {
                                isValidated = ValidateSourceUri(srcUri);
                            }

                            if (SkipValidate || isValidated) {
                                yield return new PackageSource {
                                    Location = srcUri.ToString(),
                                    Name = srcUri.ToString(),
                                    Trusted = false,
                                    IsRegistered = false,
                                    IsValidated = isValidated,
                                };
                                continue;
                            }
                            Error(ErrorCategory.InvalidArgument, src,  Constants.SourceLocationNotValid, src);
                            Warning(Constants.UnableToResolveSource, src);
                            continue;
                        }

                        // hmm. not a valid location?
                        Error(ErrorCategory.InvalidArgument, src, Constants.UriSchemeNotSupported, src);
                        Warning(Constants.UnableToResolveSource, src);
                        continue;
                    }

                    // is it a file path?
                    if (Directory.Exists(src)) {
                        yield return new PackageSource {
                            Location = src,
                            Name = src,
                            Trusted = true,
                            IsRegistered = false,
                            IsValidated = true,
                        };
                    } else {
                        // hmm. not a valid location?
                        Warning(Constants.UnableToResolveSource, src);
                    }
                }
            }
        }

        private static string NuGetExePath {
            get {
                return typeof (AggregateRepository).Assembly.Location;
            }
        }

        public bool Yield(KeyValuePair<string, string[]> pair) {
            if (pair.Value.Length == 0) {
                return YieldKeyValuePair(pair.Key, null);
            }
            return pair.Value.All(each => YieldKeyValuePair(pair.Key, each));
        }

        public bool YieldDynamicOption(OptionCategory category, string name, OptionType expectedType, bool isRequired) {
            return YieldDynamicOption((int)category, name, (int)expectedType, isRequired);
        }

        public bool YieldDynamicOption(OptionCategory category, string name, OptionType expectedType, bool isRequired, IEnumerable<string> permittedValues) {
            return YieldDynamicOption((int)category, name, (int)expectedType, isRequired) && (permittedValues ?? Enumerable.Empty<string>()).All(each => YieldKeyValuePair(name, each));
        }

        private string GetValue(OptionCategory category, string name) {
            // get the value from the request
            return (GetOptionValues((int)category, name) ?? Enumerable.Empty<string>()).LastOrDefault();
        }

        private IEnumerable<string> GetValues(OptionCategory category, string name) {
            // get the value from the request
            return (GetOptionValues((int)category, name) ?? Enumerable.Empty<string>());
        }

        internal void RemovePackageSource(string id) {
            var config = Config;
            var source = config.XPathSelectElements(string.Format("/configuration/packageSources/add[@key='{0}']", id)).FirstOrDefault();
            if (source != null) {
                source.Remove();
                Config = config;
            }
        }

        internal void AddPackageSource(string name, string location, bool isTrusted, bool isValidated) {
            if (SkipValidate || ValidateSourceLocation(location)) {
                var config = Config;
                var sources = config.XPathSelectElements("/configuration/packageSources").FirstOrDefault();
                if (sources == null) {
                    config.Root.Add(sources = new XElement("packageSources"));
                }
                var source = new XElement("add");
                source.SetAttributeValue("key", name);
                source.SetAttributeValue("value", location);
                if (isValidated) {
                    source.SetAttributeValue("validated", true);
                }
                if (isTrusted) {
                    source.SetAttributeValue("trusted", true);
                }
                sources.Add(source);
                Config = config;
            }
        }

        private bool  LocationCloseEnoughMatch(string givenLocation, string knownLocation) {
            if (givenLocation.Equals(knownLocation, StringComparison.OrdinalIgnoreCase)) {
                return true;
            }

            // make trailing slashes consistent
            if (givenLocation.TrimEnd('/').Equals(knownLocation.TrimEnd('/'), StringComparison.OrdinalIgnoreCase)) {
                return true;
            }

            // and trailing backslashes
            if (givenLocation.TrimEnd('\\').Equals(knownLocation.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase)) {
                return true;
            }
            return false;
        }

        internal PackageSource FindRegisteredSource(string name) {
            var srcs = RegisteredPackageSources;
            if (srcs.ContainsKey(name)) {
                return srcs[name];
            }

            var src = srcs.Values.FirstOrDefault(each =>LocationCloseEnoughMatch(name, each.Location));
            if (src != null) {
                return src;
            }

            return null;
        }

        internal bool ValidateSourceLocation(string location) {
            if (Uri.IsWellFormedUriString(location, UriKind.Absolute)) {
                return ValidateSourceUri(new Uri(location));
            }
            try {
                if (Directory.Exists(location) || File.Exists(location)) {
                    return true;
                }
            } catch {
            }
            return false;
        }

        private bool ValidateSourceUri(Uri srcUri) {
            if (!NuGetProvider.SupportedSchemes.Contains(srcUri.Scheme.ToLowerInvariant())) {
                return false;
            }

            if (srcUri.IsFile) {
                var path = srcUri.ToString().CanonicalizePath(false);

                if (Directory.Exists(path)) {
                    return true;
                }
                return false;
            }

            // todo: do a get on the uri and see if it responds.
            try {
                var repo = PackageRepositoryFactory.Default.CreateRepository(srcUri.ToString());
                var drepo = repo as DataServicePackageRepository;
                if (drepo != null) {
                    drepo.FindPackagesById("xx");
                }
                return true;
            } catch {
                // nope.
            }

            return false;
        }

        internal PackageSource ResolvePackageSource(string nameOrLocation) {
            var source = FindRegisteredSource(nameOrLocation);
            if (source != null) {
                return source;
            }

            try {
                // is the given value a filename?
                if (File.Exists(nameOrLocation)) {
                    return new PackageSource() {
                        IsRegistered = false,
                        IsValidated = true,
                        Location = nameOrLocation,
                        Name = nameOrLocation,
                        Trusted = true,
                    };
                }
            }
            catch {
            }

            try {
                // is the given value a directory?
                if (Directory.Exists(nameOrLocation)) {
                    return new PackageSource() {
                        IsRegistered = false,
                        IsValidated = true,
                        Location = nameOrLocation,
                        Name = nameOrLocation,
                        Trusted = true,
                    };
                }
            }
            catch {
            }

            if (Uri.IsWellFormedUriString(nameOrLocation, UriKind.Absolute)) {
                var uri = new Uri(nameOrLocation, UriKind.Absolute);
                if (!NuGetProvider.SupportedSchemes.Contains(uri.Scheme.ToLowerInvariant())) {
                    Error(ErrorCategory.InvalidArgument, uri.ToString(), Constants.UriSchemeNotSupported, uri);
                    return null;
                }

                // this is an URI, and it looks like one type that we support
                if (SkipValidate || ValidateSourceUri(uri)) {
                    return new PackageSource {
                        IsRegistered = false,
                        IsValidated = !SkipValidate,
                        Location = nameOrLocation,
                        Name = nameOrLocation,
                        Trusted = false,
                    };
                }    
            }

            Error(ErrorCategory.InvalidArgument, nameOrLocation, Constants.UnableToResolveSource, nameOrLocation);
            return null;
        }

        internal IEnumerable<IPackage> FilterOnVersion(IEnumerable<IPackage> pkgs, string requiredVersion, string minimumVersion, string maximumVersion) {
            if (!string.IsNullOrEmpty(requiredVersion)) {
                pkgs = pkgs.Where(each => each.Version == new SemanticVersion(requiredVersion));
            }

            if (!string.IsNullOrEmpty(minimumVersion)) {
                pkgs = pkgs.Where(each => each.Version >= new SemanticVersion(minimumVersion));
            }

            if (!string.IsNullOrEmpty(maximumVersion)) {
                pkgs = pkgs.Where(each => each.Version <= new SemanticVersion(maximumVersion));
            }

            return pkgs;
        }

        internal string MakeFastPath(PackageSource source, string id, string version) {
            return string.Format(@"${0}\{1}\{2}", source.Serialized, id.ToBase64(), version.ToBase64());
        }

        public bool TryParseFastPath(string fastPath, out string source, out string id, out string version) {
            var match = _rxFastPath.Match(fastPath);
            source = match.Success ? match.Groups["source"].Value.FromBase64() : null;
            id = match.Success ? match.Groups["id"].Value.FromBase64() : null;
            version = match.Success ? match.Groups["version"].Value.FromBase64() : null;
            return match.Success;
        }

        internal bool YieldPackage(PackageItem pkg, string searchKey) {
            if (YieldSoftwareIdentity(pkg.FastPath, pkg.Package.Id, pkg.Package.Version.ToString(), "semver", pkg.Package.Summary, pkg.PackageSource.Name, searchKey, pkg.FullPath, pkg.PackageFilename)) {
                if (!YieldSoftwareMetadata(pkg.FastPath, "copyright", pkg.Package.Copyright)) {
                    return false;
                }
                if (!YieldSoftwareMetadata(pkg.FastPath, "description", pkg.Package.Description)) {
                    return false;
                }
                if (!YieldSoftwareMetadata(pkg.FastPath, "language", pkg.Package.Language)) {
                    return false;
                }
                if (!YieldSoftwareMetadata(pkg.FastPath, "releaseNotes", pkg.Package.ReleaseNotes)) {
                    return false;
                }
                if (!YieldSoftwareMetadata(pkg.FastPath, "tags", pkg.Package.Tags)) {
                    return false;
                }
                if (!YieldSoftwareMetadata(pkg.FastPath, "title", pkg.Package.Title)) {
                    return false;
                }
                if (!YieldSoftwareMetadata(pkg.FastPath, "developmentDependency", pkg.Package.DevelopmentDependency.ToString())) {
                    return false;
                }
                if (!YieldSoftwareMetadata(pkg.FastPath, "FromTrustedSource", pkg.PackageSource.Trusted.ToString())) {
                    return false;
                }
                if (pkg.Package.LicenseUrl != null && !string.IsNullOrEmpty(pkg.Package.LicenseUrl.ToString()) ) {
                    if(!YieldLink(pkg.FastPath, pkg.Package.LicenseUrl.ToString(), "license", null, null, null, null, null)) {
                        return false;
                    }
                }
                if (pkg.Package.ProjectUrl != null && !string.IsNullOrEmpty(pkg.Package.ProjectUrl.ToString())) {
                    if(!YieldLink(pkg.FastPath, pkg.Package.ProjectUrl.ToString(), "project", null, null, null, null, null)) {
                        return false;
                    }
                }
                if (pkg.Package.ReportAbuseUrl != null && !string.IsNullOrEmpty(pkg.Package.ReportAbuseUrl.ToString())) {
                    if(!YieldLink(pkg.FastPath, pkg.Package.ReportAbuseUrl.ToString(), "abuse", null, null, null, null, null) ) {
                        return false;
                    }
                }
                if (pkg.Package.IconUrl != null && !string.IsNullOrEmpty(pkg.Package.IconUrl.ToString())) {
                    if (!YieldLink(pkg.FastPath, pkg.Package.IconUrl.ToString(), "icon", null, null, null, null, null)) {
                        return false;
                    }
                }
                if (pkg.Package.Authors.Any(author => !YieldEntity(pkg.FastPath, author.Trim(), author.Trim(), "author", null))) {
                    return false;
                }

                if (pkg.Package.Owners.Any(owner => !YieldEntity(pkg.FastPath, owner.Trim(), owner.Trim(), "owner", null))) {
                    return false;
                }
            }
            return true;
        }

        internal bool YieldPackages(IEnumerable<PackageItem> packageReferences, string searchKey) {
            var foundPackage = false;
            if (packageReferences == null) {
                return false;
            }

            foreach (var pkg in packageReferences) {
                foundPackage = true;
                if (!YieldPackage(pkg, searchKey)) {
                    break;
                }
            }
            return foundPackage;
        }

        internal IEnumerable<PackageItem> GetPackageById(string name, string requiredVersion = null, string minimumVersion = null, string maximumVersion = null, bool allowUnlisted = false) {
            if (string.IsNullOrEmpty(name)) {
                return Enumerable.Empty<PackageItem>();
            }
            return SelectedSources.AsParallel().WithMergeOptions(ParallelMergeOptions.NotBuffered).SelectMany(source => {
                try {
                    var pkgs = source.Repository.FindPackagesById(name);

                    if (!AllVersions && (string.IsNullOrEmpty(requiredVersion) && string.IsNullOrEmpty(minimumVersion) && string.IsNullOrEmpty(maximumVersion))) {
                        pkgs = from p in pkgs where p.IsLatestVersion select p;
                    }

                    return FilterOnVersion(pkgs, requiredVersion, minimumVersion, maximumVersion)
                        .Select(pkg => new PackageItem {
                            Package = pkg,
                            PackageSource = source,
                            FastPath = MakeFastPath(source, pkg.Id, pkg.Version.ToString())
                        });
                } catch (Exception e) {
                    e.Dump(this);
                    return Enumerable.Empty<PackageItem>();
                }
            });
        }

        internal IEnumerable<IPackage> FilterOnName(IEnumerable<IPackage> pkgs, string name) {
            return pkgs.Where(each => each.Id.IndexOf(name, StringComparison.OrdinalIgnoreCase) > -1);
        }

        internal PackageItem GetPackageByFilePath(string filePath) {
            // todo: currently requires nupkg file in place.

            if (PackageHelper.IsPackageFile(filePath)) {
                var package = new ZipPackage(filePath);
                var source = ResolvePackageSource(filePath);

                return new PackageItem {
                    FastPath = MakeFastPath(source , package.Id, package.Version.ToString()),
                    PackageSource = source,
                    Package = package,
                    IsPackageFile = true,
                    FullPath = filePath,
                };
            }
            return null;
        }

        internal PackageItem GetPackageByFastpath(string fastPath) {
            string sourceLocation;
            string id;
            string version;

            if (TryParseFastPath(fastPath, out sourceLocation, out id, out version)) {
                var source = ResolvePackageSource(sourceLocation);

                if (source.IsSourceAFile) {
                    return GetPackageByFilePath(sourceLocation);
                }

                var pkg = source.Repository.FindPackage(id, new SemanticVersion(version));

                if (pkg != null) {
                    return new PackageItem {
                        FastPath = fastPath,
                        PackageSource = source,
                        Package = pkg,
                    };
                }
            }
            return null;
        }

        internal IEnumerable<PackageItem> SearchForPackages(string name, string requiredVersion, string minimumVersion, string maximumVersion) {
            return SelectedSources.AsParallel().WithMergeOptions(ParallelMergeOptions.NotBuffered).SelectMany(source => SearchSourceForPackages(source, name, requiredVersion, minimumVersion, maximumVersion));
        }

        private IEnumerable<PackageItem> SearchSourceForPackages(PackageSource source, string name, string requiredVersion, string minimumVersion, string maximumVersion) {
            var criteria = Contains;
            if (string.IsNullOrEmpty(criteria)) {
                criteria = name;
            }
            var packages = source.Repository.GetPackages().Find(criteria);

            // why does this method return less results? It looks the same to me!?
            // var packages = repository.Search(Hint.Is() ? Hint : name);

            IEnumerable<IPackage> pkgs = null;

            // query filtering:
            if (!AllVersions && (string.IsNullOrEmpty(requiredVersion) && string.IsNullOrEmpty(minimumVersion) && string.IsNullOrEmpty(maximumVersion))) {
                pkgs = packages.FindLatestVersion();
            }
            else {
                // post-query filtering:
                pkgs = packages;
            }

            // if they passed a name, restrict the search things that actually contain the name in the FullName.
            if (!string.IsNullOrEmpty(name)) {
                pkgs = FilterOnName(pkgs, name);
            }

            return FilterOnVersion(pkgs, requiredVersion, minimumVersion, maximumVersion)
                .Select(pkg => new PackageItem {
                    Package = pkg,
                    PackageSource = source,
                    FastPath = MakeFastPath(source, pkg.Id, pkg.Version.ToString())
                }); 
        }

        public bool IsPackageInstalled(string name, string version) {
#if find_installed_packages_with_nuspec 

            var nuspecs = from pkgFile in Directory.EnumerateFileSystemEntries(Destination, "*.nuspec", SearchOption.AllDirectories) select pkgFile ;

            foreach (var n in nuspecs) {
                // uh, do we have to parse these?
                // hmmm.
            }

            // or we could search in this folder for a directory with or without the version
            // then examine the contents.
            // hmmm. I'd rather let nuget do that if I can, it's better at it.

#endif
            return (from pkgFile in Directory.EnumerateFileSystemEntries(Destination, "*.nupkg", SearchOption.AllDirectories)
                where PackageHelper.IsPackageFile(pkgFile)
                select new ZipPackage(pkgFile))
                .Any(pkg => pkg.Id.Equals(name, StringComparison.OrdinalIgnoreCase) && pkg.Version.ToString().Equals(version, StringComparison.OrdinalIgnoreCase));
        }

        internal IEnumerable<PackageItem> GetUninstalledPackageDependencies(PackageItem packageItem) {
            foreach (var depSet in packageItem.Package.DependencySets) {
                foreach (var dep in depSet.Dependencies) {
                    // get all the packages that match this dependency
                    var depRefs = dep.VersionSpec == null ? GetPackageById(dep.Id).ToArray() : GetPackageByIdAndVersionSpec(dep.Id, dep.VersionSpec, true).ToArray();

                    if (depRefs.Length == 0) {
                        ThrowError(ErrorCategory.ObjectNotFound, packageItem.GetCanonicalId(this), Constants.DependencyResolutionError, GetCanonicalPackageId(Constants.ProviderName, dep.Id, ((object)dep.VersionSpec ?? "").ToString()));
                    }

                    if (depRefs.Any(each => IsPackageInstalled(each.Id, each.Version))) {
                        // we have a compatible version installed.
                        continue;
                    }

                    yield return depRefs[0];

                    // it's not installed. return this as a needed package, but first, get it's dependencies.
                    foreach (var nestedDep in GetUninstalledPackageDependencies(depRefs[0])) {
                        yield return nestedDep;
                    }
                }
            }
        }

        private PackageItem ParseOutputFull(PackageSource source, string packageId, string version, string line) {
            var match = _rxPkgParse.Match(line);
            if (match.Success) {
                var pkg = new PackageItem {
                    Id = match.Groups["pkgId"].Value,
                    Version = match.Groups["ver"].Value,
                };

                // if this was the package we started with, we can assume a bit more info,
                if (pkg.Id == packageId && pkg.Version == version) {
                    pkg.PackageSource = source;
                }
                pkg.FullPath = Path.Combine(Destination, ExcludeVersion ? pkg.Id : pkg.FullName);
                return pkg;
            }
            return null;
        }

        internal InstallResult NuGetInstall(PackageItem item) {
            var result = new InstallResult();

            using (
                var p = AsyncProcess.Start(NuGetExePath,
                    string.Format(@"install ""{0}"" -Version ""{1}"" -Source ""{2}"" -PackageSaveMode ""{4}""  -OutputDirectory ""{3}"" -Verbosity detailed {5}", item.Id, item.Version, item.PackageSource.Location, Destination, PackageSaveMode, ExcludeVersion ? "-ExcludeVersion" : ""))
                ) {
                foreach (var l in p.StandardOutput) {
                    if (string.IsNullOrEmpty(l)) {
                        continue;
                    }

                    Verbose("NuGet: {0}", l);
                    // Successfully installed 'ComicRack 0.9.162'.
                    if (l.Contains("Successfully installed")) {
                        result.GetOrAdd(InstallStatus.Successful, () => new List<PackageItem>()).Add(ParseOutputFull(item.PackageSource, item.Id, item.Version, l));
                        continue;
                    }
                    ;

                    if (l.Contains("already installed")) {
                        result.GetOrAdd(InstallStatus.AlreadyPresent, () => new List<PackageItem>()).Add(ParseOutputFull(item.PackageSource, item.Id, item.Version, l));
                        continue;
                    }

                    if (l.Contains("not installed")) {
                        result.GetOrAdd(InstallStatus.Failed, () => new List<PackageItem>()).Add(ParseOutputFull(item.PackageSource, item.Id, item.Version, l));
                        continue;
                    }
                }

                foreach (var l in p.StandardError.Where(l => !string.IsNullOrEmpty(l))) {
                    Warning("NuGet: {0}", l);
                }

                // if anything failed, this is a failure.
                // if we have a success message (and no failure), we'll count this as a success.
                result.Status = result.ContainsKey(InstallStatus.Failed) ? InstallStatus.Failed : result.ContainsKey(InstallStatus.Successful) ? InstallStatus.Successful : InstallStatus.AlreadyPresent;

                return result;
            }
        }

        internal IEnumerable<PackageItem> GetPackageByIdAndVersionSpec(string name, IVersionSpec versionSpec, bool allowUnlisted = false) {
            if (string.IsNullOrEmpty(name)) {
                return Enumerable.Empty<PackageItem>();
            }

            return SelectedSources.AsParallel().WithMergeOptions(ParallelMergeOptions.NotBuffered).SelectMany(source => {
                var pkgs = source.Repository.FindPackages(name, versionSpec, AllowPrereleaseVersions, allowUnlisted);

                /*
                // necessary?
                pkgs = from p in pkgs where p.IsLatestVersion select p;
                */

                var pkgs2 = pkgs.ToArray();

                return pkgs2.Select(pkg => new PackageItem {
                    Package = pkg,
                    PackageSource = source,
                    FastPath = MakeFastPath(source, pkg.Id, pkg.Version.ToString())
                });
            }).OrderByDescending(each => each.Package.Version);
        }

        internal bool InstallSinglePackage(PackageItem packageItem) {
            if (ShouldProcessPackageInstall(packageItem.Id, packageItem.Version, packageItem.PackageSource.Name)) {
                // Get NuGet to install the Package

                var results = NuGetInstall(packageItem);

                if (results.Status == InstallStatus.Successful) {
                    foreach (var installedPackage in results[InstallStatus.Successful]) {
                        if (!NotifyPackageInstalled(installedPackage.Id, installedPackage.Version, installedPackage.PackageSource.Name, installedPackage.FullPath)) {
                            // the caller has expressed that they are cancelling the install.
                            Verbose("NotifyPackageInstalled returned false--This is unexpected");
                            // todo: we should probablty uninstall this package unless the user said leave broken stuff behind
                            return false;
                        }
                        YieldPackage(packageItem, packageItem.PackageSource.Name);
                        // yay!
                    }
                    return true;
                }

                if (results.Status == InstallStatus.AlreadyPresent) {
                    // hmm Weird.
                    Verbose("Skipped Package '{0} v{1}' already installed", packageItem.Id, packageItem.Version);
                    return true;
                }
                Error(ErrorCategory.InvalidResult, packageItem.GetCanonicalId(this), Constants.MultiplePackagesInstalledExpectedOne, packageItem.GetCanonicalId(this));
            }
            return false;
        }
    }

    internal enum InstallStatus {
        Unknown,
        Successful,
        Failed,
        AlreadyPresent
    }

    internal class InstallResult : Dictionary<InstallStatus, List<PackageItem>> {
        internal InstallStatus Status = InstallStatus.Unknown;
    }
}