# Define certificate details
$certName = "DataProtectionCert"
$certPassword = "YourDefaultDevelopmentSecureDataProtectionCertPassword"
$certSecureStrPassword = ConvertTo-SecureString $certPassword -AsPlainText -Force
$pfxFilePath = Join-Path $PSScriptRoot "$($certName).pfx"

Write-Host "--- Generating Self-Signed Certificate ---"

# Create a new self-signed certificate
try {
    $cert = New-SelfSignedCertificate -Subject "CN=$certName" `
        -KeyAlgorithm RSA -KeyLength 2048 `
        -CertStoreLocation "Cert:\CurrentUser\My" `
        -NotAfter (Get-Date).AddYears(1) `
        -KeyUsage DataEncipherment, KeyEncipherment `
        -Type DocumentEncryptionCert `

}
catch {
    Write-Error "Failed to create self-signed certificate. Please check the parameters. Exiting."
    exit 1
}

Write-Host "Certificate '$certName' created in 'Cert:\CurrentUser\My'."

# Export the certificate to a PFX file with the private key
try {
    Export-PfxCertificate -Cert $cert -FilePath $pfxFilePath -Password $certSecureStrPassword -Force
}
catch {
    Write-Error "Failed to export PFX certificate to '$pfxFilePath': $($_.Exception.Message). Exiting."
    exit 1
}

Write-Host "Certificate exported to: $pfxFilePath"

# Read the PFX file content as bytes
if (-not (Test-Path $pfxFilePath)) {
    Write-Error "PFX file not found at '$pfxFilePath'. Export likely failed. Exiting."
    exit 1
}

try {
    $pfxBytes = [System.IO.File]::ReadAllBytes($pfxFilePath)
}
catch {
    Write-Error "Failed to read PFX file bytes from '$pfxFilePath': $($_.Exception.Message). Exiting."
    exit 1
}

# Convert the PFX bytes to a Base64 string
try {
    $base64Cert = [System.Convert]::ToBase64String($pfxBytes)
}
catch {
    Write-Error "Failed to convert PFX bytes to Base64 string: $($_.Exception.Message). Exiting."
    exit 1
}

Write-Host "`n--- Base64 Encoded Certificate ---"
Write-Host "Save the following as your CERTIFICATE_BASE64 environment variable:"
Write-Host "CERTIFICATE_BASE64=$base64Cert"

Write-Host "`n--- Certificate Password ---"
Write-Host "Save the following as your CERTIFICATE_PASSWORD environment variable:"
Write-Host "CERTIFICATE_PASSWORD=$certPassword"

# Clean up the PFX file for security reasons, as its content is now in the Base64 string.
try {
    Remove-Item $pfxFilePath -Force
    Write-Host "`nCleaned up: Deleted the temporary PFX file '$pfxFilePath'."
}
catch {
    Write-Warning "Could not delete the PFX file '$pfxFilePath'. Please delete it manually for security reasons."
}