//
//  Author:
//    Wojciech Kotlarski
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

using System;
using Spring.FluentContext.Builders;
using Spring.FluentContext.BuildingStages.Objects;
using Spring.FluentContext.Definitions;
using Spring.FluentContext.Utils;
using Spring.Objects;
using Spring.Objects.Factory.Config;

namespace Spring.FluentContext.Impl
{
	internal class PropertyDefinitionBuilder<TObject, TProperty> : IPropertyDefinitionBuilder<TObject, TProperty>
	{
		private readonly ObjectDefinitionBuilder<TObject> _builder;
		private readonly string _propertyName;

		public PropertyDefinitionBuilder(ObjectDefinitionBuilder<TObject> builder, string propertyName)
		{
			_builder = builder;
			_propertyName = propertyName;
		}

		public IObjectConfigurationBuildStage<TObject> ToValue(TProperty value)
		{
			return AddPropertyValue(new PropertyValue(_propertyName, value));
		}

		public IObjectConfigurationBuildStage<TObject> ToRegistered(string objectId)
		{
			return AddPropertyValue(new PropertyValue(_propertyName, new RuntimeObjectReference(objectId)));
		}

		public IObjectConfigurationBuildStage<TObject> ToRegistered(IObjectRef<TProperty> reference)
		{
			return ToRegistered(reference.Id);
		}

		public IObjectConfigurationBuildStage<TObject> ToRegisteredDefaultOf<TReferencedType>() where TReferencedType : TProperty
		{
			return ToRegistered(IdGenerator<TReferencedType>.GetDefaultId());
		}

		public IObjectConfigurationBuildStage<TObject> ToRegisteredDefault()
		{
			return ToRegistered(IdGenerator<TProperty>.GetDefaultId());
		}

		public IObjectConfigurationBuildStage<TObject> To(IDefinition<TProperty> definition)
		{
			return AddPropertyValue(new PropertyValue(_propertyName, definition.DefinitionObject));
		}

		public IObjectConfigurationBuildStage<TObject> ToInlineDefinition<TInnerObject>() where TInnerObject : TProperty
		{
			return AddPropertyValue(new PropertyValue(_propertyName, new ObjectDefinitionBuilder<TInnerObject>(null).Definition));
		}

		public IObjectConfigurationBuildStage<TObject> ToInlineDefinition<TInnerObject>(Action<IInstantiationBuildStage<TInnerObject>> innerObjectBuildAction) where TInnerObject : TProperty
		{
			var builder = new ObjectDefinitionBuilder<TInnerObject>(null);
			innerObjectBuildAction(builder);
			return AddPropertyValue(new PropertyValue(_propertyName, builder.Definition));
		}

		private IObjectConfigurationBuildStage<TObject> AddPropertyValue(PropertyValue propertyValue)
		{
			_builder.Definition.PropertyValues.Add(propertyValue);
			return _builder;
		}
	}
}