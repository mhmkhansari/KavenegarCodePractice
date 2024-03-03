using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Shared.Framework.Application
{
    public sealed class ValidationException : BadRequestException
    {
        public ValidationException(Dictionary<string, string[]> errors)
            : base("Validation errors occurred") =>
            Errors = errors;

        public Dictionary<string, string[]> Errors { get; }
    }
}
