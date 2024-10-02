Add-Type -Path "C:\Program Files\Locktime Software\NetLimiter\NetLimiter.dll"

$cli = New-Object NetLimiter.Service.NLClient

$cli.Connect()
$table = $cli.GetAccessTable();

$list = [System.Collections.Generic.List[NetLimiter.Service.Api.AccessTableRow]]::new()

$list.AddRange($table);

$item = $list | Where-Object { $_.Name -ieq "builtin\users" -or $_.Name -ieq "users" }

if (!$item) {
    $item = [NetLimiter.Service.Api.AccessTableRow]::new()
    $item.Name = "Users"
    $list.Add($item)
}

$item.AllowedRights = [NetLimiter.Service.Security.Rights]::Monitor
$item.DeniedRights = 0

$cli.SetAccessTable($table)



