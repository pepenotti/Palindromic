provider "azurerm" {
    features {
        resource_group {
          prevent_deletion_if_contains_resources = false
        }
    }
    subscription_id = var.azure_subcription_id
}

locals {
    prefix = "palindromic"
    region = var.azure_region
}

resource "azurerm_resource_group" "rg" {
    name     = "${local.prefix}-rg"
    location = "${local.region}"
}

resource "azurerm_storage_account" "acc" {
    name                     = "${replace(local.prefix,"-","")}sa"
    resource_group_name      = azurerm_resource_group.rg.name
    location                 = azurerm_resource_group.rg.location
    account_tier             = "Standard"
    account_replication_type = "LRS"
}

resource "azurerm_service_plan" "sp" {
    name                = "${local.prefix}-sp"
    location            = azurerm_resource_group.rg.location
    resource_group_name = azurerm_resource_group.rg.name
    os_type             = "Windows" 
    sku_name            = "Y1"
}

resource "azurerm_windows_function_app" "fa" {
    name                       = "${local.prefix}-fa"
    location                   = azurerm_resource_group.rg.location
    resource_group_name        = azurerm_resource_group.rg.name
    service_plan_id            = azurerm_service_plan.sp.id
    storage_account_name       = azurerm_storage_account.acc.name
    storage_account_access_key = azurerm_storage_account.acc.primary_access_key

    site_config { }
}