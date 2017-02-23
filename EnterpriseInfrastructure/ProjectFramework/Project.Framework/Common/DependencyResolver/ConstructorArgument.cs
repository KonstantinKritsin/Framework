﻿
namespace Project.Framework.Common.DependencyResolver
{
    public class ConstructorArgument
	{
		public ConstructorArgument(string name, object value)
		{
			Name = name;
			Value = value;
		}

		public string Name { get; set; }
		public object Value { get; set; }
	}
}
