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
    using System.IO;
    using global::NuGet;

    internal class PackageItem {
        internal IPackage Package {get; set;}
        // internal string Source {get; set;}

        internal PackageSource PackageSource {get; set;}

        internal string FastPath {get; set;}

        internal bool IsPackageFile {get; set;}

        private string _id;
        internal string Id {
            get {
                if (_id == null) {
                    return Package.Id;    
                }
                return _id;
            }
            set {
                _id = value;
            }
        }

        private string _version;
        internal string Version {
            get {
                if (_version == null) {
                    return Package.Version.ToString();    
                }
                return _version;
            }
            set {
                _version = value;
            }
        }

        internal string FullName {
            get {
                if (Package != null) {
                    return Package.GetFullName();
                }

                return string.Format("{0}.{1}", Id, Version);
            }
        }

        internal string PackageFilename {
            get {
                if (IsPackageFile) {
                    return Path.GetFileName(PackageSource.Location);
                }

                return Id + "." + Version+ ".nupkg";
            }
        }

        private string _fullPath;
        internal string FullPath {
            get {
                if (IsPackageFile) {
                    return Path.GetFileName(PackageSource.Location);
                }
                return _fullPath;
            }
            set {
                _fullPath = value;
            }
        }

        private string _canonicalId;
        internal string GetCanonicalId(Request request) {
            return _canonicalId ?? (_canonicalId = request.GetCanonicalPackageId(Constants.ProviderName, Id, Version));
        }
    }
}