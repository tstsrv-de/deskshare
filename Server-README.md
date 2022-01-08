# tst-srv.de Config README

Here is the config and some scripts for 'Desksahre' > tstsrv.de for Max and Co.

- Hosted @ hosteurope.de
- Domain @ inwx.de

Henning 'haenno' Beier, haenno@web.de, 24.11.2021

## Automation of git repo updates to this servers containers

With the 'server-update-cron.sh' script in the **crontab** as...
``*/5 * * * * /home/rjhadmin/deskshare/server-update-cron.sh >> /home/rjhadmin/deskshare_cronlog.txt 2>&1``

...this server automaticly pulls each new commit to this repo, and restarts the docker containers.

## Note on howto bind a "docker run" command to traefik

``docker run -d --network tstsrv_default --label traefik.enable=true --label traefik.http.routers.ds.rule=Host\(\`ds.tstsrv.de\`\) --label traefik.http.routers.ds.entrypoints=https --label traefik.http.routers.ds.tls=true --label traefik.http.routers.ds.tls.certresolver=letsencrypt --name iamfoo containous/whoami``
