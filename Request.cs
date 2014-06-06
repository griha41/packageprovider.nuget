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
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Security;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using global::NuGet;
    using Microsoft.OneGet.Core.Platform;
    using Callback = System.MarshalByRefObject;

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

        public abstract object GetPackageManagementService(Callback c);
        #endregion

        #region copy host-apis

        /// <summary>
        ///     Used by a provider to request what metadata keys were passed from the user
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<string> GetOptionKeys(int category);

        public abstract IEnumerable<string> GetOptionValues(int category, string key);

        public abstract IEnumerable<string> GetSpecifiedPackageSources();

        public abstract string GetCredentialUsername();

        public abstract SecureString GetCredentialPassword();

        public abstract bool ShouldContinueWithUntrustedPackageSource(string package, string packageSource);

        public abstract bool ShouldProcessPackageInstall(string packageName, string version, string source);

        public abstract bool ShouldProcessPackageUninstall(string packageName, string version);

        public abstract bool ShouldContinueAfterPackageInstallFailure(string packageName, string version, string source);

        public abstract bool ShouldContinueAfterPackageUninstallFailure(string packageName, string version, string source);

        public abstract bool ShouldContinueRunningInstallScript(string packageName, string version, string source, string scriptLocation);

        public abstract bool ShouldContinueRunningUninstallScript(string packageName, string version, string source, string scriptLocation);

        public abstract bool AskPermission(string permission);

        public abstract bool WhatIf();

        public abstract bool PackageInstalled(string packageName, string version, string source, string destination);

        public abstract bool BeforePackageUninstall(string packageName, string version, string source, string destination);
        #endregion

        #region copy service-apis

        public abstract void DownloadFile(Uri remoteLocation, string localFilename, Callback c);

        public abstract bool IsSupportedArchive(string localFilename, Callback c);

        public abstract IEnumerable<string> UnpackArchive(string localFilename, string destinationFolder, Callback c);

        public abstract void AddPinnedItemToTaskbar(string item, Callback c);

        public abstract void RemovePinnedItemFromTaskbar(string item, Callback c);

        public abstract void CreateShortcutLink(string linkPath, string targetPath, string description, string workingDirectory, string arguments, Callback c);

        public abstract void SetEnvironmentVariable(string variable, string value, int context, Callback c);

        public abstract void RemoveEnvironmentVariable(string variable, int context, Callback c);

        public abstract void CopyFile(string sourcePath, string destinationPath, Callback c);

        public abstract void Delete(string path, Callback c);

        public abstract void DeleteFolder(string folder, Callback c);

        public abstract void CreateFolder(string folder, Callback c);

        public abstract void DeleteFile(string filename, Callback c);

        public abstract string GetKnownFolder(string knownFolder, Callback c);

        public abstract bool IsElevated(Callback c);
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


        private static dynamic _dynamicInterface;

        public static implicit operator MarshalByRefObject(Request req) {
            if (_dynamicInterface == null) {
                // Manually load the assembly 
                var asm = Assembly.Load("Microsoft.OneGet.Utility");
                // instantiate the dynamic interface object
                _dynamicInterface = asm.CreateInstance("Microsoft.OneGet.Core.Dynamic.DynamicInterface");
            }
            return _dynamicInterface.Create((Type)AppDomain.CurrentDomain.GetData("IRequest"), req);
        }
       

        private const string DefaultConfig = @"<?xml version=""1.0""?>
<configuration>
  <packageSources>
    <add key=""nuget.org"" value=""https://www.nuget.org/api/v2/"" />
  </packageSources>
</configuration>";

        
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
                    _configurationFileLocation = Path.Combine(GetKnownFolder("ApplicationData", this), "NuGet", "NuGet.config");
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
                return GetValue(OptionCategory.Install, "Destination");
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

        internal XDocument Config {
            get {
                try {
                    var doc = XDocument.Load(ConfigurationFileLocation);
                    if (doc.Root != null && doc.Root.Name == "configuration") {
                        return doc;
                    }
                    // doc root isn't right. make a new one!
                }
                catch {
                    // a bad xml doc.
                }
                return XDocument.Load(new MemoryStream(Encoding.UTF8.GetBytes(DefaultConfig)));
            }
            set {
                if (value == null) {
                    return;
                }

                Verbose("Saving NuGet Config {0}", ConfigurationFileLocation);

                CreateFolder(Path.GetDirectoryName(ConfigurationFileLocation), this);
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
                            Trusted = each.Attributes("trusted").Any() && each.Attribute("trusted").Value.IsTrue()
                        }, StringComparer.OrdinalIgnoreCase);
                }
                catch {
                }
                return new Dictionary<string, PackageSource> {
                    {
                        "nuget.org", new PackageSource {
                            Name = "nuget.org",
                            Location = "https://www.nuget.org/api/v2/",
                            Trusted = false,
                        }
                    }
                };
            }
        }

        internal IEnumerable<PackageSource> SelectedSources {
            get {
                var sources = (GetSpecifiedPackageSources() ?? Enumerable.Empty<string>()).ToArray();
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

                    var byLocation = pkgSources.Values.FirstOrDefault(each => each.Location == src);
                    if (byLocation != null) {
                        yield return byLocation;
                        continue;
                    }

                    // doesn't look like we have this as a source.
                    if (Uri.IsWellFormedUriString(src, UriKind.Absolute)) {
                        // we have been passed in an URI
                        var srcUri = new Uri(src);
                        if (NuGetProvider.SupportedSchemes.Contains(srcUri.Scheme.ToLower())) {
                            // it's one of our supported uri types.
                            if (SkipValidate || ValidateSourceUri(srcUri)) {
                                yield return new PackageSource {
                                    Location = srcUri.ToString(),
                                    Name = srcUri.ToString(),
                                    Trusted = false
                                };
                                continue;
                            }
                        }
                        // hmm. not a valid location?
                        Warning("URI_SCHEME_NOT_SUPPORTED", src);
                        Warning("UNKNOWN_SOURCE", src);
                        continue;
                    }

                    // is it a file path?
                    if (Directory.Exists(src)) {
                        yield return new PackageSource {
                            Location = src,
                            Name = src,
                            Trusted = true,
                        };
                    }
                    else {
                        // hmm. not a valid location?
                        Warning("UNKNOWN_SOURCE", src);
                    }
                }
            }
        }

        internal IEnumerable<IPackageRepository> Repositories {
            get {
                return SelectedSources.Select(each => PackageRepositoryFactory.Default.CreateRepository(each.Location)).ToArray();
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
            var source = config.XPathSelectElements(string.Format("/configuration/packageSources/add[@id='{0}']", id)).FirstOrDefault();
            if (source != null) {
                source.Remove();
                Config = config;
            }
        }

        internal void AddPackageSource(string name, string location, bool isTrusted) {
            if (SkipValidate || ValidateSourceLocation(location)) {
                var config = Config;
                var sources = config.XPathSelectElements("/configuration/packageSources").FirstOrDefault();
                if (sources == null) {
                    config.Root.Add(sources = new XElement("packageSources"));
                }
                var source = new XElement("add");
                source.SetAttributeValue("key", name);
                source.SetAttributeValue("value", location);
                if (isTrusted) {
                    source.SetAttributeValue("trusted", true);
                }
                sources.Add(source);
                Config = config;
            }
        }

        internal PackageSource FindRegisteredSource(string name) {
            var srcs = RegisteredPackageSources;
            if (srcs.ContainsKey(name)) {
                return srcs[name];
            }

            var src = srcs.Values.FirstOrDefault(each => each.Location == name);
            if (src != null) {
                return src;
            }

            return null;
        }

        private bool ValidateSourceLocation(string location) {
            if (Uri.IsWellFormedUriString(location, UriKind.Absolute)) {
                return ValidateSourceUri(new Uri(location));
            }
            return Directory.Exists(location);
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

            if (SkipValidate) {
                return true;
            }

            // todo: do a get on the uri and see if it responds.
            return true;
            return false;
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

        public string MakeFastPath(string source, string id, string version) {
            return string.Format(@"${0}\{1}\{2}", source.ToBase64(), id.ToBase64(), version.ToBase64());
        }

        public bool TryParseFastPath(string fastPath, out string source, out string id, out string version) {
            var match = _rxFastPath.Match(fastPath);
            source = match.Success ? match.Groups["source"].Value.FromBase64() : null;
            id = match.Success ? match.Groups["id"].Value.FromBase64() : null;
            version = match.Success ? match.Groups["version"].Value.FromBase64() : null;
            return match.Success;
        }

        internal bool YieldPackages(IEnumerable<PackageReference> packageReferences, string searchKey) {
            var foundPackage = false;

            foreach (var pkg in packageReferences) {
                foundPackage = true;
                if (!YieldPackage(pkg.FastPath, pkg.Package.Id, pkg.Package.Version.ToString(), "semver", pkg.Package.Summary, GetNameForSource(pkg.Source), searchKey)) {
                    break;
                }
            }
            return foundPackage;
        }

        internal string GetNameForSource(string source) {
            var apr = RegisteredPackageSources;

            try {
                if (File.Exists(source)) {
                    return "Local File";
                }
            }
            catch {
            }

            return apr.Keys.FirstOrDefault(key => {
                var location = apr[key].Location;
                if (location.Equals(source, StringComparison.OrdinalIgnoreCase)) {
                    return true;
                }

                // make trailing slashes consistent
                if (source.TrimEnd('/').Equals(location.TrimEnd('/'), StringComparison.OrdinalIgnoreCase)) {
                    return true;
                }

                // and trailing backslashes
                if (source.TrimEnd('\\').Equals(location.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase)) {
                    return true;
                }

                return false;
            }) ?? source;
        }

        internal IEnumerable<PackageReference> GetPackageById(string name, string requiredVersion = null, string minimumVersion = null, string maximumVersion = null) {
            if (string.IsNullOrEmpty(name)) {
                return Enumerable.Empty<PackageReference>();
            }
            return Repositories.AsParallel().SelectMany(repository => {
                try {
                    var pkgs = repository.FindPackagesById(name);

                    if (!AllVersions && (string.IsNullOrEmpty(requiredVersion) && string.IsNullOrEmpty(minimumVersion) && string.IsNullOrEmpty(maximumVersion))) {
                        pkgs = from p in pkgs where p.IsLatestVersion select p;
                    }

                    return FilterOnVersion(pkgs, requiredVersion, minimumVersion, maximumVersion)
                        .Select(pkg => new PackageReference {
                            Package = pkg,
                            Source = repository.Source,
                            FastPath = MakeFastPath(repository.Source, pkg.Id, pkg.Version.ToString())
                        });
                }
                catch (Exception e) {
                    e.Dump(this);
                    return Enumerable.Empty<PackageReference>();
                }
            });
        }

        internal IEnumerable<IPackage> FilterOnName(IEnumerable<IPackage> pkgs, string name) {
            return pkgs.Where(each => each.Id.IndexOf(name, StringComparison.OrdinalIgnoreCase) > -1);
        }

        internal PackageReference GetPackageByPath(string filePath) {
            if (PackageHelper.IsPackageFile(filePath)) {
                var package = new ZipPackage(filePath);

                return new PackageReference {
                    FastPath = MakeFastPath(filePath, package.Id, package.Version.ToString()),
                    Source = Path.GetDirectoryName(filePath),
                    Package = package,
                    IsPackageFile = true,
                };
            }
            return null;
        }

        internal string ResolveRepositoryLocation(string repository) {
            var source = FindRegisteredSource(repository);
            if (source != null) {
                return source.Location;
            }
            if (ValidateSourceLocation(repository)) {
                return repository;
            }
            return null;
        }

        internal PackageReference GetPackageByFastpath(string fastPath) {
            string source;
            string id;
            string version;
            if (TryParseFastPath(fastPath, out source, out id, out version)) {
                if (source.DirectoryHasDriveLetter() && File.Exists(source)) {
                    return GetPackageByPath(source);
                }

                var repo = PackageRepositoryFactory.Default.CreateRepository(ResolveRepositoryLocation(source));
                var pkg = repo.FindPackage(id, new SemanticVersion(version));
                if (pkg != null) {
                    return new PackageReference {
                        FastPath = fastPath,
                        Source = source,
                        Package = pkg,
                    };
                }
            }
            return null;
        }

        internal IEnumerable<PackageReference> SearchForPackages(string name, string requiredVersion, string minimumVersion, string maximumVersion) {
            return Repositories.AsParallel().SelectMany(repository => {
                var criteria = Contains;
                if (string.IsNullOrEmpty(criteria)) {
                    criteria = name;
                }
                var packages = repository.GetPackages().Find(criteria);

                // why does this method return less results? It looks the same to me!?
                // var packages = repository.Search(Hint.Is() ? Hint : name);

                IEnumerable<IPackage> pkgs = null;

                // query filtering:
                if (!AllVersions && (string.IsNullOrEmpty(requiredVersion) && string.IsNullOrEmpty(minimumVersion) && string.IsNullOrEmpty(maximumVersion))) {
                    //slow, client side way: pkgs = packages.ToEnumerable.GroupBy(p => p.Id).Select(set => set.MyMax(p => p.Version));
                    // new way: uses method in NuGet.exe in 2.8.1.1+
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
                    .Select(pkg => new PackageReference {
                        Package = pkg,
                        Source = repository.Source,
                        FastPath = MakeFastPath(repository.Source, pkg.Id, pkg.Version.ToString())
                    });
            });
        }

        public bool IsPackageInstalled(string name, string version) {
            return (from pkgFile in Directory.EnumerateFileSystemEntries(Destination, "*.nupkg", SearchOption.AllDirectories)
                    where PackageHelper.IsPackageFile(pkgFile)
                    select new ZipPackage(pkgFile))
                .Any(pkg => pkg.Id.Equals(name, StringComparison.OrdinalIgnoreCase) && pkg.Version.ToString().Equals(version, StringComparison.OrdinalIgnoreCase));
        }

        internal IEnumerable<PackageReference> GetUninstalledPackageDependencies(PackageReference packageReference) {
            foreach (var depSet in packageReference.Package.DependencySets) {
                foreach (var dep in depSet.Dependencies) {
                    // get all the packages that match this dependency
                    var depRefs = dep.VersionSpec == null ? GetPackageById(dep.Id).ToArray() : GetPackageByIdAndVersionSpec(dep.Id, dep.VersionSpec).ToArray();

                    if (depRefs.Length == 0) {
                        Error("DependencyResolutionFailure", "Unable to resolve dependent package '{0} v{1}'", dep.Id, ((object)dep.VersionSpec ?? "").ToString());
                        throw new Exception("DependencyResolutionFailure");
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

        private static string NuGetExePath {
            get {
                return typeof (AggregateRepository).Assembly.Location;
            }
        }

        public bool NuGetInstall(string source, string packageId, string version, out List<Tuple<string, string>> successful, out List<Tuple<string, string>> already, out List<Tuple<string, string>> failed) {
            var s = new List<Tuple<string, string>>();
            var a = new List<Tuple<string, string>>();
            var f = new List<Tuple<string, string>>();

            using (var p = AsyncProcess.Start(NuGetExePath, string.Format(@"install ""{0}"" -Version ""{1}"" -Source ""{2}"" -PackageSaveMode ""nuspec;nupkg""  -OutputDirectory ""{3}"" -Verbosity detailed",packageId, version, source, Destination))) {
                foreach (var l in p.StandardOutput) {
                    if (string.IsNullOrEmpty(l)) {
                        continue;
                    }
                    Verbose("NuGet", l, null);
                    // Successfully installed 'ComicRack 0.9.162'.
                    if (l.Contains("Successfully installed")) {
                        var pkg = _rxPkgParse.Match(l);
                        s.Add(new Tuple<string, string>(pkg.Groups["pkgId"].Value, pkg.Groups["ver"].Value));
                        continue;
                    }

                    if (l.Contains("already installed")) {
                        var pkg = _rxPkgParse.Match(l);
                        a.Add(new Tuple<string, string>(pkg.Groups["pkgId"].Value, pkg.Groups["ver"].Value));
                        continue;
                    }

                    if (l.Contains("not installed")) {
                        var pkg = _rxPkgParse.Match(l);
                        f.Add(new Tuple<string, string>(pkg.Groups["pkgId"].Value, pkg.Groups["ver"].Value));
                        continue;
                    }
                }

                foreach (var l in p.StandardError) {
                    if (string.IsNullOrEmpty(l)) {
                        continue;
                    }
                    Warning("NuGet", l, null);
                }

                successful = s;
                already = a;
                failed = f;

                return p.ExitCode == 0;
            }
        }

        internal IEnumerable<PackageReference> GetPackageByIdAndVersionSpec(string name, IVersionSpec versionSpec) {
            if (string.IsNullOrEmpty(name)) {
                return Enumerable.Empty<PackageReference>();
            }

            return Repositories.AsParallel().SelectMany(repository => {
                var pkgs = repository.FindPackages(name, versionSpec, AllowPrereleaseVersions, false);

                /*
                // necessary?
                pkgs = from p in pkgs where p.IsLatestVersion select p;
                */

                var pkgs2 = (IEnumerable<IPackage>)pkgs;

                return pkgs2.Select(pkg => new PackageReference {
                    Package = pkg,
                    Source = repository.Source,
                    FastPath = MakeFastPath(repository.Source, pkg.Id, pkg.Version.ToString())
                });
            }).OrderByDescending(each => each.Package.Version);
        }

        internal bool InstallSinglePackage(PackageReference packageReference) {
            List<Tuple<string, string>> success;
            List<Tuple<string, string>> already;
            List<Tuple<string, string>> failed;

            if (ShouldProcessPackageInstall(packageReference.Id, packageReference.Version, packageReference.Source)) {
                // Get NuGet to install the SoftwareIdentity

                if (NuGetInstall(packageReference.Source, packageReference.Id, packageReference.Version, out success, out already, out failed)) {
                    // NuGet Installations went ok. 
                    switch (success.Count) {
                        case 0:
                            // didn't actually install anything. that's odd.
                            if (already.Count > 0 && failed.Count == 0) {
                                // looks like it was already there?
                                Verbose("Skipped", "Package '{0} v{1}' already installed", packageReference.Id, packageReference.Version);
                                return true;
                            }
                            else {
                                Verbose("NotInstalled", "Package '{0} v{1}' failed to install", packageReference.Id, packageReference.Version);
                            }
                            return false;

                        case 1:
                            try {
                                // awesome. Just like we thought should happen
                                if (PackageInstalled(success[0].Item1, success[0].Item2, packageReference.Source ,Path.Combine(Destination, success[0].Item1))) {
                                    YieldPackage(packageReference.FastPath, packageReference.Id, packageReference.Version, "semver", packageReference.Package.Summary, GetNameForSource(packageReference.Source), packageReference.FastPath);
                                    return true;
                                }
                                else {
                                    Verbose("PostProcessPackageInstall returned false", "This is unexpected");
                                }
                            }
                            catch (Exception e) {
                                // Sad. Package had a problem.
                                // roll it back.
                                Verbose("PostProcessPackageInstall or YieldPackage threw exception", "{0}/{1} \r\n{2}", e.GetType().Name, e.Message, e.StackTrace);
                                e.Dump(this);
                            }
                            if (!ContinueOnFailure) {
                                UninstallPackage(packageReference.FastPath, true);
                            }
                            return false;

                        default:
                            // what? more than one installed. Not good. Roll em back and complain.
                            // uninstall package.
                            Error("SomethingBad", "Package '{0} v{1}' installed more than one package, and this was unexpected", packageReference.Id, packageReference.Version);
                            break;
                    }
                }
            }
            else {
                return WhatIf();
            }

            return false;
        }

        private void UninstallPackage(string fastPath, bool isRollingBack) {

        }
    }

    internal class PackageReference {
        internal IPackage Package { get; set; }
        internal string Source { get; set; }
        internal string FastPath { get; set; }

        internal bool IsPackageFile { get; set; }

        internal string Id {
            get {
                return Package.Id;
            }
        }

        internal string Version {
            get {
                return Package.Version.ToString();
            }
        }
    }
}