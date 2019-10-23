using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.ErrorHandling
{
    public interface IErrorFactory
    {
        void Invalid(string message);
    }
}
