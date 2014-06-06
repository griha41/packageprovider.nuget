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
    using global::NuGet;
    using Callback = System.Object;

    public class NuGetProvider {
        private static readonly string[] _empty = new string[0];

        private static readonly Dictionary<string,string[]> _features = new Dictionary<string, string[]> {
            { "supports-powershell-get", _empty },
            { "schemes", new [] {"http", "https", "file"} },
            { "extensions", new [] {"nupkg"} },
            { "magic-signatures", _empty },
        };

        internal static IEnumerable<string> SupportedSchemes {
            get {
                return _features["schemes"];
            }
        }
        /// <summary>
        ///     Returns the name of the Provider. Doesn't need a callback .
        /// </summary>
        /// <required />
        /// <returns>the name of the package provider</returns>
        public string GetPackageProviderName() {
            return "NuGet";
        }

        public void InitializeProvider(object dynamicInterface, Callback c) {
            DynamicExtensions.DynamicInterface = dynamicInterface;
        }

        public void GetFeatures(Callback c) {
            using (var request = c.As<Request>()) {
                request.Debug("Calling 'NuGet::GetFeatures'");
                foreach (var feature in _features) {
                    request.Yield(feature);
                }
            }
        }

        public void GetDynamicOptions(int category, Callback c) {
            using (var request = c.As<Request>()) {
                request.Debug("Calling 'NuGet::GetDynamicOptions'");
                try {
                    var cat = (OptionCategory)category;

                    switch (cat) {
                        case OptionCategory.Package:
                            request.YieldDynamicOption(cat, "Tag", OptionType.StringArray, false);
                            request.YieldDynamicOption(cat, "Contains", OptionType.String, false);
                            request.YieldDynamicOption(cat, "AllowPrereleaseVersions", OptionType.Switch, false);
                            request.YieldDynamicOption(cat, "AllVersions", OptionType.Switch, false);
                            break;

                        case OptionCategory.Source:
                            request.YieldDynamicOption(cat, "ConfigFile", OptionType.String, false);
                            request.YieldDynamicOption(cat, "SkipValidate", OptionType.Switch, false);
                            break;

                        case OptionCategory.Install:
                            request.YieldDynamicOption(cat, "Destination", OptionType.Path, true);
                            request.YieldDynamicOption(cat, "SkipDependencies", OptionType.Switch, false);
                            request.YieldDynamicOption(cat, "ContinueOnFailure", OptionType.Switch, false);
                            break;
                    }
                } catch (Exception e) {
                    // this makes it ignore new OptionCategories that it doesn't know about.
                }
            }
        }


        // --- Manages package sources ---------------------------------------------------------------------------------------------------
        public void AddPackageSource(string name, string location, bool trusted, Callback c) {
            using (var request = c.As<Request>()) {
                request.Debug("Calling 'NuGet::AddPackageSource'");
                var src = request.FindRegisteredSource(name);
                if (src != null) {
                    request.RemovePackageSource(src.Name);
                }
                request.AddPackageSource(name, location, trusted);
            }
        }

        public void GetPackageSources(Callback c) {
            using (var request = c.As<Request>()) {
                request.Debug("Calling 'NuGet::GetPackageSources'");

                foreach (var source in request.SelectedSources) {
                    request.YieldPackageSource(source.Name, source.Location, source.Trusted);
                }
            }
        }

        public void RemovePackageSource(string name, Callback c) {
            using (var request = c.As<Request>()) {
                request.Debug("Calling 'NuGet::RemovePackageSource'");
                var src = request.FindRegisteredSource(name);
                if (src == null) {
                    request.Warning("UNKNOWN_SOURCE", name);
                    return;
                }

                request.RemovePackageSource(src.Name);
                request.YieldPackageSource(src.Name, src.Location, src.Trusted);
            }
        }

        // --- Finds packages ---------------------------------------------------------------------------------------------------
        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="requiredVersion"></param>
        /// <param name="minimumVersion"></param>
        /// <param name="maximumVersion"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public void FindPackage(string name, string requiredVersion, string minimumVersion, string maximumVersion, int id, Callback c) {
            using (var request = c.As<Request>()) {
                request.Debug("Calling 'NuGet::FindPackage'");

                // get the package by ID first.
                // if there are any packages, yield and return
                if (request.YieldPackages(request.GetPackageById(name, requiredVersion, minimumVersion, maximumVersion), name)) {
                    return;
                }

                // have we been cancelled?
                if (request.IsCancelled()) {
                    return;
                }

                // Try searching for matches and returning those.
                request.YieldPackages(request.SearchForPackages(name, requiredVersion, minimumVersion, maximumVersion), name);
            }
        }

        public void FindPackageByFile(string filePath, int id, Callback c) {
            using (var request = c.As<Request>()) {
                request.Debug("Calling 'NuGet::FindPackageByFile'");
                filePath = Path.GetFullPath(filePath);
                if (File.Exists(filePath)) {
                    if (PackageHelper.IsPackageFile(filePath)) {
                        var pkg = new ZipPackage(filePath);
                        var fastPath = request.MakeFastPath(filePath, pkg.Id, pkg.Version.ToString());
                        request.YieldPackage(fastPath, pkg.Id, pkg.Version.ToString(), "semver", "", filePath, filePath);
                    }
                }
            }
        }

        public void FindPackageByUri(Uri uri, int id, Callback c) {
            using (var request = c.As<Request>()) {
                request.Debug("Calling 'NuGet::FindPackageByUri'");

                // check if this URI is a valid source
                // if it is, get the list of packages from this source

                // otherwise, download the Uri and see if it's a package 
                // that we support.
            }
        }

        public void GetInstalledPackages(string name, Callback c) {
            using (var request = c.As<Request>()) {
                request.Debug("Calling 'NuGet::GetInstalledPackages'");
            }
        }

        // --- operations on a package ---------------------------------------------------------------------------------------------------
        public void DownloadPackage(string fastPath, string location, Callback c) {
            using (var request = c.As<Request>()) {
                request.Debug("Calling 'NuGet::DownloadPackage'");
            }
        }

        public void GetPackageDependencies(string fastPath, Callback c) {
            using (var request = c.As<Request>()) {
                request.Debug("Calling 'NuGet::GetPackageDependencies'");
            }
        }

        public void GetPackageDetails(string fastPath, Callback c) {
            using (var request = c.As<Request>()) {
                request.Debug("Calling 'NuGet::GetPackageDetails'");
            }
        }

        public void InstallPackage(string fastPath, Callback c) {
            using (var request = c.As<Request>()) {
                request.Debug("Calling 'NuGet::InstallPackage'");

                var pkgRef = request.GetPackageByFastpath(fastPath);
                if (pkgRef == null) {
                    request.Error("Unable to resolve package reference");
                }

                var dependencies = request.GetUninstalledPackageDependencies(pkgRef).Reverse().ToArray();

                var n = 0;
                foreach (var d in dependencies) {
                    if (!request.InstallSinglePackage(d)) {
                        request.Error("InstallFailure", "Dependent Package '{0} {1}' not installed", d.Id, d.Version);
                        return;
                    }
                }

                // got this far, let's install the package we came here for.
                if (!request.InstallSinglePackage(pkgRef)) {
                    // package itself didn't install.
                    // roll that back out everything we did install.
                    // and get out of here.
                    request.Error("InstallFailure", "Package '{0}' not installed", pkgRef.Id);
                    
                }
            }
        }

        // callback for each package installed when installing dependencies?

        public void UninstallPackage(string fastPath, Callback c) {
            using (var request = c.As<Request>()) {
                request.Debug("Calling 'NuGet::UninstallPackage'");
            }
        }
    }
}