task . {
    exec { podman build -f .\Caarro\Dockerfile -t registry.fly.io/caarro:latest . }
    exec { podman push registry.fly.io/caarro:latest }
    exec { flyctl deploy }
}
