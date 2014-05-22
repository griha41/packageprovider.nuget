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
    using Utility;
    using Callback = System.Func<string, System.Collections.Generic.IEnumerable<object>, object>;

    public class NuGetProvider {
        #region implement PackageProvider-interface
/// <summary>
            /// Returns the name of the Provider. Doesn't need a callback .
            /// </summary>
            /// <required/>
            /// <returns>the name of the package provider</returns>
        public string GetPackageProviderName(){
            return "NuGet";
        }

        public void InitializeProvider(Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'InitializeProvider'" );
            }

        }
        public void GetFeatures(Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'GetFeatures'" );
            }

        }
        public void GetDynamicOptions(int category, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'GetDynamicOptions'" );
            }

        }

        // --- Optimization features -----------------------------------------------------------------------------------------------------
        public IEnumerable<string> GetMagicSignatures(){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.

            return  default(IEnumerable<string>);
        }
        public IEnumerable<string> GetSchemes(){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.

            return  default(IEnumerable<string>);
        }
        public IEnumerable<string> GetFileExtensions(){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.

            return  default(IEnumerable<string>);
        }
        public bool GetIsSourceRequired(){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.

            return  default(bool);
        }

        // or should we imply this from the GetPackageSources() == null/empty?

            // --- Manages package sources ---------------------------------------------------------------------------------------------------
        public void AddPackageSource(string name, string location, bool trusted, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'AddPackageSource'" );
            }

        }
        public bool GetPackageSources(Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'GetPackageSources'" );
            }

            return  default(bool);
        }
        public void RemovePackageSource(string name, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'RemovePackageSource'" );
            }

        }

        // --- Finds packages ---------------------------------------------------------------------------------------------------
            /// <summary>
            /// 
            /// 
            /// Notes:
            /// 
            ///  - If a call to GetPackageSources() on this object returns no sources, the cmdlet won't call FindPackage on this source
            ///  - (ie, the expectation is that you have to provide a source in order to use find package)
            /// </summary>
            /// <param name="name"></param>
            /// <param name="requiredVersion"></param>
            /// <param name="minimumVersion"></param>
            /// <param name="maximumVersion"></param>
            /// <param name="c"></param>
            /// <returns></returns>
        public bool FindPackage(string name, string requiredVersion, string minimumVersion, string maximumVersion, int id, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'FindPackage'" );
            }

            return  default(bool);
        }
        public bool FindPackageByFile(string file, int id, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'FindPackageByFile'" );
            }

            return  default(bool);
        }
        public bool FindPackageByUri(Uri uri, int id, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'FindPackageByUri'" );
            }

            return  default(bool);
        }
        public bool GetInstalledPackages(string name, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'GetInstalledPackages'" );
            }

            return  default(bool);
        }

        // --- operations on a package ---------------------------------------------------------------------------------------------------
        public bool DownloadPackage(string fastPath, string location, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'DownloadPackage'" );
            }

            return  default(bool);
        }
        public bool GetPackageDependencies(string fastPath, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'GetPackageDependencies'" );
            }

            return  default(bool);
        }
        public bool GetPackageDetails(string fastPath, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'GetPackageDetails'" );
            }

            return  default(bool);
        }
        public bool InstallPackage(string fastPath, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'InstallPackage'" );
            }

            return  default(bool);
        }

        // auto-install-dependencies
                // skip-dependency-check
                // continue-on-failure
                // location system/user/folder
                // callback for each package installed when installing dependencies?
        public bool UninstallPackage(string fastPath, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'UninstallPackage'" );
            }

            return  default(bool);
        }
        public int StartFind(Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'StartFind'" );
            }

            return  default(int);
        }
        public bool CompleteFind(int id, Callback c){
             // TODO: Fill in implementation
             // Delete this method if you do not need to implement it
             // Please don't throw an not implemented exception, it's not optimal.
            using (var request = new Request(c)) {
                // use the request object to interact with the OneGet core:
                request.Debug("Information","Calling 'CompleteFind'" );
            }

            return  default(bool);
        }

        #endregion

    }
}