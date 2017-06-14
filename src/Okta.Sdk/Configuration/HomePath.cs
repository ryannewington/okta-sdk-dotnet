// <copyright file="HomePath.cs" company="Okta, Inc">
// Copyright (c) 2014-2017 Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Okta.Sdk.Configuration
{
    /// <summary>
    /// Contains methods for resolving the home directory path.
    /// </summary>
    public static class HomePath
    {
        /// <summary>
        /// Resolves the current user's home directory path.
        /// </summary>
        /// <returns>The home path.</returns>
        public static string GetPath()
        {
#if NET45
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
#else
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Environment.GetEnvironmentVariable("USERPROFILE") ??
                    Path.Combine(Environment.GetEnvironmentVariable("HOMEDRIVE"), Environment.GetEnvironmentVariable("HOMEPATH"));
            }

            var home = Environment.GetEnvironmentVariable("HOME");

            if (string.IsNullOrEmpty(home))
            {
                throw new Exception("Home directory not found. The HOME environment variable is not set.");
            }

            return home;
#endif
        }
    }
}
