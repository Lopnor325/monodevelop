// 
// IPhoneSoftDebuggerEngine.cs
//  
// Author:
//       Michael Hutchinson <mhutchinson@novell.com>
// 
// Copyright (c) 2009 Novell, Inc. (http://www.novell.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.IO;
using MonoDevelop.Debugger;
using MonoDevelop.Core;
using MonoDevelop.Core.Execution;
using MonoDevelop.IPhone;
using Mono.Debugging.Client;
using MonoDevelop.Debugger.Soft;
using System.Net;

namespace MonoDevelop.Debugger.Soft.IPhone
{
	public class IPhoneSoftDebuggerEngine: IDebuggerEngine
	{
		
		public bool CanDebugCommand (ExecutionCommand command)
		{
			var cmd = command as IPhoneExecutionCommand;
			return cmd != null && cmd.DebugMode;
		}
		
		public DebuggerStartInfo CreateDebuggerStartInfo (ExecutionCommand command)
		{
			var cmd = (IPhoneExecutionCommand) command;
			var startInfo = new IPhoneDebuggerStartInfo (new IPEndPoint (IPAddress.Any, 10000), cmd);
			return startInfo;
		}

		public DebuggerFeatures SupportedFeatures {
			get {
				return DebuggerFeatures.Breakpoints | 
					   DebuggerFeatures.Pause | 
					   DebuggerFeatures.Stepping | 
					   DebuggerFeatures.DebugFile |
					   DebuggerFeatures.Catchpoints;
			}
		}
		
		public DebuggerSession CreateSession ()
		{
			return new IPhoneDebuggerSession ();
		}
		
		public ProcessInfo[] GetAttachableProcesses ()
		{
			return new ProcessInfo[0];
		}
		
		public string Name {
			get {
				return "Mono Soft Debugger";
			}
		}
	}
	
	class IPhoneDebuggerStartInfo : DebuggerStartInfo
	{
		public IPEndPoint Endpoint { get; private set; }
		public IPhoneExecutionCommand ExecutionCommand {  get; private set; }
		
		public IPhoneDebuggerStartInfo (IPEndPoint endpoint, IPhoneExecutionCommand command)
		{
			this.Endpoint = endpoint;
			this.ExecutionCommand = command;
		}		
	}
}
