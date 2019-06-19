# IBAN validation

Code and infrastructure to validate IBAN.

## Building

ensure .NET Core SDK installed. Go to solution dir, run:

```cmd
dotnet build
```

## Web service

1. In order to run Web service go to Iban.Validation.WebService and run

```cmd
dotnet run
```

configuration is available in ./Iban.Validation.WebService/Properties/launchSettings.json

2. use URL:

```http
http://host:port/api/iban/validate/{iban}
```

or CURL:

```cmd
curl http://localhost:5000/api/iban/validate/GB82WEST12345698765432
```

Return is always valid JSON object. E.g.:

```json
{"success":true,"message":null}
```

or

```json
{"success":false,"iban":"GB82WEST1234569876543","message":"IBAN string has incorrect length"}
```

There is also ability of batch validation:

Use post endpoint:

```http
http:/host/api/iban/validate/
```

pass JSON object in the body:

```json
{ibans:["MT31MALT01100000000000000000123","GB82WEST1234569876543"]}
```

return will be array of valid JSON objects:

```json
[
    {
        "success": true,
        "iban": "MT31MALT01100000000000000000123",
        "message": null
    },
    {
        "success": false,
        "iban": "GB82WEST1234569876543",
        "message": "IBAN string has incorrect length"
    }
]
```

try CURL:

```cmd

curl --location --request POST "http://localhost:5000/api/iban/validate/" --header "Content-Type: application/json"   --data "{ibans:[\"MT31MALT01100000000000000000123\",\"GB82WEST1234569876543\"]}"

```

## Tests

One can run unit tests from './Tests' dir:

```cmd

dotnet test

```