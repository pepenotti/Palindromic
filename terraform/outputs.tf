output "azure_function_url" {
    value = azurerm_windows_function_app.fa.default_hostname
}