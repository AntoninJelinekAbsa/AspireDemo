Start postgres container:
```
docker run --hostname=21b227157a31 --mac-address=02:42:ac:11:00:04 --env=POSTGRES_PASSWORD=p@ssword123 --env=GOSU_VERSION=1.16 --env=LANG=en_US.utf8 --env=PG_MAJOR=16 --env=PG_VERSION=16.1-1.pgdg120+1 --env=PGDATA=/var/lib/postgresql/data --env=POSTGRES_USER=SeriesUser --env=POSTGRES_DB=SeriesDb --env=PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/usr/lib/postgresql/16/bin --volume=/var/lib/postgresql/data -p 5432:5432 --restart=no --runtime=runc -d postgres:latest
```


Aspire

Install workload (can be done via Visual Studio installer):
```
dotnet workload install aspire
```











Links:
- https://github.com/prom3theu5/aspirational-manifests -  Automate deployment of a .NET Aspire AppHost to a Kubernetes Cluster
