task . {
    exec { podman build -f .\Caarro\Dockerfile -t registry.fly.io/caarro:latest . }
    exec { podman push registry.fly.io/caarro:latest }
    exec { flyctl deploy }
}

task migrate {
    Write-Output -Message 'Run a migraiton with "dotnet ef migraitons add $Name"'
}
