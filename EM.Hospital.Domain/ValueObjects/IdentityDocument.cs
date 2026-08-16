using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Domain.ValueObjects
{
    public class IdentityDocument
    {
        public string Type { get; private set; }
        public string Document { get; private set; }
        public IdentityDocument()
        {
            
        }
        private IdentityDocument(string type, string document)
        {
            Type = type;
            Document = document;
        }
        public static Result<IdentityDocument> Create(string type, string document)
        {
            if (string.IsNullOrEmpty(type))
                return Result.Failure<IdentityDocument>("Identity document type is required");
            if (string.IsNullOrEmpty(document))
                return Result.Failure<IdentityDocument>("Identity document number is required");
            return Result.Success(new IdentityDocument(type, document));
        }
    }
}
