param suffix string
param location string

resource signalR 'Microsoft.SignalRService/signalR@2023-08-01-preview' = {
  name: 'signalr-${suffix}'
  location: location 
  properties: {
    features: [
      {
        flag: 'ServiceMode'
        value: 'Default'
      }
      {
        flag: 'EnableConnectivityLogs'
        value: 'true'
      }
    ]
  }
  sku: {
    name: 'Premium_P1'
    tier: 'Premium'
    capacity: 1
  }
}
