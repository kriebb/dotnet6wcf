# wcf

## install

https://docs.microsoft.com/en-us/dotnet/core/additional-tools/dotnet-svcutil-guide?tabs=dotnetsvcutil2x

````dotnet tool install --global dotnet-svcutil````

````dotnet svcutil````
````text
It was not possible to find any compatible framework version
  - The following frameworks were found:
      3.1.23 at [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
      6.0.3 at [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
````

https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-5.0.17-windows-x64-installer

## Usage

Usage of a java wsdl: https://www.ebi.ac.uk/europepmc/webservices/soap?wsdl

````dotnet svcutil https://www.ebi.ac.uk/europepmc/webservices/soap?wsdl````


dotnet svcutil https://ec.europa.eu/taxation_customs/tin/services/checkTinService.wsdl
## Coding

reverse engineer: https://europepmc.org/doc/WSCitationImplPortClient.java
https://europepmc.org/JaxWs

Go to SoapServiceClient.cs and the ContractBehavior `UseNamespacesPrefixes` to the `actory.Endpoint.Contract.ContractBehaviors` 

```csharp
    private TService CreateServiceChannel<TService>(string url)
    {
...
        factory.Endpoint.Contract.ContractBehaviors.Add(new UseNamespacesPrefixes());
...
        return factory.CreateChannel();
    }
```

## Consume the service

### Example empty namspace

```xml
<?xml version="1.0" encoding="UTF-8"?>
<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/">
   <s:Body xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
      <searchPublications xmlns="http://webservice.cdb.ebi.ac.uk/">
         <queryString xmlns="">OPEN_ACCESS:y HAS_UNIPROT:y HAS_REFLIST:y HAS_XREFS:y sort_cited</queryString>
         <resultType xmlns="">lite</resultType>
         <cursorMark xmlns="">*</cursorMark>
         <synonym xmlns="">false</synonym>
         <email xmlns="">testclient@ebi.ac.uk</email>
      </searchPublications>
   </s:Body>
</s:Envelope>
```
### After removal is applied

```xml
<?xml version="1.0" encoding="UTF-8"?>
<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/">
   <s:Body>
      <webservice.cdb.ebi.ac.uk:searchPublications xmlns:webservice.cdb.ebi.ac.uk="http://webservice.cdb.ebi.ac.uk/">
         <queryString>OPEN_ACCESS:y HAS_UNIPROT:y HAS_REFLIST:y HAS_XREFS:y sort_cited</queryString>
         <resultType>lite</resultType>
         <cursorMark>*</cursorMark>
         <synonym>false</synonym>
         <email>testclient@ebi.ac.uk</email>
      </webservice.cdb.ebi.ac.uk:searchPublications>
   </s:Body>
</s:Envelope>
```


### Without prefix

```<?xml version="1.0" encoding="UTF-8"?>
<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/">
   <s:Body xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
      <checkTin xmlns="urn:ec.europa.eu:taxud:tin:services:checkTin:types">
         <countryCode>BE</countryCode>
         <tinNumber>71.09.07–213–64</tinNumber>
      </checkTin>
   </s:Body>
</s:Envelope>
```

### With prefix

```xml
<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/">
   <s:Body>
      <unjal:checkTin xmlns:unjal="urn:ec.europa.eu:taxud:tin:services:checkTin:types">
         <unjal:countryCode>BE</unjal:countryCode>
         <unjal:tinNumber>71.09.07–213–64</unjal:tinNumber>
      </unjal:checkTin>
   </s:Body>
</s:Envelope>
```

```