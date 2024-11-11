targetScope = 'subscription'

@minLength(1)
@maxLength(64)
@description('Name of the the environment which is used to generate a short unique hash used in all resources.')
param environmentName string

@minLength(1)
@description('Primary location for all resources')
param location string

// Optional parameters to override the default azd resource naming conventions. Update the main.parameters.json file to provide values. e.g.,:
// "resourceGroupName": {
//      "value": "myGroupName"
// }
param resourceGroupName string = ''
param webServiceName string = ''
param catalogDatabaseName string = 'catalogDatabase'
param catalogDatabaseServerName string = ''
param identityDatabaseName string = 'identityDatabase'
param identityDatabaseServerName string = ''
param appServicePlanName string = ''
param keyVaultName string = ''

// Client VM parameters
param imageOfferUI string = 'windows-11'
param imageSkuUI string = 'win11-22h2-pro'
param vmSizeUI string = 'Standard_D2ds_v4'
param vmNameUI string = 'VMUI'
param vmUserName string = 'tester'
@secure()
param vmUserPassword string
@secure()
param agentPat string
param agentPool string = 'DevTestLabs-agents'
param labName string
param labVirtualNetworkName string = 'VNet'
param labSubnetName string = 'Subnet'
param organisationName string = 'myorg' // Azure DevOps organisation name
param chromeVersion string = 'latest'
param firefoxVersion string = 'latest'
@description('Amount of UI System Test VMs')
@minValue(1)
@maxValue(10)
param vmCountUiTest int = 1
param deploymentNameUI string = 'UiVmDeploy'

// Application Server VM parameters
param imageOfferAS string = 'windowsserver-gen2preview'
param imageSkuAS string = '2019-datacenter-gen2'
param vmSizeAS string = 'Standard_D2ds_v4'
param vmNameAS string = 'VMAS'
param windowsServerVmDeploymentName string = 'ApplicationServerVmDeploy'

@description('Id of the user or app to assign application roles')
param principalId string = ''

@secure()
@description('SQL Server administrator password')
param sqlAdminPassword string

@secure()
@description('Application user password')
param appUserPassword string

var abbrs = loadJsonContent('./abbreviations.json')
var resourceToken = toLower(uniqueString(subscription().id, environmentName, location))
var tags = { 'azd-env-name': environmentName }

// Organize resources in a resource group
resource rg 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: !empty(resourceGroupName) ? resourceGroupName : '${abbrs.resourcesResourceGroups}${environmentName}'
  location: location
  tags: tags
}

// The application frontend
module web './core/host/appservice.bicep' = {
  name: 'web'
  scope: rg
  params: {
    name: !empty(webServiceName) ? webServiceName : '${abbrs.webSitesAppService}web-${resourceToken}'
    location: location
    appServicePlanId: appServicePlan.outputs.id
    keyVaultName: keyVault.outputs.name
    runtimeName: 'dotnetcore'
    runtimeVersion: '8.0'
    tags: union(tags, { 'azd-service-name': 'web' })
    appSettings: {
      AZURE_SQL_CATALOG_CONNECTION_STRING_KEY: 'AZURE-SQL-CATALOG-CONNECTION-STRING'
      AZURE_SQL_IDENTITY_CONNECTION_STRING_KEY: 'AZURE-SQL-IDENTITY-CONNECTION-STRING'
      AZURE_KEY_VAULT_ENDPOINT: keyVault.outputs.endpoint
    }
  }
}

module apiKeyVaultAccess './core/security/keyvault-access.bicep' = {
  name: 'api-keyvault-access'
  scope: rg
  params: {
    keyVaultName: keyVault.outputs.name
    principalId: web.outputs.identityPrincipalId
  }
}

// The application database: Catalog
module catalogDb './core/database/sqlserver/sqlserver.bicep' = {
  name: 'sql-catalog'
  scope: rg
  params: {
    name: !empty(catalogDatabaseServerName) ? catalogDatabaseServerName : '${abbrs.sqlServers}catalog-${resourceToken}'
    databaseName: catalogDatabaseName
    location: location
    tags: tags
    sqlAdminPassword: sqlAdminPassword
    appUserPassword: appUserPassword
    keyVaultName: keyVault.outputs.name
    connectionStringKey: 'AZURE-SQL-CATALOG-CONNECTION-STRING'
  }
}

// The application database: Identity
module identityDb './core/database/sqlserver/sqlserver.bicep' = {
  name: 'sql-identity'
  scope: rg
  params: {
    name: !empty(identityDatabaseServerName)
      ? identityDatabaseServerName
      : '${abbrs.sqlServers}identity-${resourceToken}'
    databaseName: identityDatabaseName
    location: location
    tags: tags
    sqlAdminPassword: sqlAdminPassword
    appUserPassword: appUserPassword
    keyVaultName: keyVault.outputs.name
    connectionStringKey: 'AZURE-SQL-IDENTITY-CONNECTION-STRING'
  }
}

// Store secrets in a keyvault
module keyVault './core/security/keyvault.bicep' = {
  name: 'keyvault'
  scope: rg
  params: {
    name: !empty(keyVaultName) ? keyVaultName : '${abbrs.keyVaultVaults}${resourceToken}'
    location: location
    tags: tags
    principalId: principalId
  }
}

// Create an App Service Plan to group applications under the same payment plan and SKU
module appServicePlan './core/host/appserviceplan.bicep' = {
  name: 'appserviceplan'
  scope: rg
  params: {
    name: !empty(appServicePlanName) ? appServicePlanName : '${abbrs.webServerFarms}${resourceToken}'
    location: location
    tags: tags
    sku: {
      name: 'B1'
    }
  }
}

// Create a lab to create VMs to run UI tests
module lab './core/devtestlabs/labs.bicep' = if(environmentName == 'test') {
  name: !empty(labName) ? labName : '${abbrs.labLabPrefix}lab-${resourceToken}'
  scope: rg
  params: {
    location: location
  }
}

module clientVm './core/devtestlabs/windows-client-vm.bicep' = [
  for i in range(1, vmCountUiTest): if(environmentName == 'test') {
    name: '${deploymentNameUI}${i}'
    params: {
      vmName: '${abbrs.labVirtualMachinePrefix}-${vmNameUI}${i}'
      imageOffer: imageOfferUI
      imageSku: imageSkuUI
      location: location
      vmSize: vmSizeUI
      labName: labName
      labVirtualNetworkName: labVirtualNetworkName
      labSubnetName: labSubnetName
      vmUserName: vmUserName
      adminPassword: vmUserPassword
      Install_Chocolatey_Packages_chrome_packageVersion: chromeVersion
      Install_Chocolatey_Packages_firefox_packageVersion: firefoxVersion
      Azure_Pipelines_Agent_vstsAccount: organisationName
      Azure_Pipelines_Agent_vstsPassword: agentPat
      Azure_Pipelines_Agent_poolName: agentPool
      Azure_Pipelines_Agent_agentName: '${vmNameUI}${i}'
      Azure_Pipelines_Agent_RunAsAutoLogon: false
      Azure_Pipelines_Agent_windowsLogonAccount: vmUserName
      Azure_Pipelines_Agent_windowsLogonPassword: vmUserPassword
    }
    scope: rg
  }
]

module applicationServerVm './core/devtestlabs/windows-server-vm.bicep' = if(environmentName == 'test') {
  name: windowsServerVmDeploymentName
  params: {
    vmName: '${abbrs.labVirtualMachinePrefix}-${vmNameAS}'
    imageOffer: imageOfferAS
    imageSku: imageSkuAS
    location: location
    vmSize: vmSizeAS
    labName: labName
    labVirtualNetworkName: labVirtualNetworkName
    labSubnetName: labSubnetName
    vmUserName: vmUserName
    adminPassword: vmUserPassword
    Azure_Pipelines_Agent_vstsAccount: organisationName
    Azure_Pipelines_Agent_vstsPassword: agentPat
    Azure_Pipelines_Agent_poolName: agentPool
    Azure_Pipelines_Agent_agentName: vmNameAS
    Azure_Pipelines_Agent_RunAsAutoLogon: false
    Azure_Pipelines_Agent_windowsLogonAccount: vmUserName
    Azure_Pipelines_Agent_windowsLogonPassword: vmUserPassword
  }
  scope: rg
}

// Data outputs
output AZURE_SQL_CATALOG_CONNECTION_STRING_KEY string = catalogDb.outputs.connectionStringKey
output AZURE_SQL_IDENTITY_CONNECTION_STRING_KEY string = identityDb.outputs.connectionStringKey
output AZURE_SQL_CATALOG_DATABASE_NAME string = catalogDb.outputs.databaseName
output AZURE_SQL_IDENTITY_DATABASE_NAME string = identityDb.outputs.databaseName

// App outputs
output AZURE_LOCATION string = location
output AZURE_TENANT_ID string = tenant().tenantId
output AZURE_KEY_VAULT_ENDPOINT string = keyVault.outputs.endpoint
output AZURE_KEY_VAULT_NAME string = keyVault.outputs.name

// Application Server VM outputs
output APPLICATION_SERVER_VM_NAME string = applicationServerVm.outputs.labVMName
output APPLICATION_SERVER_VM_ID string = applicationServerVm.outputs.labVMId

// Client VM outputs
output CLIENT_VM_ID_NAME array = [
  for i in range(1, vmCountUiTest): {
    id: clientVm[i - 1].outputs.labVMId
    name: clientVm[i - 1].outputs.labVMName
  }
]

