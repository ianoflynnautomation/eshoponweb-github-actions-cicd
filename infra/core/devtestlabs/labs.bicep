
metadata description = 'Create a DevTest Lab with a Virtual Network'

@description('The name of the DevTest Lab')
param labName string = 'Ians-DevTestLab'
@description('The name of the Virtual Network')
param labVirtualNetworkName string = 'vnet'
@description('The location of the DevTest Lab')
param location string = resourceGroup().location


// Define the DevTest Lab
resource devTestLab 'Microsoft.DevTestLab/labs@2018-09-15' = {
  name: labName
  location: location
  tags: {
    environment: 'Testing'
  }
  properties: {
    environmentPermission: 'Contributor'
    extendedProperties: {}
    labStorageType: 'Premium'
    mandatoryArtifactsResourceIdsLinux: []
    mandatoryArtifactsResourceIdsWindows: []
    premiumDataDisks: 'Disabled'
  }
}

// Define the DevTest Virtual Network
resource devTestVNet 'Microsoft.DevTestLab/labs/virtualnetworks@2018-09-15' = {
  name: 'Dtl${labVirtualNetworkName}'
  parent: devTestLab
  properties: {
    description: 'Virtual network for the DevTestLab'
    subnetOverrides: [
      {
        useInVmCreationPermission: 'Allow'
      }
    ]
  }
}
