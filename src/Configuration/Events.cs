﻿// The MIT License (MIT)
// 
// Copyright (c) 2015 Microsoft
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace MetricSystem.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Tracing;
    using System.Net;

    [EventSource(Name = "MetricSystem-Configuration", Guid = "623e2915-f18a-438f-b0d6-2969c8fbc877")]
    public sealed class Events : EventSource
    {
        public static readonly Events Write = new Events();

        private static string FormatSources(IEnumerable<ConfigurationSource> sources)
        {
            return sources != null ? string.Join(", ", sources) : string.Empty;
        }

        [NonEvent]
        internal void Info(IEnumerable<ConfigurationSource> sources, string message)
        {
            this.Info(FormatSources(sources), message);
        }

        [Event(1, Level = EventLevel.Informational)]
        public void Info(string source, string message)
        {
            if (this.IsEnabled())
            {
                this.WriteEvent(1, source, message);
            }
        }

        [NonEvent]
        internal void Warning(IEnumerable<ConfigurationSource> sources, string message)
        {
            this.Warning(FormatSources(sources), message);
        }

        [Event(2, Level = EventLevel.Warning)]
        public void Warning(string source, string message)
        {
            if (this.IsEnabled())
            {
                this.WriteEvent(2, source, message);
            }
        }

        [NonEvent]
        internal void Error(IEnumerable<ConfigurationSource> sources, string message)
        {
            this.Error(FormatSources(sources), message);
        }

        [Event(3, Level = EventLevel.Error)]
        public void Error(string source, string message)
        {
            if (this.IsEnabled())
            {
                this.WriteEvent(3, source, message);
            }
        }

        [NonEvent]
        public void BeginUpdateHttpConfigurationContent(Uri source)
        {
            this.BeginUpdateHttpConfigurationContent(source.ToString());
        }

        [Event(10, Level = EventLevel.Verbose)]
        private void BeginUpdateHttpConfigurationContent(string source)
        {
            if (this.IsEnabled(EventLevel.Verbose, EventKeywords.None))
            {
                this.WriteEvent(10, source);
            }
        }

        [NonEvent]
        public void EndUpdateHttpConfigurationContent(Uri source, bool updated)
        {
            this.EndUpdateHttpConfigurationContent(source.ToString(), updated);
        }

        [Event(11, Level = EventLevel.Verbose)]
        public void EndUpdateHttpConfigurationContent(string source, bool updated)
        {
            if (this.IsEnabled(EventLevel.Verbose, EventKeywords.None))
            {
                this.EndUpdateHttpConfigurationContent(source, updated);
            }
        }

        [NonEvent]
        public void UpdateHttpConfigurationRequestFailed(Uri source, HttpStatusCode statusCode, string reasonPhrase)
        {
            this.UpdateHttpConfigurationRequestFailed(source.ToString(), (int)statusCode, reasonPhrase);
        }

        [Event(12, Level = EventLevel.Warning)]
        private void UpdateHttpConfigurationRequestFailed(string source, int statusCode, string reasonPhrase)
        {
            if (this.IsEnabled())
            {
                this.WriteEvent(12, source, statusCode, reasonPhrase);
            }
        }

        [NonEvent]
        public void HttpExceptionFromSource(Uri source, Exception ex)
        {
            this.HttpExceptionFromSource(source.ToString(), ex.GetType().ToString(), ex.Message);
        }

        [Event(20, Level = EventLevel.Warning)]
        private void HttpExceptionFromSource(string source, string exceptionType, string exceptionMessage)
        {
            if (this.IsEnabled())
            {
                this.WriteEvent(20, source, exceptionType, exceptionMessage);
            }
        }
    }
}
