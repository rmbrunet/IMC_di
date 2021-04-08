# IMC_di Tax Calculator Sample

## Description

Sales Tax Calculator Web Service using the [TaxJar REST API](https://developers.taxjar.com/api/reference).

## Running Prototype

The sample is running at https://imc-rmb.azurewebsites.net
using a Basic Azure AppService Dev/Test B1 instance (1.75 GB memory and 100 ACU).

For <u>demo</u> purposes the Swagger endpoint is exposed at:

https://imc-rmb.azurewebsites.net/swagger/index.html

The following json body can be used for testing the Order tax endpoint:

```
{
  "id": "1",
  "customerId": "1",
  "addressTo": {
    "countryCode": "US",
    "zip": "90002",
    "stateCode": "CA",
    "city": "Los Angeles",
    "street": "1335 E 103rd St"
  },
  "addressFrom": {
    "countryCode": "US",
    "zip": "92093",
    "stateCode": "CA",
    "city": "La Jolla",
    "street": "9500 Gilman Drive"
  },
  "shipping": 1.5,
  "lineItems": [
    {
      "id": "abc",
      "quantity": 1,
      "productTaxCode": "20010",
      "unitPrice": 15,
      "discount": 0
    }
  ]
}
```

To run the project locally is necessary to create a user's secret file and add two settings: TaxJarUrl and TaxJarApiKey. In Azure deployment these values are in the AppService configuration but a KeyVault would be more appropriate.

## Project Structure

Only the minimal set of Entities as required by the TaxJar REST Api where created. The IMC Domain Entities are totally independent from the TaxJar API opening the possibility of having other Tax Calculators in the future. 

## Building & Testing

There is a simple build.cake [Cakebuild](https://cakebuild.net/) file included in the root folder. This uses the Cake.Tool locally to drive the execution of the build, testing, and publishing of artifacts via the command line
:    

``` text
dotnet cake
```

After cloning the repository (that includes the tools manifest file) Cake.Tool should be restored using:

```
dotnet tool restore
```

Cakebuild could eventually be used also in a CI build server so that the build runs the same everywhere. 

A IMC.rest file is also included in the solution root. This file is intended to work with the [VS Code REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) extension and provides additional testing capabilities.

## Validations

For demonstration purposes some rules have been imposed on the TaxJar entities following the documentation. Analogously, some rules have been applied to the Order entity in the IMC.Domain just as an example. [FluentValidation](https://www.nuget.org/packages/FluentValidation/) was used to create some relatively complex rules. For example the TaxJar requirement that the State Code is required for USA and Canada:

```
RuleFor(o => o.ToState)
    .NotEmpty()
    .When(o => (o.ToCountry == "US") || (o.ToCountry == "CA"))
    .WithMessage("State Code is mandatory for US and Canada.");
```


