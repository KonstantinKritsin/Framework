using System;
using System.Runtime.Serialization;

// ReSharper disable UnusedMember.Global

namespace Project.Framework.Common.Exceptions
{
	[Serializable]
	public class ParentContextIsNullException : Exception
	{
		public ParentContextIsNullException()
		{
		}

		public ParentContextIsNullException(string message) : base(message)
		{
		}

		public ParentContextIsNullException(string message, Exception inner) : base(message, inner)
		{
		}

		protected ParentContextIsNullException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}