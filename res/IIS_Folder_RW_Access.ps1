$IncommingPath = "C:\Custom.Configuration.Provider.Demo"
$Acl = Get-Acl $IncommingPath
$Ar = New-Object  system.security.accesscontrol.filesystemaccessrule("IIS AppPool\Custom.Configuration.Provider.Demo.Pool","FullControl","ContainerInherit, ObjectInherit", "None", "Allow")
$Acl.SetAccessRule($Ar)
Set-Acl $IncommingPath $Acl