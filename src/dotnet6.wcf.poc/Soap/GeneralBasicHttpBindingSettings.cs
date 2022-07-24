using System.ComponentModel.DataAnnotations;
using System.ServiceModel;

namespace dotnet6.wcf.poc.Soap;

public class GeneralBasicHttpBindingSettings
{
    [Required] public BasicHttpBinding? HttpBinding { get; set; }
}