param(
    [ValidateNotNullOrEmpty()]
    [string]$MigrationName
)

task . {
    exec { podman build -f .\Caarro\Dockerfile -t registry.fly.io/caarro:latest . }
    exec { podman push registry.fly.io/caarro:latest }
    exec { flyctl deploy }
}

task migrate {
    if ($null -ne $MigrationName) {
        $MigrationName = $MigrationName.Replace(' ', '_')
        cd .\Caarro
        exec { dotnet ef migrations add $MigrationName }
    } else {
        Write-Error 'Set the parameter -MigrationName to use the migration task' -EA Stop
    }
}
