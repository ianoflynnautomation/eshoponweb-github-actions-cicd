metadata description = 'Create a Windows VM in a DevTestLab with Chocolatey packages and Azure DevOps Agent'

@description('The location of the resource.')
param location string = resourceGroup().location
@description('The name of the DevTest Lab in which to create the virtual machine.')
param labName string = 'Ians-DevTestLab'
@description('The name of the VM in the DevTest Lab')
param vmName string
@description('The name of the virtual network in the DevTest Lab.')
param labVirtualNetworkName string = 'vnet'
@description('The lab subnet name of the virtual machine.')
param labSubnetName string = 'subnet'
@description('The size of the virtual machine.')
@allowed([
  'Standard_D2ds_v4'
  'Standard_D4ds_v4'
  'Standard_D8ds_v4'
])
param vmSize string
@description('The user name of the virtual machine.')
param vmUserName string
@description('The password of the virtual machine administrator.')
@secure()
param adminPassword string
@description('The offer of the image.')
@allowed([
  'WindowsServer'
])
param imageOffer string
@description('The SKU of the image.')
@allowed([
  '2019-Datacenter'
  '2022-Datacenter'
])
param imageSku string
@description('Chocolatey packages for PowerShell')
param Install_Chocolatey_Packages_ps_packages string = 'powershell-core'
@description('Allow empty checksums for Chocolatey packages for PowerShell')
param Install_Chocolatey_Packages_ps_allowEmptyChecksums bool = true
@description('Ignore checksums for Chocolatey packages for PowerShell')
param Install_Chocolatey_Packages_ps_ignoreChecksums bool = false
@description('Chocolatey packages for Azure CLI')
param Install_Chocolatey_Packages_az_packages string = 'azure-cli'
@description('Allow empty checksums for Chocolatey packages for Azure CLI')
param Install_Chocolatey_Packages_az_allowEmptyChecksums bool = true
@description('Ignore checksums for Chocolatey packages for Azure CLI')
param Install_Chocolatey_Packages_az_ignoreChecksums bool = false
@description('Azure DevOps Account for the agent')
param Azure_Pipelines_Agent_vstsAccount string
@description('Azure DevOps PAT for the agent')
@secure()
param Azure_Pipelines_Agent_vstsPassword string
@description('Name of the agent')
param Azure_Pipelines_Agent_agentName string
@description('Suffix for the agent name')
param Azure_Pipelines_Agent_agentNameSuffix string = ''
@description('Azure DevOps Pool for the agent')
param Azure_Pipelines_Agent_poolName string
@description('Run as auto logon')
param Azure_Pipelines_Agent_RunAsAutoLogon bool = false
@description('Windows logon account')
param Azure_Pipelines_Agent_windowsLogonAccount string = ''
@description('Windows logon password')
@secure()
param Azure_Pipelines_Agent_windowsLogonPassword string
@description('Driver letter')
param Azure_Pipelines_Agent_driverLetter string = 'C'
@description('Work directory')
param Azure_Pipelines_Agent_workDirectory string = ''
@description('Replace agent')
param Azure_Pipelines_Agent_replaceAgent bool = true

// Variables
var labVirtualNetwordId = resourceId('Microsoft.DevTestLab/labs/virtualnetworks', labName, labVirtualNetworkName)
var vmId = resourceId('Microsoft.DevTestLab/labs/virtualmachines', labName, vmName)
var fullVmName = '${labName}/${vmName}'

// Create the Virtual Machine with artifacts
var defaultArtifacts = [
  {
    artifactId: resourceId(
      'Microsoft.DevTestLab/labs/artifactSources/artifacts',
      labName,
      'public repo',
      'windows-chocolatey'
    )
    parameters: [
      {
        name: 'packages'
        value: Install_Chocolatey_Packages_ps_packages
      }
      {
        name: 'allowEmptyChecksums'
        value: Install_Chocolatey_Packages_ps_allowEmptyChecksums
      }
      {
        name: 'ignoreChecksums'
        value: Install_Chocolatey_Packages_ps_ignoreChecksums
      }
    ]
  }
  {
    artifactId: resourceId(
      'Microsoft.DevTestLab/labs/artifactSources/artifacts',
      labName,
      'public repo',
      'windows-chocolatey'
    )
    parameters: [
      {
        name: 'packages'
        value: Install_Chocolatey_Packages_az_packages
      }
      {
        name: 'allowEmptyChecksums'
        value: Install_Chocolatey_Packages_az_allowEmptyChecksums
      }
      {
        name: 'ignoreChecksums'
        value: Install_Chocolatey_Packages_az_ignoreChecksums
      }
    ]
  }
  {
    artifactId: resourceId(
      'Microsoft.DevTestLab/labs/artifactSources/artifacts',
      labName,
      'public repo',
      'windows-vsts-build-agent'
    )
    parameters: [
      {
        name: 'vstsAccount'
        value: Azure_Pipelines_Agent_vstsAccount
      }
      {
        name: 'vstsPassword'
        value: Azure_Pipelines_Agent_vstsPassword
      }
      {
        name: 'agentName'
        value: Azure_Pipelines_Agent_agentName
      }
      {
        name: 'agentNameSuffix'
        value: Azure_Pipelines_Agent_agentNameSuffix
      }
      {
        name: 'poolName'
        value: Azure_Pipelines_Agent_poolName
      }
      {
        name: 'RunAsAutoLogon'
        value: Azure_Pipelines_Agent_RunAsAutoLogon
      }
      {
        name: 'windowsLogonAccount'
        value: Azure_Pipelines_Agent_windowsLogonAccount
      }
      {
        name: 'windowsLogonPassword'
        value: Azure_Pipelines_Agent_windowsLogonPassword
      }
      {
        name: 'driverLetter'
        value: Azure_Pipelines_Agent_driverLetter
      }
      {
        name: 'workDirectory'
        value: Azure_Pipelines_Agent_workDirectory
      }
      {
        name: 'replaceAgent'
        value: Azure_Pipelines_Agent_replaceAgent
      }
    ]
  }
]

// Create the Virtual Machine
resource vm 'Microsoft.DevTestLab/labs/virtualmachines@2018-09-15' = {
  name: fullVmName
  location: location
  properties: {
    labVirtualNetworkId: labVirtualNetwordId
    galleryImageReference: {
      offer: imageOffer
      publisher: 'microsoftwindowsserver'
      sku: imageSku
      osType: 'Windows'
      version: 'latest'
    }
    size: vmSize
    userName: vmUserName
    password: adminPassword
    isAuthenticationWithSshKey: false
    artifacts: defaultArtifacts
    labSubnetName: labSubnetName
    disallowPublicIpAddress: true
    storageType: 'StandardSSD'
    allowClaim: false
  }
}

output labVMId string = vmId
output labVMName string = vm.name
