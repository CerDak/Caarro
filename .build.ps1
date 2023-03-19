task . {
    exec { docker build -f .\Caarro\Dockerfile -t registry.fly.io/caarro:latest . }
    exec { docker push registry.fly.io/caarro:latest }
    exec { flyctl deploy }
}
