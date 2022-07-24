using System.ComponentModel.DataAnnotations;
using System.ServiceModel;

namespace be.axa.customer.reglogext.infrastructure.Soap;

public class GeneralBasicHttpBindingSettings
{
    [Required] public BasicHttpBinding? HttpBinding { get; set; }
}