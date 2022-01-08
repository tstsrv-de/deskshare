# tst-srv.de Config README

Here is the config and some scripts for 'Desksahre' > tstsrv.de for Max and Co.

- Hosted @ hosteurope.de
- Domain @ inwx.de

Henning 'haenno' Beier, haenno@web.de, 08.01.2022

## Automation of git repo updates to this servers containers

With scripts in the **crontab** this server automaticly pulls each new commit to this repo, and restarts the docker containers every 5 minutes. Every 30 minutes it performs a complete rebuild.

### Update

``*/5 * * * * /home/rjhadmin/deskshare/server-update-cron.sh >> /home/rjhadmin/deskshare_cronlog.txt 2>&1``

### Rebuild

``*/30 * * * * /home/rjhadmin/deskshare/server-rebuild.sh >> /home/rjhadmin/deskshare_cronlog.txt 2>&1``
