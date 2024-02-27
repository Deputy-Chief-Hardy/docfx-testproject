# MasterData.APIClient

This project provides an auto-generated client which can call the MasterData.API.

Use NSwagStudio to re-generate the client whenever MasterData.API is changed.
The configuration is stored in `MasterDataClient.nswag`.

## Considerations

Microsoft Kiota seems to be a promising platform to generate REST clients, 
instead of NSwag. However, their interpretation of non-nullable objects as nullable
is debated by many developers (https://github.com/microsoft/kiota/issues/3911)
and makes the library unusable for us right now.

