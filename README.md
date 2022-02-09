# README for 'Deskshare'-project

## Howto for local testing 

### Install git and docker
```shell
- Open a terminal, create an empty directory and change into it
- Clone git repo into the current directory with ``git clone git@github.com:tstsrv-de/deskshare.git .``
```

### rename Docker.yml
```shell
rename docker-compose.yaml to srv_docker-compose.yaml
rename local_docker-compose.yaml to docker-compose.yaml
```

### run docker-compose.yml on your local system
```shell
navigate to project foler 
run docker-compose up --build
```
