using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class ForbiddenRequestException : ApplicationException
    {
        public ForbiddenRequestException(string message) : base(message)
        {

        }
    }
}