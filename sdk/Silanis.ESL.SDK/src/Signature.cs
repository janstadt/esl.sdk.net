using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
	public class Signature
	{
		private List<Field> fields = new List<Field>();

		public Signature (string signerEmail, int page, double x, double y)
		{
			SignerEmail = signerEmail;
            GroupId = null;
			Page = page;
			X = x;
			Y = y;
		}

        public Signature( GroupId groupId, int page, double x, double y)
        {
            SignerEmail = null;
            GroupId = groupId;
            Page = page;
            X = x;
            Y = y;
        }

        public GroupId GroupId
        {
            get;
            private set;
        }

		public string SignerEmail 
		{
			get;
			private set;
		}

		public int Page 
		{
			get;
			private set;
		}

		public double X 
		{
			get;
			private set;
		}

		public double Y
		{
			get;
			private set;
		}

		public double Height {
			get;
			set;
		}

		public double Width {
			get;
			set;
		}

		public SignatureStyle Style {
			get;
			set;
		}

		public void AddFields (IList<Field> fields)
		{
			this.fields.AddRange (fields);
		}

		public List<Field> Fields
		{
			get
			{
				return fields;
			}
		}

		public string Name {
			get;
			set;
		}

		public bool Extract {
			get;
			set;
		}

        public TextAnchor TextAnchor
        {
            get;
            set;
        }

		public bool IsGroupSignature()
		{
			return GroupId != null;
		}
	}
}