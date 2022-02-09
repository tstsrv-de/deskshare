# README for 'Deskshare'-project

## Howto for local testing 

### Install git and docker
```shell
- Open a terminal, create an empty directory and change into it
- Clone git repo into the current directory with ``git clone git@github.com:tstsrv-de/deskshare.git .``
```

### rename Docker.yml
```shell
rename Docker.yaml to srv_Docker.yaml
rename local_Docker.yaml to Docker.yaml
```

### run Docker.yml
```shell
navigate to project foler 
run docker-compose up --build
```