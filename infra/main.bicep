targetScope = 'subscription'

@minLength(1)
@description('Primary location for all resources')
param location string

var suffix = uniqueString(rg.id)

resource rg 'Microsoft.Resources/resourceGroups@2022-09-01' = {
  name: 'rg-signalr-hub-demo'
  location: location
}

module signalR 'modules/signalR/signalRService.bicep' = {
  scope: resourceGroup(rg.name)
  name: 'signalR'
  params: {
    location: location
    suffix: suffix
  }
}
