#Command
    #- Azure CLI
        #- CLI 1.0 (NodeJS) - Deprecated
        #- CLI 2.0 (Python) - Current Version


Login-AzAccount

#list all resources groups
Get-AzureRmResourceGroup

#Create New Group
New-AzResourceGroup -Name MyDemoGroup -Location Southeastasia

#Create storage account
New-AzStorageAccount -Name sonusamplestorage2 -Location 'Central India' -ResourceGroupName MyDemoGroup -SkuName Standard_LRS -Kind StorageV2 -AccessTier Hot

#Remove storage account
Remove-AzStorageAccount -ResourceGroupName MyDemoGroup -Name sonusamplestorage2