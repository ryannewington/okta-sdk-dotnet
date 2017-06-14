// <copyright file="ConfigurationValidator.cs" company="Okta, Inc">
// Copyright (c) 2014-2017 Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Sdk.Configuration
{
    public static class ConfigurationValidator
    {
#pragma warning disable SA1503 // Braces must not be omitted
        public static void Validate(ClientConfiguration configuration)
        {

            if (string.IsNullOrEmpty(configuration.OrgUrl)) throw new ArgumentNullException(nameof(configuration.OrgUrl), "Copy your Okta Organization URL (like https://dev-123456.oktapreview.com) and pass it to the SDK client.");
            if (string.IsNullOrEmpty(configuration.Token)) throw new ArgumentNullException(nameof(configuration.Token), "Generate an API token in the Okta developer dashboard and pass it to the SDK client.");
        }
#pragma warning restore SA1503 // Braces must not be omitted
    }
}
