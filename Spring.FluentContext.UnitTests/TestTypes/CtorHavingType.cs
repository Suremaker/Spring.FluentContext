//
//  Author:
//    Wojciech Kotlarski wojciech.kotlarski@gmail.com
//
//  Copyright (c) 2012, Wojciech Kotlarski
//
//  All rights reserved.
//
//  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.	 
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
//       the documentation and/or other materials provided with the distribution.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
//  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT 
//  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
//  FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
//  CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, 
//  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
//  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF 
//  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
//  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
//  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
namespace Spring.FluentContext.UnitTests.TestTypes
{
	class CtorHavingType
	{
		public NestingType Nesting { get; private set; }

		public string Text { get; private set; }

		public int Value { get; private set; }

		public double OtherValue { get; private set; }

		public CtorHavingType(string text) : this(null, text, 0)
		{
		}

		public CtorHavingType(string text, int value) : this(null,text,value)
		{
		}

		public CtorHavingType(NestingType nesting) : this(nesting,null,0)
		{
		}

		public CtorHavingType(NestingType nesting, string text, int value) : this(nesting, text, value, 0.0)
		{
		}

		public CtorHavingType(NestingType nesting, string text, int value, double otherValue)
		{
			Nesting = nesting;
			Text = text;
			Value = value;
			OtherValue = otherValue;
		}
	}
}